namespace Yazlab
{
    partial class KayitForm
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
            this.user_kayit = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.user_age = new System.Windows.Forms.TextBox();
            this.user_location = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.user_password = new System.Windows.Forms.TextBox();
            this.user_name = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // user_kayit
            // 
            this.user_kayit.Location = new System.Drawing.Point(100, 136);
            this.user_kayit.Name = "user_kayit";
            this.user_kayit.Size = new System.Drawing.Size(210, 38);
            this.user_kayit.TabIndex = 17;
            this.user_kayit.Text = "Kayit Ol";
            this.user_kayit.UseVisualStyleBackColor = true;
            this.user_kayit.Click += new System.EventHandler(this.user_kayit_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(63, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Yas:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Konum:";
            // 
            // user_age
            // 
            this.user_age.Location = new System.Drawing.Point(100, 110);
            this.user_age.Name = "user_age";
            this.user_age.Size = new System.Drawing.Size(210, 20);
            this.user_age.TabIndex = 14;
            // 
            // user_location
            // 
            this.user_location.Location = new System.Drawing.Point(100, 84);
            this.user_location.Name = "user_location";
            this.user_location.Size = new System.Drawing.Size(210, 20);
            this.user_location.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Sifre:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Kullanici Adi:";
            // 
            // user_password
            // 
            this.user_password.Location = new System.Drawing.Point(100, 58);
            this.user_password.Name = "user_password";
            this.user_password.Size = new System.Drawing.Size(210, 20);
            this.user_password.TabIndex = 10;
            // 
            // user_name
            // 
            this.user_name.Location = new System.Drawing.Point(100, 32);
            this.user_name.Name = "user_name";
            this.user_name.Size = new System.Drawing.Size(210, 20);
            this.user_name.TabIndex = 9;
            // 
            // KayitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 224);
            this.Controls.Add(this.user_kayit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.user_age);
            this.Controls.Add(this.user_location);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.user_password);
            this.Controls.Add(this.user_name);
            this.MaximizeBox = false;
            this.Name = "KayitForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KayitForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox user_age;
        private System.Windows.Forms.TextBox user_location;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox user_password;
        private System.Windows.Forms.TextBox user_name;
        public System.Windows.Forms.Button user_kayit;
    }
}