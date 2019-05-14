namespace Yazlab
{
    partial class SecimEkrani
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGeri = new System.Windows.Forms.Button();
            this.btnIleri = new System.Windows.Forms.Button();
            this.lblSayfa = new System.Windows.Forms.Label();
            this.kategoriler = new System.Windows.Forms.MenuStrip();
            this.tümKitaplarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kitapÖnerileriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enPopülerKitaplarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enIyiKitaplar = new System.Windows.Forms.ToolStripMenuItem();
            this.yeniEklenenKitaplar = new System.Windows.Forms.ToolStripMenuItem();
            this.çıkışToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnIlk = new System.Windows.Forms.Button();
            this.btnSon = new System.Windows.Forms.Button();
            this.panelBtn = new System.Windows.Forms.Panel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btnOy = new System.Windows.Forms.Button();
            this.kategoriler.SuspendLayout();
            this.panelBtn.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGeri
            // 
            this.btnGeri.Location = new System.Drawing.Point(73, 13);
            this.btnGeri.Name = "btnGeri";
            this.btnGeri.Size = new System.Drawing.Size(50, 20);
            this.btnGeri.TabIndex = 0;
            this.btnGeri.Text = "<";
            this.btnGeri.UseVisualStyleBackColor = true;
            this.btnGeri.Click += new System.EventHandler(this.btnGeri_Click);
            // 
            // btnIleri
            // 
            this.btnIleri.Location = new System.Drawing.Point(193, 13);
            this.btnIleri.Name = "btnIleri";
            this.btnIleri.Size = new System.Drawing.Size(50, 20);
            this.btnIleri.TabIndex = 1;
            this.btnIleri.Text = ">";
            this.btnIleri.UseVisualStyleBackColor = true;
            this.btnIleri.Click += new System.EventHandler(this.btnIleri_Click);
            // 
            // lblSayfa
            // 
            this.lblSayfa.AutoSize = true;
            this.lblSayfa.Location = new System.Drawing.Point(129, 17);
            this.lblSayfa.Name = "lblSayfa";
            this.lblSayfa.Size = new System.Drawing.Size(35, 13);
            this.lblSayfa.TabIndex = 2;
            this.lblSayfa.Text = "label1";
            // 
            // kategoriler
            // 
            this.kategoriler.Dock = System.Windows.Forms.DockStyle.None;
            this.kategoriler.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tümKitaplarToolStripMenuItem,
            this.kitapÖnerileriToolStripMenuItem,
            this.enPopülerKitaplarToolStripMenuItem,
            this.enIyiKitaplar,
            this.yeniEklenenKitaplar,
            this.çıkışToolStripMenuItem});
            this.kategoriler.Location = new System.Drawing.Point(193, 9);
            this.kategoriler.Name = "kategoriler";
            this.kategoriler.Size = new System.Drawing.Size(573, 24);
            this.kategoriler.TabIndex = 15;
            this.kategoriler.Text = "menuStrip1";
            // 
            // tümKitaplarToolStripMenuItem
            // 
            this.tümKitaplarToolStripMenuItem.Name = "tümKitaplarToolStripMenuItem";
            this.tümKitaplarToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.tümKitaplarToolStripMenuItem.Text = "Tüm Kitaplar";
            this.tümKitaplarToolStripMenuItem.Click += new System.EventHandler(this.tümKitaplarToolStripMenuItem_Click);
            // 
            // kitapÖnerileriToolStripMenuItem
            // 
            this.kitapÖnerileriToolStripMenuItem.Name = "kitapÖnerileriToolStripMenuItem";
            this.kitapÖnerileriToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.kitapÖnerileriToolStripMenuItem.Text = "Kitap Önerileri";
            this.kitapÖnerileriToolStripMenuItem.Click += new System.EventHandler(this.kitapÖnerileriToolStripMenuItem_Click);
            // 
            // enPopülerKitaplarToolStripMenuItem
            // 
            this.enPopülerKitaplarToolStripMenuItem.Name = "enPopülerKitaplarToolStripMenuItem";
            this.enPopülerKitaplarToolStripMenuItem.Size = new System.Drawing.Size(119, 20);
            this.enPopülerKitaplarToolStripMenuItem.Text = "En Popüler Kitaplar";
            this.enPopülerKitaplarToolStripMenuItem.Click += new System.EventHandler(this.enPopülerKitaplarToolStripMenuItem_Click);
            // 
            // enIyiKitaplar
            // 
            this.enIyiKitaplar.Name = "enIyiKitaplar";
            this.enIyiKitaplar.Size = new System.Drawing.Size(93, 20);
            this.enIyiKitaplar.Text = "En İyi Kitaplar ";
            this.enIyiKitaplar.Click += new System.EventHandler(this.enIyiKitaplar_Click);
            // 
            // yeniEklenenKitaplar
            // 
            this.yeniEklenenKitaplar.Name = "yeniEklenenKitaplar";
            this.yeniEklenenKitaplar.Size = new System.Drawing.Size(128, 20);
            this.yeniEklenenKitaplar.Text = "Yeni Eklenen Kitaplar";
            this.yeniEklenenKitaplar.Click += new System.EventHandler(this.yeniEklenenKitaplar_Click);
            // 
            // çıkışToolStripMenuItem
            // 
            this.çıkışToolStripMenuItem.Name = "çıkışToolStripMenuItem";
            this.çıkışToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.çıkışToolStripMenuItem.Text = "Çıkış";
            this.çıkışToolStripMenuItem.Click += new System.EventHandler(this.çıkışToolStripMenuItem_Click);
            // 
            // btnIlk
            // 
            this.btnIlk.Location = new System.Drawing.Point(18, 13);
            this.btnIlk.Name = "btnIlk";
            this.btnIlk.Size = new System.Drawing.Size(50, 20);
            this.btnIlk.TabIndex = 16;
            this.btnIlk.Text = "<<";
            this.btnIlk.UseVisualStyleBackColor = true;
            this.btnIlk.Click += new System.EventHandler(this.btnIlk_Click);
            // 
            // btnSon
            // 
            this.btnSon.Location = new System.Drawing.Point(248, 13);
            this.btnSon.Name = "btnSon";
            this.btnSon.Size = new System.Drawing.Size(50, 20);
            this.btnSon.TabIndex = 17;
            this.btnSon.Text = ">>";
            this.btnSon.UseVisualStyleBackColor = true;
            this.btnSon.Click += new System.EventHandler(this.btnSon_Click);
            // 
            // panelBtn
            // 
            this.panelBtn.Controls.Add(this.btnIleri);
            this.panelBtn.Controls.Add(this.btnSon);
            this.panelBtn.Controls.Add(this.btnGeri);
            this.panelBtn.Controls.Add(this.btnIlk);
            this.panelBtn.Controls.Add(this.lblSayfa);
            this.panelBtn.Location = new System.Drawing.Point(317, 500);
            this.panelBtn.Name = "panelBtn";
            this.panelBtn.Size = new System.Drawing.Size(311, 48);
            this.panelBtn.TabIndex = 18;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Checked = false;
            this.dateTimePicker1.Location = new System.Drawing.Point(770, 12);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(156, 20);
            this.dateTimePicker1.TabIndex = 19;
            // 
            // btnOy
            // 
            this.btnOy.Location = new System.Drawing.Point(785, 509);
            this.btnOy.Name = "btnOy";
            this.btnOy.Size = new System.Drawing.Size(85, 23);
            this.btnOy.TabIndex = 20;
            this.btnOy.Text = "Oylamayı Bitir";
            this.btnOy.UseVisualStyleBackColor = true;
            this.btnOy.Click += new System.EventHandler(this.btnOy_Click);
            // 
            // SecimEkrani
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(938, 560);
            this.Controls.Add(this.btnOy);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.panelBtn);
            this.Controls.Add(this.kategoriler);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MainMenuStrip = this.kategoriler;
            this.MaximizeBox = false;
            this.Name = "SecimEkrani";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kitap Okuma Uygulaması";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.kategoriler.ResumeLayout(false);
            this.kategoriler.PerformLayout();
            this.panelBtn.ResumeLayout(false);
            this.panelBtn.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnGeri;
        private System.Windows.Forms.Button btnIleri;
        private System.Windows.Forms.Label lblSayfa;
        private System.Windows.Forms.MenuStrip kategoriler;
        private System.Windows.Forms.ToolStripMenuItem enPopülerKitaplarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enIyiKitaplar;
        private System.Windows.Forms.Button btnIlk;
        private System.Windows.Forms.Button btnSon;
        private System.Windows.Forms.ToolStripMenuItem tümKitaplarToolStripMenuItem;
        private System.Windows.Forms.Panel panelBtn;
        private System.Windows.Forms.ToolStripMenuItem yeniEklenenKitaplar;
        private System.Windows.Forms.ToolStripMenuItem çıkışToolStripMenuItem;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button btnOy;
        private System.Windows.Forms.ToolStripMenuItem kitapÖnerileriToolStripMenuItem;
    }
}

