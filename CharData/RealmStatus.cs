using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace CharData
{
    public class Realm
    {
        public string type { get; set; }
        public string population { get; set; }
        public bool queue { get; set; }
        public bool status { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string battlegroup { get; set; }
        public string locale { get; set; }
        public string timezone { get; set; }
        public List<string> connected_realms { get; set; }
    }

    public class RealmStatus
    {
        public static RealmStatus getAllRealms(string accessToken)
        {
            RealmStatus realmList = null;
            string address = @"https://us.api.blizzard.com/wow/realm/status?locale=en_US&access_token=" + accessToken;
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
                        realmList = JsonConvert.DeserializeObject<RealmStatus>(responseJSon);
                    }
                }
            }
            catch (WebException)
            {
                MessageBox.Show("Unable to get list of realms.");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
            return realmList;
        }


        public List<Realm> realms { get; set; }
    }
}
