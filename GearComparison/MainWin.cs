using System;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;

namespace GearComparison
{
    public partial class MainWin : Form
    {
        StatWeights weights;
        ItemStats currItem;
        ItemStats newItem;

        public MainWin()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            InitializeComponent();
        }

        private double getItem1DPS()
        {
            double intel = 0.0, crit = 0.0, haste = 0.0, mastery = 0.0, vers = 0.0;

            if ((intTextBox1.Text != null) && (intTextBox1.TextLength > 0))
                intel = double.Parse(intTextBox1.Text) * weights.intellect;

            if ((critTextBox1.Text != null) && (critTextBox1.TextLength > 0))
                crit = double.Parse(critTextBox1.Text) * weights.crit;

            if ((hasteTextBox1.Text != null) && (hasteTextBox1.TextLength > 0))
                haste = double.Parse(hasteTextBox1.Text) * weights.haste;

            if ((mastTextBox1.Text != null) && (mastTextBox1.TextLength > 0))
                mastery = double.Parse(mastTextBox1.Text) * weights.mastery;

            if ((versTextBox1.Text != null) && (versTextBox1.TextLength > 0))
                vers = double.Parse(versTextBox1.Text) * weights.versatility;

            return intel + crit + haste + mastery + vers;
        }
        private double getItem2DPS()
        {
            double intel = 0.0, crit = 0.0, haste = 0.0, mastery = 0.0, vers = 0.0;

            if ((intTextBox2.Text != null) && (intTextBox2.TextLength > 0))
                intel = double.Parse(intTextBox2.Text) * weights.intellect;

            if ((critTextBox2.Text != null) && (critTextBox2.TextLength > 0))
                crit = double.Parse(critTextBox2.Text) * weights.crit;

            if ((hasteTextBox2.Text != null) && (hasteTextBox2.TextLength > 0))
                haste = double.Parse(hasteTextBox2.Text) * weights.haste;

            if ((mastTextBox2.Text != null) && (mastTextBox2.TextLength > 0))
                mastery = double.Parse(mastTextBox2.Text) * weights.mastery;

            if ((versTextBox2.Text != null) && (versTextBox2.TextLength > 0))
                vers = double.Parse(versTextBox2.Text) * weights.versatility;

            return intel + crit + haste + mastery + vers;
        }

        private void setCurrentStatWeights()
        {
            var results = MessageBox.Show("No weights defined. Do you want to load from a file?", "Stat Weights", MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);
            if (results == DialogResult.Cancel)
                return;
            else if (results == DialogResult.No)
                weights = new StatWeights();
            else
            {
                using (OpenFileDialog file = new OpenFileDialog())
                {
                    if (file.ShowDialog() == DialogResult.OK)
                    {
                        String statData = System.IO.File.ReadAllText(file.FileName);
                        weights = JsonConvert.DeserializeObject<StatWeights>(statData);
                        serverTextBox.Text = weights.server;
                        serverTextBox.ReadOnly = true;
                        charTextBox.Text = weights.name;
                        charTextBox.ReadOnly = true;
                        SpecTextBox.Text = weights.spec;
                        SpecTextBox.ReadOnly = true;

                    }
                    else
                        return;
                }
            }
            return;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (weights == null)
                weights = new StatWeights();

            using (StatWeightWin statWin = new StatWeightWin())
            {
                statWin.setStatWeights(weights);
                statWin.ShowDialog();
            }
        }


        private void button1_Click(Object sender, EventArgs e)
        {
            String dps1, dps2;
            double item1DPS, item2DPS, diff;

            if (weights == null)
            {
                setCurrentStatWeights();
                if (weights == null)
                    return;
            }

            item1DPS = getItem1DPS();
            item2DPS = getItem2DPS();

            dps1 = String.Format("{0:0.00}", item1DPS);
            dps2 = String.Format("{0:0.00}", item2DPS);
            diff = System.Math.Abs(item1DPS - item2DPS);
            if (diff <= 1E-4)
                MessageBox.Show("Items are equivalent.");
            else if (item1DPS > item2DPS)
                MessageBox.Show("Item 1 (" + dps1 + ") is better than item 2 (" + dps2 + ")");
            else
                MessageBox.Show("Item 2 (" + dps2 + ") is better than item 1 (" + dps1 + ")");

            return;
        }

        private bool isNumberInput(TextBox tbox)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(tbox.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                tbox.ResetText();
                return false;
            }
            return true;
        }

        private void intTextBox1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isNumberInput(intTextBox1))
                e.Cancel = true;

            return;
        }
        private void intTextBox2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isNumberInput(intTextBox2))
                e.Cancel = true;

            return;
        }
        private void critTextBox1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isNumberInput(critTextBox1))
                e.Cancel = true;

            return;
        }
        private void critTextBox2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isNumberInput(critTextBox2))
                e.Cancel = true;

            return;
        }
        private void hasteTextBox1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isNumberInput(hasteTextBox1))
                e.Cancel = true;

            return;
        }
        private void hasteTextBox2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isNumberInput(hasteTextBox2))
                e.Cancel = true;

            return;
        }
        private void mastTextBox1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isNumberInput(mastTextBox1))
                e.Cancel = true;

            return;
        }
        private void mastTextBox2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isNumberInput(mastTextBox2))
                e.Cancel = true;

            return;
        }

        private void versTextBox1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isNumberInput(versTextBox1))
                e.Cancel = true;

            return;
        }
        private void versTextBox2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isNumberInput(versTextBox2))
                e.Cancel = true;

            return;
        }

        private void serverTextBox_TextChanged(object sender, EventArgs e)
        {
            if (weights == null)
                weights = new StatWeights();

            weights.server = serverTextBox.Text;
        }

        private void charTextBox_TextChanged(object sender, EventArgs e)
        {
            if (weights == null)
                weights = new StatWeights();

            weights.name = charTextBox.Text;

        }

        private void SpecTextBox_TextChanged(object sender, EventArgs e)
        {
            if (weights == null)
                weights = new StatWeights();

            weights.spec = SpecTextBox.Text;

        }
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            weights = StatWeights.LoadStatWeights();
            if (weights != null)
            {
                serverTextBox.Text = weights.server;
                serverTextBox.ReadOnly = true;
                charTextBox.Text = weights.name;
                charTextBox.ReadOnly = true;
                SpecTextBox.Text = weights.spec;
                SpecTextBox.ReadOnly = true;
            }
            return;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (weights == null)
                return;

            weights.SaveStatWeights();
            serverTextBox.ReadOnly = true;
            charTextBox.ReadOnly = true;
            SpecTextBox.ReadOnly = true;
            return;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (weights == null)
                weights = new StatWeights();

            serverTextBox.ResetText();
            serverTextBox.ReadOnly = false;
            charTextBox.ResetText();
            charTextBox.ReadOnly = false;
            SpecTextBox.ResetText();
            SpecTextBox.ReadOnly = false;
        }

        private void setCurrentItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currItem == null)
                currItem = new ItemStats();

            using (ItemForm getItemForm = new ItemForm())
            {
                getItemForm.setItem(currItem);
                getItemForm.ShowDialog();
                intTextBox1.Text = String.Format("{0:0}", currItem.intellect);
                critTextBox1.Text = String.Format("{0:0}", currItem.crit);
                hasteTextBox1.Text = String.Format("{0:0}", currItem.haste);
                mastTextBox1.Text = String.Format("{0:0}", currItem.mastery);
                versTextBox1.Text = String.Format("{0:0}", currItem.versatility);
            }

        }

        private void setNewItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (newItem == null)
                newItem = new ItemStats();

            using (ItemForm getItemForm = new ItemForm())
            {
                getItemForm.setItem(newItem);
                getItemForm.ShowDialog();
                intTextBox2.Text = String.Format("{0:0}", newItem.intellect);
                critTextBox2.Text = String.Format("{0:0}", newItem.crit);
                hasteTextBox2.Text = String.Format("{0:0}", newItem.haste);
                mastTextBox2.Text = String.Format("{0:0}", newItem.mastery);
                versTextBox2.Text = String.Format("{0:0}", newItem.versatility);
            }

        }
    }
}
