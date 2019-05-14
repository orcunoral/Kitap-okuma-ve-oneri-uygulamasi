namespace Yazlab
{
    partial class KullaniciGirisi
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
            this.kayit = new System.Windows.Forms.Button();
            this.giris = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.user_password = new System.Windows.Forms.TextBox();
            this.user_name = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // kayit
            // 
            this.kayit.Location = new System.Drawing.Point(125, 133);
            this.kayit.Name = "kayit";
            this.kayit.Size = new System.Drawing.Size(210, 25);
            this.kayit.TabIndex = 24;
            this.kayit.Text = "Kayıt ol";
            this.kayit.UseVisualStyleBackColor = true;
            this.kayit.Click += new System.EventHandler(this.kayit_Click);
            // 
            // giris
            // 
            this.giris.Location = new System.Drawing.Point(125, 102);
            this.giris.Name = "giris";
            this.giris.Size = new System.Drawing.Size(210, 25);
            this.giris.TabIndex = 23;
            this.giris.Text = "Giris";
            this.giris.UseVisualStyleBackColor = true;
            this.giris.Click += new System.EventHandler(this.giris_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(88, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Sifre:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Kullanici Adi:";
            // 
            // user_password
            // 
            this.user_password.Location = new System.Drawing.Point(125, 67);
            this.user_password.Name = "user_password";
            this.user_password.Size = new System.Drawing.Size(210, 20);
            this.user_password.TabIndex = 20;
            // 
            // user_name
            // 
            this.user_name.Location = new System.Drawing.Point(125, 41);
            this.user_name.Name = "user_name";
            this.user_name.Size = new System.Drawing.Size(210, 20);
            this.user_name.TabIndex = 19;
            // 
            // KullaniciGirisi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 197);
            this.Controls.Add(this.kayit);
            this.Controls.Add(this.giris);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.user_password);
            this.Controls.Add(this.user_name);
            this.MaximizeBox = false;
            this.Name = "KullaniciGirisi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KullaniciGirisi";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button kayit;
        private System.Windows.Forms.Button giris;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox user_password;
        private System.Windows.Forms.TextBox user_name;
    }
}