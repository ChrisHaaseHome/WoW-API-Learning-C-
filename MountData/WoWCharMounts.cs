using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace MountList
{


    public class MountCollection
    {
        public int numCollected { get; set; }
        public int numNotCollected { get; set; }
        public List<Mount> collected { get; set; }
    }

    public class WoWCharMounts
    {
        public static WoWCharMounts getMountListForCharacter(string server, string name, string accessToken)
        {
            WoWCharMounts MountList = null;

            if (server == null || server.Length <= 0)
                return null;

            if (name == null || name.Length <= 0)
                return null;

            string address = @"https://us.api.blizzard.com/wow/character/" + server + "/" + name + "?fields=mounts&locale=en_US&access_token=" + accessToken;
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
                        MountList = JsonConvert.DeserializeObject<WoWCharMounts>(responseJSon);
                    }
                }
            }
            catch (WebException)
            {
                MessageBox.Show("Unable to get list of mounts.");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
            return MountList;
        }

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
        public MountCollection mounts { get; set; }
        public int totalHonorableKills { get; set; }
    }

}
