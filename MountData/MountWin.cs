using System;
using System.Windows.Forms;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MountList
{
    public partial class MountWin : Form
    {
        WoWMounts AllMounts;
        RealmStatus AllRealms;
        string ACCESS_TOKEN;

        class AccessToken
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public long expires_in { get; set; }
        }

        private static async Task<string> GetToken()
        {
            String responseStr = String.Empty;
            string clientId = "d75cab72a46748deadcfc30325d4e17c";
            string clientSecret = "Fqb6O2KYnJIKjrH2wDZkxw7gcf4ejQtV";
            string credentials = String.Format("{0}:{1}", clientId, clientSecret);

            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("POST"), "https://us.battle.net/oauth/token"))
                {
                    String base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes(clientId + ":" + clientSecret));
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {base64authorization}");
                    request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");
                    HttpResponseMessage response = httpClient.SendAsync(request).Result;
                    response.EnsureSuccessStatusCode();

                    String responseBodyAsText = await response.Content.ReadAsStringAsync();

                    if (!String.IsNullOrWhiteSpace(responseBodyAsText))
                    {
                        AccessToken token = JsonConvert.DeserializeObject<AccessToken>(responseBodyAsText);
                        responseStr = token.access_token;
                    }
                }
            }

            return responseStr;
        }

        public MountWin()
        {
            DataTable GridTable;
            InitializeComponent();

            dataGridView1.DataSource = new DataTable();
            GridTable = (DataTable)dataGridView1.DataSource;
            GridTable.Columns.Add("Name", typeof(string));
            GridTable.Columns.Add("Ground", typeof(bool));
            GridTable.Columns.Add("Flying", typeof(bool));
            GridTable.Columns.Add("Aquatic", typeof(bool));
            GridTable.Columns.Add("Jumping", typeof(bool));

            // get the access token
            ACCESS_TOKEN = GetToken().Result;

            // Get the list of all the mounts in the system if it hasn't been done yet
            AllMounts = WoWMounts.getAllMounts(ACCESS_TOKEN);
            if (AllMounts == null)
                return;

            AllMounts.mounts.Sort((x, y) => String.Compare(x.name, y.name));

            AllRealms = RealmStatus.getAllRealms(ACCESS_TOKEN);
            if (AllRealms == null)
                return;

            AllRealms.realms.Sort((x, y) => String.Compare(x.name, y.name));

            foreach(Realm realm in AllRealms.realms)
            {
                ServerComboBox.Items.Add(realm.name);
            }


            return;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int maxLen = 0, tmpLen = 0;
            DataTable GridTable;
            WoWCharMounts myMounts;
            Dictionary<string, Mount> dict = new Dictionary<string, Mount>();

            if ((CharacterTextBox.Text == null) || (CharacterTextBox.Text.Length <= 0))
            {
                MessageBox.Show("Please enter a character name.");
                return;
            }

            if (ServerComboBox.Text == null || (ServerComboBox.Text.Length <= 0))
            {
                MessageBox.Show("Please pick a server name.");
            }

            // get the mounts for a specifc character
            myMounts = WoWCharMounts.getMountListForCharacter(ServerComboBox.Text, CharacterTextBox.Text, ACCESS_TOKEN);
            if (myMounts == null)
                return;

            myMounts.mounts.collected.Sort((x, y) => String.Compare(x.name, y.name));

            // Find what mounts are missing for this character and populate the list box
            var DiffMounts = AllMounts.mounts.Except(myMounts.mounts.collected);

            GridTable = (DataTable)dataGridView1.DataSource;
            GridTable.Clear();
            foreach (Mount mount in DiffMounts)
            {
                if (!dict.ContainsKey(mount.name))
                {
                    dict.Add(mount.name, mount);
                    GridTable.Rows.Add(mount.name, mount.isGround, mount.isFlying, mount.isAquatic, mount.isJumping);
                    tmpLen = TextRenderer.MeasureText(mount.name, dataGridView1.Font).Width;
                    if (tmpLen > maxLen)
                        maxLen = tmpLen;
                }
            }

            dataGridView1.Columns[0].Width = maxLen;
            return;
         }
         private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ServerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CharacterTextBox.Clear();
        }
    }
}
