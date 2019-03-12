using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CharData
{
    public partial class CharWin : Form
    {
        RealmStatus AllRealms;
        List<WoWClass> AllClasses;
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

        public CharWin()
        {
            InitializeComponent();

            ACCESS_TOKEN = GetToken().Result;
            AllRealms = RealmStatus.getAllRealms(ACCESS_TOKEN);
            if (AllRealms == null)
                return;

            AllRealms.realms.Sort((x, y) => String.Compare(x.name, y.name));

            foreach (Realm realm in AllRealms.realms)
            {
                ServerComboBox.Items.Add(realm.name);
            }

            AllClasses = ClassData.getAllWoWClasses(ACCESS_TOKEN);
            if (AllClasses == null)
                return;
        }

        private void ServerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CharacterTextBox.Clear();
        }

        private void CharacterTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CharData CurrentChar;
            string url;

            CurrentChar = CharData.getCharacterDescription(ServerComboBox.Text, CharacterTextBox.Text, ACCESS_TOKEN);
            if (CurrentChar == null)
                return;

            BattleGroup.Text = CurrentChar.battlegroup;
            Class.Text = ClassData.getClassNameFromID(AllClasses, CurrentChar.@class);
            Race.Text = WoWRace.getRaceFromID(ACCESS_TOKEN, CurrentChar.race);
            if (CurrentChar.gender == 0)
                Gender.Text = "Male";
            else
                Gender.Text = "Female";
            Level.Text = CurrentChar.level.ToString();

            url = "http://render-us.worldofwarcraft.com/character/" + CurrentChar.thumbnail;
            pictureBox1.ImageLocation = url;

            return;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
