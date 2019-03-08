namespace GearComparison
{
    partial class StatWeightWin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.intStatBox = new System.Windows.Forms.TextBox();
            this.critStatBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.hasteStatBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.mastStatBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.versStatBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Intellect";
            // 
            // intStatBox
            // 
            this.intStatBox.Location = new System.Drawing.Point(110, 36);
            this.intStatBox.Name = "intStatBox";
            this.intStatBox.Size = new System.Drawing.Size(100, 20);
            this.intStatBox.TabIndex = 1;
            this.intStatBox.Leave += new System.EventHandler(this.intStatBox_Leave);
            this.intStatBox.Validating += new System.ComponentModel.CancelEventHandler(this.intTextBox_Validating);
            // 
            // critStatBox
            // 
            this.critStatBox.Location = new System.Drawing.Point(110, 76);
            this.critStatBox.Name = "critStatBox";
            this.critStatBox.Size = new System.Drawing.Size(100, 20);
            this.critStatBox.TabIndex = 3;
            this.critStatBox.Leave += new System.EventHandler(this.critStatBox_Leave);
            this.critStatBox.Validating += new System.ComponentModel.CancelEventHandler(this.critTextBox_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Crit";
            // 
            // hasteStatBox
            // 
            this.hasteStatBox.Location = new System.Drawing.Point(110, 111);
            this.hasteStatBox.Name = "hasteStatBox";
            this.hasteStatBox.Size = new System.Drawing.Size(100, 20);
            this.hasteStatBox.TabIndex = 5;
            this.hasteStatBox.Leave += new System.EventHandler(this.hasteStatBox_Leave);
            this.hasteStatBox.Validating += new System.ComponentModel.CancelEventHandler(this.hasteTextBox_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Haste";
            // 
            // mastStatBox
            // 
            this.mastStatBox.Location = new System.Drawing.Point(110, 147);
            this.mastStatBox.Name = "mastStatBox";
            this.mastStatBox.Size = new System.Drawing.Size(100, 20);
            this.mastStatBox.TabIndex = 7;
            this.mastStatBox.Leave += new System.EventHandler(this.mastStatBox_Leave);
            this.mastStatBox.Validating += new System.ComponentModel.CancelEventHandler(this.mastTextBox_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Mastery";
            // 
            // versStatBox
            // 
            this.versStatBox.Location = new System.Drawing.Point(110, 184);
            this.versStatBox.Name = "versStatBox";
            this.versStatBox.Size = new System.Drawing.Size(100, 20);
            this.versStatBox.TabIndex = 9;
            this.versStatBox.Leave += new System.EventHandler(this.versStatBox_Leave);
            this.versStatBox.Validating += new System.ComponentModel.CancelEventHandler(this.versTextBox_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(42, 187);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Vers";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(85, 230);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 31);
            this.button2.TabIndex = 11;
            this.button2.Text = "Exit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(253, 293);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.versStatBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.mastStatBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.hasteStatBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.critStatBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.intStatBox);
            this.Controls.Add(this.label1);
            this.Name = "Form2";
            this.Text = "Stat Weights";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox intStatBox;
        private System.Windows.Forms.TextBox critStatBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox hasteStatBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox mastStatBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox versStatBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;
    }
}