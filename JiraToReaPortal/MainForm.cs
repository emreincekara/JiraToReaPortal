using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace JiraToReaPortal
{
    public partial class MainForm : Form
    {
        private static readonly string jiraBaseUrl = "https://borusanotomotiv.atlassian.net/rest/api/2";
        private static readonly string reaBaseUrl = "https://portalapi.reateknoloji.com/api";

        private static string reaToken = string.Empty;
        private static string jiraAccountId = string.Empty;

        private static int reaUserId = 0;

        private bool IsLoggedInForRea = false;
        private bool IsLoggedInForJira = false;

        private readonly CheckBox HeaderCheckBox = new();
        private bool isUpdating = false;

        private static int selectedCount = 0;

        public MainForm()
        {
            InitializeComponent();

            Point headerCellLocation = dgWorklog.GetCellDisplayRectangle(0, -1, true).Location;

            HeaderCheckBox.Location = new Point(headerCellLocation.X + 10, headerCellLocation.Y + 4);
            HeaderCheckBox.Size = new Size(18, 18);
            HeaderCheckBox.BackColor = Color.White;
            HeaderCheckBox.CheckedChanged += HeaderCheckBox_CheckedChanged;

            dgWorklog.Controls.Add(HeaderCheckBox);

            lblGithub.Text = $"© {DateTime.Now.Year} @emreincekara. All rights reserved.";

            InitReaData();
            InitJiraData();
        }

        public class ComboboxItem
        {
            public required string Name { get; set; }
            public required string Value { get; set; }
        }

        public class IssueDetail
        {
            public required string Key { get; set; }
            public required string Summary { get; set; }
            public List<WorklogDetail> Worklogs { get; set; } = [];
        }

        public class WorklogDetail
        {
            public required string AccountId { get; set; }
            public string? Comment { get; set; }
            public required string Started { get; set; }
            public required string Ended { get; set; }
            public required string TimeSpent { get; set; }
            public double TimeSpentHours { get; set; }
        }

        public class TimeSheetDetail
        {
            public int UserId { get; set; }
            public int ProjectId { get; set; }
            public required string Task { get; set; }
            public required string StartDate { get; set; }
            public required string EndDate { get; set; }
            public double Effort { get; set; }
            public string? Comment { get; set; }
        }

        private void InitReaData()
        {
            if (Properties.Settings.Default.ReaUsername != string.Empty)
            {
                if (Properties.Settings.Default.ReaRememberMe == true)
                {
                    txtReaUsername.Text = Properties.Settings.Default.ReaUsername;
                    txtReaPassword.Text = Properties.Settings.Default.ReaPassword;
                    chkReaRememberMe.Checked = true;
                }
                else
                {
                    txtReaUsername.Text = Properties.Settings.Default.ReaUsername;
                    txtReaPassword.Text = Properties.Settings.Default.ReaPassword;
                }
            }
        }

        private void SaveReaData()
        {
            if (chkReaRememberMe.Checked)
            {
                Properties.Settings.Default.ReaUsername = txtReaUsername.Text.Trim();
                Properties.Settings.Default.ReaPassword = txtReaPassword.Text.Trim();
                Properties.Settings.Default.ReaRememberMe = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.ReaUsername = string.Empty;
                Properties.Settings.Default.ReaPassword = string.Empty;
                Properties.Settings.Default.ReaRememberMe = false;
                Properties.Settings.Default.Save();
            }
        }

        private void InitJiraData()
        {
            if (Properties.Settings.Default.JiraUsername != string.Empty)
            {
                if (Properties.Settings.Default.JiraRememberMe == true)
                {
                    txtJiraUsername.Text = Properties.Settings.Default.JiraUsername;
                    txtJiraAPIToken.Text = Properties.Settings.Default.JiraAPIToken;
                    chkJiraRememberMe.Checked = true;
                }
                else
                {
                    txtJiraUsername.Text = Properties.Settings.Default.JiraUsername;
                    txtJiraAPIToken.Text = Properties.Settings.Default.JiraAPIToken;
                }
            }
        }

        private void SaveJiraData()
        {
            if (chkJiraRememberMe.Checked)
            {
                Properties.Settings.Default.JiraUsername = txtJiraUsername.Text.Trim();
                Properties.Settings.Default.JiraAPIToken = txtJiraAPIToken.Text.Trim();
                Properties.Settings.Default.JiraRememberMe = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.JiraUsername = string.Empty;
                Properties.Settings.Default.JiraAPIToken = string.Empty;
                Properties.Settings.Default.JiraRememberMe = false;
                Properties.Settings.Default.Save();
            }
        }

        private async void btnReaLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtReaUsername.Text) && !string.IsNullOrEmpty(txtReaPassword.Text))
                {
                    if (!IsLoggedInForRea)
                    {
                        SaveReaData();

                        string authUrl = $"{reaBaseUrl}/auth/login";

                        var authData = new
                        {
                            username = txtReaUsername.Text,
                            password = txtReaPassword.Text
                        };
                        var authJson = JsonConvert.SerializeObject(authData);

                        using var authClient = new HttpClient();
                        using var authResponse = await authClient.PostAsync(authUrl, new StringContent(authJson, Encoding.UTF8, "application/json"));

                        if (authResponse.IsSuccessStatusCode)
                        {
                            var authResult = JsonConvert.DeserializeObject<dynamic>(await authResponse.Content.ReadAsStringAsync());
                            reaToken = authResult?.data.accessToken;

                            string userUrl = $"{reaBaseUrl}/user/getAll";

                            using var userClient = new HttpClient();
                            userClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", reaToken);

                            using var userResponse = await userClient.GetAsync(userUrl);

                            if (!userResponse.IsSuccessStatusCode)
                            {
                                MessageBox.Show("Failed to get user with status code: " + userResponse.StatusCode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            var userData = JsonConvert.DeserializeObject<dynamic>(await userResponse.Content.ReadAsStringAsync());

                            var users = userData?.data;

                            foreach (var user in users)
                            {
                                if (txtReaUsername.Text == user["userName"].ToString())
                                {
                                    reaUserId = user["id"];
                                    break;
                                }
                            }

                            if (reaUserId is 0)
                            {
                                MessageBox.Show("User id not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            string projectsUrl = $"{reaBaseUrl}/project/getAll";

                            using var projectClient = new HttpClient();
                            projectClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", reaToken);

                            using var projectsResponse = await projectClient.GetAsync(projectsUrl);

                            if (!projectsResponse.IsSuccessStatusCode)
                            {
                                MessageBox.Show("Failed to get projects with status code: " + projectsResponse.StatusCode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            var projectData = JsonConvert.DeserializeObject<dynamic>(await projectsResponse.Content.ReadAsStringAsync());

                            var projects = projectData?.data;

                            cmbProjectName.DisplayMember = "Name";
                            cmbProjectName.ValueMember = "Value";

                            var projectList = new List<ComboboxItem>();

                            foreach (var project in projects)
                            {
                                projectList.Add(new ComboboxItem()
                                {
                                    Name = project["name"],
                                    Value = project["id"]
                                });
                            }

                            if (projectList.Count != 0)
                            {
                                IsLoggedInForRea = lblProjectName.Enabled = cmbProjectName.Enabled = true;
                                lblReaUsername.Enabled = txtReaUsername.Enabled = lblReaPassword.Enabled = txtReaPassword.Enabled = chkReaRememberMe.Enabled = false;
                                btnReaLogin.Text = "Logout";
                            }

                            cmbProjectName.DataSource = projectList.ToArray();
                        }
                        else if (authResponse.StatusCode == HttpStatusCode.Unauthorized)
                            MessageBox.Show("Username or Password is wrong. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                            MessageBox.Show("Authentication failed with status code: " + authResponse.StatusCode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        IsLoggedInForRea = lblProjectName.Enabled = cmbProjectName.Enabled = false;
                        lblReaUsername.Enabled = txtReaUsername.Enabled = lblReaPassword.Enabled = txtReaPassword.Enabled = chkReaRememberMe.Enabled = true;
                        btnReaLogin.Text = "Login";
                    }

                    UpdateImportButtonState();
                }
                else
                    MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnJiraLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtJiraUsername.Text) && !string.IsNullOrEmpty(txtJiraAPIToken.Text))
                {
                    if (!IsLoggedInForJira)
                    {
                        SaveJiraData();

                        string authUrl = $"{jiraBaseUrl}/myself";

                        using var authClient = new HttpClient();

                        authClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{txtJiraUsername.Text}:{txtJiraAPIToken.Text}")));

                        using var authResponse = await authClient.GetAsync(authUrl);

                        if (authResponse.IsSuccessStatusCode)
                        {
                            var authResult = JsonConvert.DeserializeObject<dynamic>(await authResponse.Content.ReadAsStringAsync());
                            jiraAccountId = authResult?.accountId;

                            InitJiraData();

                            IsLoggedInForJira = grbWorklog.Enabled = true;
                            lblJiraUsername.Enabled = txtJiraUsername.Enabled = lblJiraAPIToken.Enabled = txtJiraAPIToken.Enabled = chkJiraRememberMe.Enabled = false;
                            btnJiraLogin.Text = "Logout";
                        }
                        else if (authResponse.StatusCode == HttpStatusCode.Unauthorized)
                            MessageBox.Show("Username or Password is wrong. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                            MessageBox.Show("Authentication failed with status code: " + authResponse.StatusCode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        IsLoggedInForJira = grbWorklog.Enabled = false;
                        lblJiraUsername.Enabled = txtJiraUsername.Enabled = lblJiraAPIToken.Enabled = txtJiraAPIToken.Enabled = chkJiraRememberMe.Enabled = true;
                        btnJiraLogin.Text = "Login";
                    }

                    UpdateImportButtonState();
                }
                else
                    MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                dgWorklog.Rows.Clear();

                string issueUrl = $"{jiraBaseUrl}/search?fields=summary,worklog&maxResults=1000&jql=worklogDate >= \"{dtpStartTime.Value.AddDays(-1):yyyy-MM-dd}\" and worklogDate < \"{dtpEndDate.Value.AddDays(1):yyyy-MM-dd}\" and (worklogAuthor in (\"{jiraAccountId}\"))&startAt=0";

                using var jiraClient = new HttpClient();

                jiraClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{txtJiraUsername.Text}:{txtJiraAPIToken.Text}")));

                using var issueResponse = await jiraClient.GetAsync(issueUrl);

                if (issueResponse.IsSuccessStatusCode)
                {
                    var issues = new List<IssueDetail>();

                    var issuesJson = JObject.Parse(await issueResponse.Content.ReadAsStringAsync());

                    foreach (var issueJson in JArray.Parse(issuesJson["issues"].ToString()))
                    {
                        var issueDetail = new IssueDetail
                        {
                            Key = issueJson["key"].ToString(),
                            Summary = issueJson["fields"]["summary"].ToString().Trim(),
                        };

                        var worklogData = issueJson["fields"]["worklog"];

                        if (Convert.ToInt32(worklogData["total"]) > Convert.ToInt32(worklogData["maxResults"]))
                        {
                            string worklogUrl = $"{jiraBaseUrl}/issue/{issueDetail.Key}/worklog?maxResults=1000&startAt=0";

                            using var worklogClient = new HttpClient();

                            worklogClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{txtJiraUsername.Text}:{txtJiraAPIToken.Text}")));

                            using var worklogResponse = await jiraClient.GetAsync(worklogUrl);

                            if (worklogResponse.IsSuccessStatusCode)
                            {
                                var worklogJson = JObject.Parse(await worklogResponse.Content.ReadAsStringAsync());

                                foreach (var worklogItem in JArray.Parse(worklogJson["worklogs"].ToString()))
                                    AddWorklogIfValid(worklogItem, issueDetail);
                            }
                        }
                        else
                        {
                            foreach (var worklogItem in JArray.Parse(worklogData["worklogs"].ToString()))
                                AddWorklogIfValid(worklogItem, issueDetail);
                        }
                    }

                    if (dgWorklog.Rows.Count > 0)
                        dgWorklog.Enabled = HeaderCheckBox.Checked = true;
                    else
                    {
                        dgWorklog.Enabled = HeaderCheckBox.Checked;
                        MessageBox.Show($"Worklog not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    GetSelectedRowCount();
                    UpdateImportButtonState();
                }
                else
                    MessageBox.Show("Failed to get worklog with status code: " + issueResponse.StatusCode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedWorklogs = dgWorklog.Rows.Cast<DataGridViewRow>()
                    .Where(row => row.Cells[0] is DataGridViewCheckBoxCell checkBoxCell && (bool)checkBoxCell.Value)
                    .Select(row => new TimeSheetDetail
                    {
                        UserId = reaUserId,
                        ProjectId = Convert.ToInt32(cmbProjectName.SelectedValue),
                        Task = $"{row.Cells["Key"].Value} {row.Cells["Summary"].Value}",
                        StartDate = row.Cells["StartDate"].Value.ToString(),
                        EndDate = row.Cells["EndDate"].Value.ToString(),
                        Effort = Convert.ToDouble(row.Cells["TimeSpentHours"].Value),
                        Comment = row.Cells["Comment"].Value.ToString()
                    })
                    .ToList();

                if (selectedWorklogs.Count > 0)
                {
                    var result = MessageBox.Show("Are you sure you want to proceed?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        string originalButtonText = btnImport.Text;

                        btnImport.Text = "Processing...";
                        btnImport.Enabled = false;

                        string timeSheetCreateUrl = $"{reaBaseUrl}/timeSheet/create";

                        using var reaClient = new HttpClient();
                        reaClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", reaToken);

                        foreach (var worklog in selectedWorklogs)
                        {
                            var worklogJson = JsonConvert.SerializeObject(worklog);

                            using var timeSheetCreateResponse = await reaClient.PostAsync(timeSheetCreateUrl, new StringContent(worklogJson, Encoding.UTF8, "application/json"));

                            if (timeSheetCreateResponse.IsSuccessStatusCode)
                            {
                                var timeSheetCreateResult = JsonConvert.DeserializeObject<dynamic>(await timeSheetCreateResponse.Content.ReadAsStringAsync());

                                var status = timeSheetCreateResult?.status ?? 1;

                                if (status != 0)
                                    MessageBox.Show($"Failed to process worklog for Task: {worklog.Task} on {worklog.StartDate}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else if (timeSheetCreateResponse.StatusCode == HttpStatusCode.Unauthorized)
                            {
                                MessageBox.Show("Authentication failed with status code: " + timeSheetCreateResponse.StatusCode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                IsLoggedInForRea = lblProjectName.Enabled = cmbProjectName.Enabled = false;
                                lblReaUsername.Enabled = txtReaUsername.Enabled = lblReaPassword.Enabled = txtReaPassword.Enabled = chkReaRememberMe.Enabled = true;
                                btnReaLogin.Text = "Login";

                                break;
                            }
                        }

                        btnImport.Text = originalButtonText;
                        UpdateImportButtonState();
                    }
                }
                else
                    MessageBox.Show("Please select at least one entry from the table before proceeding.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "https://github.com/emreincekara",
                    UseShellExecute = true
                });
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while opening the website.");
            }
        }

        private void txtReaPassword_Enter(object sender, EventArgs e)
        {
            if (txtReaPassword.Text.Length > 0)
                txtReaPassword.UseSystemPasswordChar = true;
        }

        private void txtReaPassword_Leave(object sender, EventArgs e)
        {
            if (txtReaPassword.Text.Length == 0)
            {
                txtReaPassword.UseSystemPasswordChar = false;
                SelectNextControl(txtReaPassword, true, true, false, true);
            }
        }

        private void txtJiraAPIToken_Enter(object sender, EventArgs e)
        {
            if (txtJiraAPIToken.Text.Length > 0)
                txtJiraAPIToken.UseSystemPasswordChar = true;
        }

        private void txtJiraAPIToken_Leave(object sender, EventArgs e)
        {
            if (txtJiraAPIToken.Text.Length == 0)
            {
                txtJiraAPIToken.UseSystemPasswordChar = false;
                SelectNextControl(txtJiraAPIToken, true, true, false, true);
            }
        }

        private void dgWorklog_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                bool isChecked = (bool)dgWorklog.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                foreach (DataGridViewCell cell in dgWorklog.Rows[e.RowIndex].Cells)
                {
                    if (cell.ColumnIndex != e.ColumnIndex)
                    {
                        cell.ReadOnly = !isChecked;
                        cell.Style.ForeColor = isChecked ? Color.Black : Color.Gray;
                    }
                }

                dgWorklog.Rows[e.RowIndex].DefaultCellStyle.BackColor = isChecked ? Color.White : Color.LightGray;

                bool allChecked = true;

                foreach (DataGridViewRow row in dgWorklog.Rows)
                {
                    if (!(bool)row.Cells[0].Value)
                    {
                        allChecked = false;
                        break;
                    }
                }

                GetSelectedRowCount();

                HeaderCheckBox.CheckedChanged -= HeaderCheckBox_CheckedChanged;
                HeaderCheckBox.Checked = allChecked;
                HeaderCheckBox.CheckedChanged += HeaderCheckBox_CheckedChanged;
            }
        }

        private void dgWorklog_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgWorklog.CurrentCell is DataGridViewCheckBoxCell && dgWorklog.IsCurrentCellDirty)
                dgWorklog.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void HeaderCheckBox_CheckedChanged(object? sender, EventArgs e)
        {
            if (isUpdating) return;

            bool isChecked = HeaderCheckBox.Checked;

            try
            {
                isUpdating = true;

                foreach (DataGridViewRow row in dgWorklog.Rows)
                    row.Cells[0].Value = isChecked;
            }
            finally
            {
                isUpdating = false;
            }
        }

        private void GetSelectedRowCount()
        {
            selectedCount = 0;

            foreach (DataGridViewRow row in dgWorklog.Rows)
            {
                if (row.Cells[0] is DataGridViewCheckBoxCell checkBoxCell)
                {
                    bool isChecked = (bool)checkBoxCell.Value;

                    if (isChecked)
                        selectedCount++;
                }
            }

            lblSelectedRowCount.Text = $"Selected rows count: {selectedCount}";

            UpdateImportButtonState();
        }

        private void AddWorklogIfValid(dynamic worklogJson, IssueDetail issueDetail)
        {
            var accountId = worklogJson["author"]["accountId"].ToString();
            var started = Convert.ToDateTime(worklogJson["started"]);
            var timeSpentSeconds = Convert.ToInt32(worklogJson["timeSpentSeconds"].ToString());

            if (accountId == jiraAccountId && started >= dtpStartTime.Value.Date && started < dtpEndDate.Value.AddDays(1).Date && timeSpentSeconds > 0)
            {
                var worklogDetail = new WorklogDetail
                {
                    AccountId = accountId,
                    Comment = worklogJson["comment"].ToString().Trim(),
                    Started = started.ToString("yyyy-MM-dd"),
                    Ended = started.AddSeconds(timeSpentSeconds).ToString("yyyy-MM-dd"),
                    TimeSpent = worklogJson["timeSpent"].ToString(),
                    TimeSpentHours = timeSpentSeconds / 3600.0
                };

                issueDetail.Worklogs.Add(worklogDetail);

                dgWorklog.Rows.Add(true,
                    issueDetail.Key,
                    issueDetail.Summary,
                    worklogDetail.Comment,
                    worklogDetail.Started,
                    worklogDetail.Ended,
                    worklogDetail.TimeSpent,
                    worklogDetail.TimeSpentHours);
            }
        }

        public void UpdateImportButtonState()
        {
            btnImport.Enabled = IsLoggedInForJira && IsLoggedInForRea && selectedCount > 0;
        }
    }
}
