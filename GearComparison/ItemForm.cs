using GearComparison.WoWModels;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GearComparison
{
    public partial class ItemForm : Form
    {
        private ItemStats _item;
        private int STAT_MASTERY = 49;
        private int STAT_HASTE = 36;
        private int STAT_INTELLECT = 5;
        // private int STAT_STAMINA = 7;
        private int STAT_CRIT = 32;
        private int STAT_VERSATILITY = 40;
        private string ACCESS_TOKEN;

        internal void setItem(ItemStats item)
        {
            _item = item;
        }

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

        private bool getItemData(int itemID)
        {
            RootObject tmpItem;
            string address = @"https://us.api.blizzard.com/wow/item/" + itemID + "?locale=en_US&access_token=" + ACCESS_TOKEN;
            WebRequest request = WebRequest.Create(address);
            try
            {
                HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();
                Stream responseStream = httpResponse.GetResponseStream();
                if (responseStream != null)
                {
                    using (StreamReader sr = new StreamReader(responseStream))
                    {
                        string responseJSon = sr.ReadToEnd();
                        tmpItem = JsonConvert.DeserializeObject<RootObject>(responseJSon);
                        _item.intellect = _item.crit = _item.haste = _item.mastery = _item.versatility = 0;
                        foreach (BonusStat stat in tmpItem.bonusStats)
                        {
                            if (stat.stat == STAT_INTELLECT)
                                _item.intellect = stat.amount;
                            else if (stat.stat == STAT_CRIT)
                                _item.crit = stat.amount;
                            else if (stat.stat == STAT_HASTE)
                                _item.haste = stat.amount;
                            else if (stat.stat == STAT_MASTERY)
                                _item.mastery = stat.amount;
                            else if (stat.stat == STAT_VERSATILITY)
                                _item.versatility = stat.amount;
                        }
                        return true;
                    }
                }
            }
            catch (WebException e)
            {
                return false;
            }
            return false;
        }

        public ItemForm()
        {
            InitializeComponent();
            ACCESS_TOKEN = GetToken().Result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string itemID;

           itemID = maskedTextBox1.Text;
            if ((itemID != null) && (itemID.Length > 0))
            {
                if (getItemData(int.Parse(itemID)) == false)
                    MessageBox.Show("Invalid item ID specified. Please check the number and try again.");
                this.Close();
            }
        }
    }
}
