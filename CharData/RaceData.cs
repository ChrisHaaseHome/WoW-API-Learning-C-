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
    public class GenderName
    {
        public string male_name { get; set; }
        public string female_name { get; set; }
    }

    public class Faction
    {
        public int id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
    }

    public class WoWRace
    {
        public Links _links { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public GenderName gender_name { get; set; }
        public List<Faction> faction { get; set; }

        public static string getRaceFromID(string AccessToken, int RaceID)
        {
            WoWRace WoWRace = null;
            string address = @"https://us.api.blizzard.com/data/wow/race/" + RaceID + "?namespace=static-us&locale=en_US&access_token=" + AccessToken;
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
                        WoWRace = JsonConvert.DeserializeObject<WoWRace>(responseJSon);
                    }
                }
            }
            catch (WebException)
            {
                MessageBox.Show("Unable to get race name for race: <" + RaceID + ">.");
                return "Unknown Race " + RaceID;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

            return WoWRace.name;
        }
    }

}
