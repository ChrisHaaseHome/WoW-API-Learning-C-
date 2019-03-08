using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace MountList
{
    public class WoWMounts
    {
        public static WoWMounts getAllMounts(string accessToken)
        {
            WoWMounts MountList = null;
            string address = @"https://us.api.blizzard.com/wow/mount/?locale=en_US&access_token=" + accessToken;
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
                        MountList = JsonConvert.DeserializeObject<WoWMounts>(responseJSon);
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
        public List<Mount> mounts { get; set; }

    }
}
