using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace CharData
{
    public class Self
    {
        public string href { get; set; }
    }

    public class Links
    {
        public Self self { get; set; }
    }

    public class Key
    {
        public string href { get; set; }
    }

    public class WoWClass
    {
        public Key key { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }
    public class ClassData
    {
        public Links _links { get; set; }
        public List<WoWClass> classes { get; set; }

        public static string getClassNameFromID(List<WoWClass> WoWClasses, int ClassID)
        {
            foreach (WoWClass WClass in WoWClasses)
            {
                if (WClass.id == ClassID)
                    return WClass.name;
            }

            return "Unknow class <" + ClassID + ">";
        }


        public static List<WoWClass> getAllWoWClasses(string AccessToken)
        {
            ClassData ClassList = null;

            string address = @"https://us.api.blizzard.com/data/wow/playable-class/index?namespace=static-us&locale=en_US&access_token=" + AccessToken;
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
                        ClassList = JsonConvert.DeserializeObject<ClassData>(responseJSon);
                    }
                }
            }
            catch (WebException)
            {
                MessageBox.Show("Unable to get list of playable classes.");
                return null;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
            return ClassList.classes;
        }
    }

}
