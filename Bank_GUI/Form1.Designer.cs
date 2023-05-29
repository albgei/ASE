namespace Bank_GUI
{
    partial class BankingGUI
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label3 = new System.Windows.Forms.Label();
            this.cbBank = new System.Windows.Forms.ComboBox();
            this.btnNewAccount = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbTransactions = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tBAccout = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.lbSavings = new System.Windows.Forms.ListBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Kontoname";
            // 
            // cbBank
            // 
            this.cbBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBank.FormattingEnabled = true;
            this.cbBank.Location = new System.Drawing.Point(54, 58);
            this.cbBank.Name = "cbBank";
            this.cbBank.Size = new System.Drawing.Size(164, 23);
            this.cbBank.TabIndex = 2;
            this.cbBank.SelectedIndexChanged += new System.EventHandler(this.cbBank_SelectedIndexChanged);
            // 
            // btnNewAccount
            // 
            this.btnNewAccount.Location = new System.Drawing.Point(54, 87);
            this.btnNewAccount.Name = "btnNewAccount";
            this.btnNewAccount.Size = new System.Drawing.Size(164, 23);
            this.btnNewAccount.TabIndex = 3;
            this.btnNewAccount.Text = "Neues Konto hinzufügen";
            this.btnNewAccount.UseVisualStyleBackColor = true;
            this.btnNewAccount.Click += new System.EventHandler(this.btnNewAccount_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 163);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Aktive Transaktionen";
            // 
            // lbTransactions
            // 
            this.lbTransactions.FormattingEnabled = true;
            this.lbTransactions.ItemHeight = 15;
            this.lbTransactions.Location = new System.Drawing.Point(54, 181);
            this.lbTransactions.Name = "lbTransactions";
            this.lbTransactions.Size = new System.Drawing.Size(296, 199);
            this.lbTransactions.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(54, 386);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(164, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Neue Transaktion anlegen";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(54, 415);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(164, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Neues Gehalt anlegen";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tBAccout
            // 
            this.tBAccout.Location = new System.Drawing.Point(54, 114);
            this.tBAccout.Name = "tBAccout";
            this.tBAccout.Size = new System.Drawing.Size(296, 23);
            this.tBAccout.TabIndex = 8;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(228, 87);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(122, 23);
            this.button4.TabIndex = 10;
            this.button4.Text = "Konto löschen";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(225, 386);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(125, 23);
            this.button5.TabIndex = 11;
            this.button5.Text = "Transaktion löschen";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // lbSavings
            // 
            this.lbSavings.FormattingEnabled = true;
            this.lbSavings.ItemHeight = 15;
            this.lbSavings.Location = new System.Drawing.Point(418, 181);
            this.lbSavings.Name = "lbSavings";
            this.lbSavings.Size = new System.Drawing.Size(327, 199);
            this.lbSavings.TabIndex = 12;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(418, 152);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(200, 23);
            this.button3.TabIndex = 13;
            this.button3.Text = "Alle Kontostände berechnen";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(418, 123);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(200, 23);
            this.button6.TabIndex = 14;
            this.button6.Text = "Aktuellen Kontostand berechnen";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(418, 94);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(200, 23);
            this.button7.TabIndex = 15;
            this.button7.Text = "Akuelles Konto prüfen";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // BankingGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.lbSavings);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.tBAccout);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbTransactions);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnNewAccount);
            this.Controls.Add(this.cbBank);
            this.Controls.Add(this.label3);
            this.Name = "BankingGUI";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BankingGUI_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label label3;
        private ComboBox cbBank;
        private Button btnNewAccount;
        private Label label1;
        private ListBox lbTransactions;
        private Button button1;
        private Button button2;
        private TextBox tBAccout;
        private Button button4;
        private Button button5;
        private ListBox lbSavings;
        private Button button3;
        private Button button6;
        private Button button7;
    }
}