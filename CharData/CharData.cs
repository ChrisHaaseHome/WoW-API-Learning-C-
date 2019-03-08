using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CharData
{
    
    public class CharData
    {
        public long lastModified { get; set; }
        public string name { get; set; }
        public string realm { get; set; }
        public string battlegroup { get; set; }
        public int @class { get; set; }
        public int race { get; set; }
        public int gender { get; set; }
        public int level { get; set; }
        public int achievementPoints { get; set; }
        public string thumbnail { get; set; }
        public string calcClass { get; set; }
        public int faction { get; set; }
        public int totalHonorableKills { get; set; }

        public static CharData getCharacterDescription(string server, string CharName, string AccessToken)
        {
            CharData charData = null;

            string address = @"https://us.api.blizzard.com/wow/character/" + server + "/" + CharName + "?locale=en_US&access_token=" + AccessToken;
            address = System.Uri.EscapeUriString(address);
            WebRequest request = WebRequest.Create(address);
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();
                Stream responseStream = httpResponse.GetResponseStream();
                if (responseStream != null)
                {
                    using (StreamReader sr = new StreamReader(responseStream))
                    {
                        string responseJSon = sr.ReadToEnd();
                        charData = JsonConvert.DeserializeObject<CharData>(responseJSon);
                    }
                }
            }
            catch (WebException)
            {
                MessageBox.Show("Unable to get character data.");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
            return charData;
        }
    }
}
