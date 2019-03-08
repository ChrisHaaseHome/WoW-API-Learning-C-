using System;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace GearComparison
{
    internal class StatWeights
    {
        public string name;
        public string server;
        public string spec;
        public double intellect;
        public double haste;
        public double mastery;
        public double crit;
        public double versatility;

        public StatWeights()
        {
            name = "";
            server = "";
            spec = "";
            intellect = 1.0;
            haste = 1.0;
            mastery = 1.0;
            crit = 1.0;
            versatility = 1.0;
        }

        public StatWeights(StatWeights weights)
        {
            this.name = weights.name;
            this.server = weights.server;
            this.intellect = weights.intellect;
            this.haste = weights.haste;
            this.mastery = weights.mastery;
            this.crit = weights.crit;
            this.versatility = weights.versatility;
            this.spec = weights.spec;
        }

        public void setAllWeights(double intel, double haste, double mastery, double crit, double vers)
        {
            this.intellect = intel;
            this.haste = haste;
            this.mastery = mastery;
            this.crit = crit;
            this.versatility = vers;
        }

        public static StatWeights LoadStatWeights()
        {
            StatWeights weights = new StatWeights();
            using (OpenFileDialog file = new OpenFileDialog())
            {
                file.Filter = "Stat files (*.stats)|*.stats| All files (*.*)|*.*";
                if (file.ShowDialog() == DialogResult.OK)
                {
                    String statData = System.IO.File.ReadAllText(file.FileName);
                    try
                    {
                        weights = JsonConvert.DeserializeObject<StatWeights>(statData);
                        return weights;
                    }
                    catch
                    {
                        MessageBox.Show("Unable to load file. Confirm the file format and try again.");
                    }
                }
            }

            return weights;

        }

        public void SaveStatWeights()
        {
            if ((this.name == null) || (this.name.Length <= 0))
            {
                MessageBox.Show("Character Name has not been specified.");
                return;
            }

            if ((this.server == null) || (this.server.Length <= 0))
            {
                MessageBox.Show("Server has not been specified.");
                return;
            }

            if ((this.spec == null) || (this.spec.Length <= 0))
            {
                MessageBox.Show("Spec has not been specified.");
                return;
            }
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                String json = JsonConvert.SerializeObject(this, Formatting.Indented);
                using (SaveFileDialog fd = new SaveFileDialog())
                {
                    fd.Filter = "Stat files (*.stats)|*.stats| All files (*.*)|*.*";
                    DialogResult dr = fd.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        System.IO.File.WriteAllText(fd.FileName, json);
                    }
                }
                MessageBox.Show("Save Complete.");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }


        }
    }
}
