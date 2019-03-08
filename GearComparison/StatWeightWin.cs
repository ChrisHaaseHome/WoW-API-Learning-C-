using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GearComparison;
using Newtonsoft.Json;

namespace GearComparison
{
    public partial class StatWeightWin : Form
    {
        private StatWeights _weights;

        public StatWeightWin()
        {
            InitializeComponent();
        }

        internal void setStatWeights(StatWeights weights)
        {
            _weights = weights;
        }

        private bool validateStatWeight(TextBox tbox, System.ComponentModel.CancelEventArgs e)
        {
            String value = intStatBox.Text;
            double convValue;

            if (!double.TryParse(value, out convValue))
            {
                MessageBox.Show("Please enter only positive numbers.");
                tbox.ResetText();
                e.Cancel = true;
                return false;
            }

            if (convValue < 0.0)
            {
                MessageBox.Show("Please enter only positive numbers.");
                tbox.ResetText();
                e.Cancel = true;
                return false;
            }
            return true;
        }

        private void intTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.validateStatWeight(intStatBox, e);
            return;
        }

        private void critTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.validateStatWeight(critStatBox, e);
            return;
        }
        private void hasteTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.validateStatWeight(hasteStatBox, e);
            return;
        }

        private void mastTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.validateStatWeight(mastStatBox, e);
            return;
        }

        private void versTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.validateStatWeight(versStatBox, e);
            return;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            intStatBox.Text = String.Format("{0:0.00}",_weights.intellect);
            critStatBox.Text = String.Format("{0:0.00}", _weights.crit);
            hasteStatBox.Text = String.Format("{0:0.00}", _weights.haste);
            mastStatBox.Text = String.Format("{0:0.00}", _weights.mastery);
            versStatBox.Text = String.Format("{0:0.00}", _weights.versatility);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _weights.SaveStatWeights();
        }

        private void intStatBox_Leave(object sender, EventArgs e)
        {
            _weights.intellect = Double.Parse(intStatBox.Text);
            intStatBox.Text = String.Format("{0:0.00}", _weights.intellect);
        }

        private void critStatBox_Leave(object sender, EventArgs e)
        {
            _weights.crit = Double.Parse(critStatBox.Text);
            critStatBox.Text = String.Format("{0:0.00}", _weights.crit);
        }

        private void hasteStatBox_Leave(object sender, EventArgs e)
        {
            _weights.haste = Double.Parse(hasteStatBox.Text);
            hasteStatBox.Text = String.Format("{0:0.00}", _weights.haste);
        }

        private void mastStatBox_Leave(object sender, EventArgs e)
        {
            _weights.mastery = Double.Parse(mastStatBox.Text);
            mastStatBox.Text = String.Format("{0:0.00}", _weights.mastery);
        }

        private void versStatBox_Leave(object sender, EventArgs e)
        {
            _weights.versatility = Double.Parse(versStatBox.Text);
            versStatBox.Text = String.Format("{0:0.00}", _weights.versatility);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
