using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using MySql.Data.MySqlClient;

namespace Yazlab
{
    public partial class SecimEkrani : Form
    {
        MySqlConnection mysqlbaglan;
        string[,] array;
        string[,] popKit;//en popüler kitaplar
        string[,] enKit;//en iyi kitaplar
        string[,] önKit;//önerilen kitaplar
        string[,] yeni;//yeni eklenen kitaplar
        public int newUser = -1;
        public string newName;
        public string newPass;
        public string newLocation;
        public string newAge;
        int rowCount;
        int sayfa = 0;
        int yeniUser = 0;
        int dnm = 0;
        int fark = 0;
        int boyut = 0;
        public int kitapSayfa = 1;
        public int deneme = 0;//1 ise yeni üye değilse eski
        Panel panel1 = new Panel();
        RichTextBox rtxt;
        ComboBox cx;
        List<kitapOy> kisiList;//kişi bilgileri
        List<kitapOy> oyList;//oy bilgileri
        List<kitapOy> kitapBilgi;//kitap bilgileri
        List<kitapOy> oyVerme = new List<kitapOy>();//Oy verilen kitap bilgileri
        List<kitapOy> önerme;//Kitap önerme verileri
        class kitapOy
        {
            int user_Id;
            string kitap_Id;
            string kitap_Ad;
            string yazar;
            int basim_Yil;
            string yayinEv;
            string image;
            int ortOy;
            int oySay;
            double oyKare;
            int kesisim;
            public int User_Id { get => user_Id; set => user_Id = value; }
            public string Kitap_Id { get => kitap_Id; set => kitap_Id = value; }
            public int Oy { get => ortOy; set => ortOy = value; }
            public int OySay { get => oySay; set => oySay = value; }
            public string Kitap_Ad { get => kitap_Ad; set => kitap_Ad = value; }
            public int Basim_Yil { get => basim_Yil; set => basim_Yil = value; }
            public string Image { get => image; set => image = value; }
            public string YayinEv { get => yayinEv; set => yayinEv = value; }
            public string Yazar { get => yazar; set => yazar = value; }
            public double OyKare { get => oyKare; set => oyKare = value; }
            public int Kesisim { get => kesisim; set => kesisim = value; }
        }

        public SecimEkrani()
        {
            mySqlBaglantisi();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            kitapRate();
            if (deneme == 1)//Yeni kayıtsa 1 olur yeni üyedir 10 kitap oylaması gerekir..
            {
                MessageBox.Show("Lütfen en az 10 kitap oylayınız.");
                kategoriler.Hide();
                refreshList(2);
            }
            else
            {
                btnOy.Hide();
                refreshList(2);
            }

        }

        public void mySqlBaglantisi()//Sadece mysqlbaglanı veritabanına bağlar..
        {
            //mysql bağlan
            string sIp = "127.0.0.1"; // Sunucu IP Adresi
            string sDB = "books"; // Veritabanı
            string sKA = "root"; // Kullanıcı Adı
            string sSifre = "root"; // Şifre
            mysqlbaglan = new MySqlConnection("Server=" + sIp + ";Database=" + sDB + ";Uid=" + sKA + ";Pwd='" + sSifre + "';SslMode=none");
            mysqlbaglan.Open();
            if (mysqlbaglan.State != ConnectionState.Closed)
            {
                Console.WriteLine("Bağlantı var");
            }
            else
            {
                Console.WriteLine("Bağlantı yok");
            }
            mysqlbaglan.Close();
            //mysql kapat
        }

        public void vbRead()//Veritabanından kitap bilgilerini çekmek için sql sorguları yapılır..
        {
            mysqlbaglan.Open();
            string sql = "SELECT * FROM `bx-books` LIMIT " + (sayfa * 16) + ", 16";
            MySqlCommand cmd = new MySqlCommand(sql, mysqlbaglan);
            cmd.ExecuteNonQuery();
            cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, mysqlbaglan);
            var vR = cmd.ExecuteReader();

            int count = vR.FieldCount;
            int j = 0;
            array = new String[16, 9];
            while (vR.Read())
            {

                for (int i = 0; i < count; i++)
                {
                    array[j, i] = vR.GetValue(i).ToString();
                }
                j++;
            }
            vR.Close();

            sql = "SELECT COUNT(*) FROM `bx-books`";
            cmd = new MySqlCommand(sql, mysqlbaglan);
            cmd.ExecuteNonQuery();
            cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, mysqlbaglan);
            vR = cmd.ExecuteReader();
            while (vR.Read())
            {
                rowCount = Int32.Parse(vR.GetValue(0).ToString());
            }
            rowCount++;
            mysqlbaglan.Close();
        }

        void kitapResim()//Veritabanından alınan kitap bilgilerini arayüzde gösterir..
        {
            panel1.Hide();
            panel1 = new Panel
            {
                MinimumSize = new Size(1000, 460),
                Location = new Point(10, 35),

            };

            this.Controls.Add(panel1);
            int sizeX = 100, sizeY = 160;//860-460
            int i = 0;
            for (int x = 25; x + sizeX + 10 < 1000; x += sizeX + 10)
            {
                for (int y = 15; y + sizeY + 10 < 460; y += sizeY + 60)
                {
                    if (array[i, 1] != null)
                    {
                        PictureBox resim = new PictureBox
                        {
                            SizeMode = PictureBoxSizeMode.AutoSize,
                            Location = new Point(x, y),
                            MaximumSize = new Size(100, 155),
                            ImageLocation = array[i, 6],
                        };
                        Label kitapAdi = new Label
                        {
                            Location = new Point(x, y + sizeY),
                            MaximumSize = new Size(105, 40),
                            Text = array[i, 1],
                            AutoSize = true,
                        };
                        panel1.Controls.Add(kitapAdi);
                        kitapAdi.Click += new System.EventHandler(lb_Click);
                        panel1.Controls.Add(resim);
                        i++;
                    }
                    else
                        break;

                }
            }
        }

        void kitapOku()//Pdften kitap açar..
        {
            string str = string.Empty;
            string fileName = "C://Users//PC//Desktop//Yazlab//ataturkilke.pdf";
            try//Kemal Tahir - Esir Şehir Üçlemesi #3 - Yol Ayrımı - İthaki Yayınları - 2016
            {
                PdfReader oku = new PdfReader(fileName);
                if (kitapSayfa <= oku.NumberOfPages && kitapSayfa >= 1)
                {
                    ITextExtractionStrategy its = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();
                    String s = PdfTextExtractor.GetTextFromPage(oku, kitapSayfa, its);
                    s = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(s)));
                    str += s;
                    rtxt.Text = str;
                }
                oku.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void kitapRate()//Book-ratingsten veri çeker..
        {
            mysqlbaglan.Open();
            //bx-book-ratings ısbnye göre artan şekilde okur.
            //Kitaplara verilen ort oyu bulur bu sayede en iyi kitaplar bulunur.
            //Bir kitap verilen oy sayılarınıda bir değişkende toplar bu sayede de en popüler kitaplar bulunur.
            string sql = "SELECT * FROM `bx-book-ratings` ORDER BY `bx-book-ratings`.`ISBN` ASC";
            MySqlCommand cmd = new MySqlCommand(sql, mysqlbaglan);
            cmd.ExecuteNonQuery();
            cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, mysqlbaglan);
            var vR = cmd.ExecuteReader();
            int oytop = 0;//1 kitaba verilen oyların toplamı
            int oysay = 0;//1 kitaba verilen oyların sayısı
            string temp = string.Empty;//kitapıdler tutuluyor
            int id = 0;
            oyList = new List<kitapOy>();
            kisiList = new List<kitapOy>();
            while (vR.Read())
            {
                kitapOy deneme = new kitapOy();
                //Yeni gelen kitap bir önceki ile aynı mı aynı ise oyları toplanır
                if (0 == String.Compare(vR.GetValue(1).ToString(), temp))
                {
                    oytop += Int32.Parse(vR.GetValue(2).ToString());
                    oysay++;
                }
                else if (1 == String.Compare(vR.GetValue(1).ToString(), temp) && oysay != 0)
                {
                    deneme.Kitap_Id = temp;
                    deneme.Oy = oytop / oysay;
                    deneme.OySay = oysay;
                    oyList.Add(deneme);
                    oysay = 0;
                    oytop = 0;
                    oytop += Int32.Parse(vR.GetValue(2).ToString());
                    oysay++;
                }
                temp = vR.GetValue(1).ToString();
                kitapOy deneme2 = new kitapOy();
                if (id == Int32.Parse(vR.GetValue(0).ToString()))
                {
                    deneme2.User_Id = Int32.Parse(vR.GetValue(0).ToString());
                    deneme2.Kitap_Id = vR.GetValue(1).ToString();
                    deneme2.Oy = Int32.Parse(vR.GetValue(2).ToString());
                    deneme2.OyKare += Math.Pow(Int32.Parse(vR.GetValue(2).ToString()), 2);
                    kisiList.Add(deneme2);
                }
                else
                {
                    id = Int32.Parse(vR.GetValue(0).ToString());
                    deneme2.User_Id = Int32.Parse(vR.GetValue(0).ToString());
                    deneme2.Kitap_Id = vR.GetValue(1).ToString();
                    deneme2.Oy = Int32.Parse(vR.GetValue(2).ToString());
                    deneme2.OyKare = Math.Pow(Int32.Parse(vR.GetValue(2).ToString()), 2);
                    kisiList.Add(deneme2);
                }
                if (newUser == Int32.Parse(vR.GetValue(0).ToString()))
                {
                    kitapOy user = new kitapOy();
                    user.Oy = Int32.Parse(vR.GetValue(2).ToString());
                    user.Kitap_Id = vR.GetValue(1).ToString();
                    user.OyKare = Math.Pow(user.Oy, 2);
                    oyVerme.Add(user);
                }
            }
            vR.Close();

            //bx-bookstaki tüm kitapların bilgilerini alma işlemi
            string sql3 = "SELECT * FROM `bx-books`";
            cmd = new MySqlCommand(sql3, mysqlbaglan);
            cmd.ExecuteNonQuery();
            cmd = new MySql.Data.MySqlClient.MySqlCommand(sql3, mysqlbaglan);
            vR = cmd.ExecuteReader();
            kitapBilgi = new List<kitapOy>();
            while (vR.Read())
            {
                kitapOy deneme = new kitapOy();
                deneme.Kitap_Id = vR.GetValue(0).ToString();
                deneme.Kitap_Ad = vR.GetValue(1).ToString();
                deneme.Yazar = vR.GetValue(2).ToString();
                deneme.Basim_Yil = Int32.Parse(vR.GetValue(3).ToString());
                deneme.YayinEv = vR.GetValue(4).ToString();
                deneme.Image = vR.GetValue(6).ToString();
                kitapBilgi.Add(deneme);
            }
            vR.Close();
            mysqlbaglan.Close();

            //En çok oylanan kitapların bulunma işlemi
            oyList = oyList.OrderByDescending(kitapOy => kitapOy.OySay).ToList();
            int aa = 0;
            popKit = new string[8, 10];
            //[bilgileri,kitap];
            foreach (kitapOy tmp in oyList)
            {
                if (aa == 10)
                    break;
                foreach (kitapOy tmp2 in kitapBilgi)
                {
                    if (0 == String.Compare(tmp.Kitap_Id, tmp2.Kitap_Id))
                    {
                        popKit[0, aa] = tmp2.Image;
                        popKit[1, aa] = tmp2.Kitap_Ad;
                        popKit[2, aa] = tmp2.Kitap_Id;
                        popKit[3, aa] = tmp2.Yazar;
                        popKit[4, aa] = tmp2.YayinEv;
                        popKit[5, aa] = "" + tmp2.Basim_Yil;
                        popKit[6, aa] = "" + tmp.Oy;
                        popKit[7, aa] = "" + tmp.OySay;
                        aa++;
                    }
                }
            }

            //En iyi ort oya sahip kitaplar
            aa = 0;
            enKit = new string[8, 10];
            oyList = oyList.OrderByDescending(kitapOy => kitapOy.Oy).ToList();
            foreach (kitapOy tmp in oyList)
            {
                if (aa == 10)
                    break;
                foreach (kitapOy tmp2 in kitapBilgi)
                {
                    if (0 == String.Compare(tmp.Kitap_Id, tmp2.Kitap_Id))
                    {
                        enKit[0, aa] = tmp2.Image;
                        enKit[1, aa] = tmp2.Kitap_Ad;
                        enKit[2, aa] = tmp2.Kitap_Id;
                        enKit[3, aa] = tmp2.Yazar;
                        enKit[4, aa] = tmp2.YayinEv;
                        enKit[5, aa] = "" + tmp2.Basim_Yil;
                        enKit[6, aa] = "" + tmp.Oy;
                        enKit[7, aa] = "" + tmp.OySay;
                        aa++;
                    }
                }
            }
        }

        void lb_Click(object sender, EventArgs e)//İsmine tıklanan kitabı açar..
        {
            Label lb = sender as Label;
            okuForm of = new okuForm();
            for (int i = 0; i < 16; i++)
            {
                if (array[i, 1] == lb.Text && fark == 0)
                {
                    dnm = i;
                    fark = 0;
                    of.Size = new Size(830, 700);
                    PictureBox pc = new PictureBox
                    {
                        Name = "pc1",
                        SizeMode = PictureBoxSizeMode.AutoSize,
                        Location = new Point(650, 10),
                        MaximumSize = new Size(150, 155),
                        ImageLocation = array[i, 6],
                    };
                    Label kitapAdi = new Label
                    {
                        Name = "label1",
                        Location = new Point(620, 180),
                        MaximumSize = new Size(150, 40),
                        AutoSize = true,
                        Text = "Kitap Adı:  " + lb.Text,
                    };
                    kitapAdi.Font = new Font("Arial", 10, FontStyle.Bold);
                    Label basimYili = new Label
                    {
                        Name = "label2",
                        Location = new Point(620, 225),
                        AutoSize = true,
                        Text = "Basım Yılı:  " + array[i, 3],
                    };
                    basimYili.Font = new Font("Arial", 10, FontStyle.Bold);
                    Label yazar = new Label
                    {
                        Name = "label3",
                        Location = new Point(620, 250),
                        AutoSize = true,
                        MaximumSize = new Size(200, 40),
                        Text = "Yazar :  " + array[i, 4],
                    };
                    yazar.Font = new Font("Arial", 10, FontStyle.Bold);
                    Label yayinci = new Label
                    {
                        Name = "label4",
                        Location = new Point(620, 295),
                        AutoSize = true,
                        MaximumSize = new Size(200, 40),
                        Text = "Yayınevi :  " + array[i, 4],
                    };
                    yayinci.Font = new Font("Arial", 10, FontStyle.Bold);
                    Label rate = new Label
                    {
                        Name = "label5",
                        Location = new Point(620, 335),
                        AutoSize = true,
                        Text = "Kitabı Oylayınız :  ",
                    };
                    rate.Font = new Font("Arial", 10, FontStyle.Bold);
                    cx = new ComboBox
                    {
                        Name = "cbx",
                        Location = new Point(750, 335),
                        MaximumSize = new Size(40, 15),
                        Text = "0",
                    };
                    ArrayList row = new ArrayList { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", };
                    cx.Items.AddRange(row.ToArray());
                    Button oyVer = new Button
                    {
                        Location = new Point(620, 360),
                        Size = new Size(100, 15),
                        AutoSize = true,
                        Text = "Oy Ver",
                    };
                    oyVer.Font = new Font("Arial", 10, FontStyle.Bold);
                    rtxt = new RichTextBox
                    {
                        Location = new Point(10, 10),
                        Size = new Size(600, 600),
                    };
                    Button btnNext = new Button
                    {
                        Location = new Point(540, 620),
                        AutoSize = true,
                        Text = ">",
                    };
                    Button btnBack = new Button
                    {
                        Location = new Point(10, 620),
                        AutoSize = true,
                        Text = "<",
                    };

                    oyVer.Click += new System.EventHandler(btnOyver_Click);
                    btnNext.Click += new System.EventHandler(btnNext_Click);
                    btnBack.Click += new System.EventHandler(btnBack_Click);
                    of.Controls.Add(pc);
                    of.Controls.Add(kitapAdi);
                    of.Controls.Add(basimYili);
                    of.Controls.Add(yazar);
                    of.Controls.Add(yayinci);
                    of.Controls.Add(rate);
                    of.Controls.Add(cx);
                    of.Controls.Add(oyVer);
                    of.Controls.Add(btnNext);
                    of.Controls.Add(btnBack);
                    of.Controls.Add(rtxt);
                    of.Show();
                    kitapOku();
                }

                else if (i < 10 && fark < 3)
                {
                    if (popKit[1, i] == lb.Text && fark == 1)
                    {
                        dnm = i;
                        of.Size = new Size(845, 700);
                        PictureBox pc = new PictureBox
                        {
                            Name = "pc1",
                            SizeMode = PictureBoxSizeMode.AutoSize,
                            Location = new Point(650, 10),
                            MaximumSize = new Size(150, 155),
                            ImageLocation = popKit[0, i],
                        };
                        Label kitapAdi = new Label
                        {
                            Name = "label1",
                            Location = new Point(620, 180),
                            MaximumSize = new Size(150, 40),
                            AutoSize = true,
                            Text = "Kitap Adı:  " + lb.Text,
                        };
                        kitapAdi.Font = new Font("Arial", 10, FontStyle.Bold);
                        Label basimYili = new Label
                        {
                            Name = "label2",
                            Location = new Point(620, 225),
                            AutoSize = true,
                            Text = "Basım Yılı:  " + popKit[5, i],
                        };
                        basimYili.Font = new Font("Arial", 10, FontStyle.Bold);
                        Label yazar = new Label
                        {
                            Name = "label3",
                            Location = new Point(620, 250),
                            AutoSize = true,
                            MaximumSize = new Size(200, 40),
                            Text = "Yazar :  " + popKit[3, i],
                        };
                        yazar.Font = new Font("Arial", 10, FontStyle.Bold);
                        Label yayinci = new Label
                        {
                            Name = "label4",
                            Location = new Point(620, 275),
                            AutoSize = true,
                            MaximumSize = new Size(200, 40),
                            Text = "Yayınevi :  " + popKit[4, i],
                        };
                        yayinci.Font = new Font("Arial", 10, FontStyle.Bold);
                        Label oySay = new Label
                        {
                            Name = "label5",
                            Location = new Point(620, 305),
                            AutoSize = true,
                            Text = "Kitabın Aldığı Oy Sayısı :  " + popKit[7, i],
                        };
                        oySay.Font = new Font("Arial", 10, FontStyle.Bold);
                        Label rate = new Label
                        {
                            Name = "label5",
                            Location = new Point(620, 325),
                            AutoSize = true,
                            Text = "Kitabı Oylayınız :  ",
                        };
                        rate.Font = new Font("Arial", 10, FontStyle.Bold);
                        cx = new ComboBox
                        {
                            Name = "cbx",
                            Location = new Point(750, 325),
                            MaximumSize = new Size(40, 15),
                            Text = "0",
                        };
                        ArrayList row = new ArrayList { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", };
                        cx.Items.AddRange(row.ToArray());
                        Button oyVer = new Button
                        {
                            Location = new Point(620, 355),
                            Size = new Size(100, 15),
                            AutoSize = true,
                            Text = "Oy Ver",
                        };
                        oyVer.Font = new Font("Arial", 10, FontStyle.Bold);
                        rtxt = new RichTextBox
                        {
                            Location = new Point(10, 10),
                            Size = new Size(600, 600),
                        };
                        Button btnNext = new Button
                        {
                            Location = new Point(540, 620),
                            AutoSize = true,
                            Text = ">",
                        };
                        Button btnBack = new Button
                        {
                            Location = new Point(10, 620),
                            AutoSize = true,
                            Text = "<",
                        };

                        oyVer.Click += new System.EventHandler(btnOyver_Click);
                        btnNext.Click += new System.EventHandler(btnNext_Click);
                        btnBack.Click += new System.EventHandler(btnBack_Click);
                        of.Controls.Add(pc);
                        of.Controls.Add(kitapAdi);
                        of.Controls.Add(basimYili);
                        of.Controls.Add(yazar);
                        of.Controls.Add(yayinci);
                        of.Controls.Add(oySay);
                        of.Controls.Add(rate);
                        of.Controls.Add(cx);
                        of.Controls.Add(oyVer);
                        of.Controls.Add(btnNext);
                        of.Controls.Add(btnBack);
                        of.Controls.Add(rtxt);
                        of.Show();
                        kitapOku();
                    }

                    else if (enKit[1, i] == lb.Text && fark == 2)
                    {
                        dnm = i;
                        of.Size = new Size(845, 700);
                        PictureBox pc = new PictureBox
                        {
                            Name = "pc1",
                            SizeMode = PictureBoxSizeMode.AutoSize,
                            Location = new Point(650, 10),
                            MaximumSize = new Size(150, 155),
                            ImageLocation = enKit[0, i],
                        };
                        Label kitapAdi = new Label
                        {
                            Name = "label1",
                            Location = new Point(620, 180),
                            MaximumSize = new Size(150, 40),
                            AutoSize = true,
                            Text = "Kitap Adı:  " + lb.Text,
                        };
                        kitapAdi.Font = new Font("Arial", 10, FontStyle.Bold);
                        Label basimYili = new Label
                        {
                            Name = "label2",
                            Location = new Point(620, 225),
                            AutoSize = true,
                            Text = "Basım Yılı:  " + enKit[5, i],
                        };
                        basimYili.Font = new Font("Arial", 10, FontStyle.Bold);
                        Label yazar = new Label
                        {
                            Name = "label3",
                            Location = new Point(620, 250),
                            AutoSize = true,
                            MaximumSize = new Size(200, 40),
                            Text = "Yazar :  " + enKit[3, i],
                        };
                        yazar.Font = new Font("Arial", 10, FontStyle.Bold);
                        Label yayinci = new Label
                        {
                            Name = "label4",
                            Location = new Point(620, 280),
                            AutoSize = true,
                            MaximumSize = new Size(200, 40),
                            Text = "Yayınevi :  " + enKit[4, i],
                        };
                        yayinci.Font = new Font("Arial", 10, FontStyle.Bold);
                        Label oySay = new Label
                        {
                            Name = "label5",
                            Location = new Point(620, 320),
                            AutoSize = true,
                            Text = "Kitabın Aldığı Oy Sayısı :  " + enKit[7, i],
                        };
                        oySay.Font = new Font("Arial", 10, FontStyle.Bold);
                        Label ortOy = new Label
                        {
                            Name = "label5",
                            Location = new Point(620, 335),
                            AutoSize = true,
                            Text = "Kitabın Aldığı Oy :  " + enKit[6, i],
                        };
                        ortOy.Font = new Font("Arial", 10, FontStyle.Bold);
                        Label rate = new Label
                        {
                            Name = "label5",
                            Location = new Point(620, 355),
                            AutoSize = true,
                            Text = "Kitabı Oylayınız :  ",
                        };
                        rate.Font = new Font("Arial", 10, FontStyle.Bold);
                        cx = new ComboBox
                        {
                            Name = "cbx",
                            Location = new Point(750, 355),
                            MaximumSize = new Size(40, 15),
                            Text = "0",
                        };
                        ArrayList row = new ArrayList { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", };
                        cx.Items.AddRange(row.ToArray());
                        Button oyVer = new Button
                        {
                            Location = new Point(620, 385),
                            Size = new Size(100, 15),
                            AutoSize = true,
                            Text = "Oy Ver",
                        };
                        oyVer.Font = new Font("Arial", 10, FontStyle.Bold);
                        rtxt = new RichTextBox
                        {
                            Location = new Point(10, 10),
                            Size = new Size(600, 600),
                        };
                        Button btnNext = new Button
                        {
                            Location = new Point(540, 620),
                            AutoSize = true,
                            Text = ">",
                        };
                        Button btnBack = new Button
                        {
                            Location = new Point(10, 620),
                            AutoSize = true,
                            Text = "<",
                        };

                        oyVer.Click += new System.EventHandler(btnOyver_Click);
                        btnNext.Click += new System.EventHandler(btnNext_Click);
                        btnBack.Click += new System.EventHandler(btnBack_Click);
                        of.Controls.Add(pc);
                        of.Controls.Add(kitapAdi);
                        of.Controls.Add(basimYili);
                        of.Controls.Add(yazar);
                        of.Controls.Add(yayinci);
                        of.Controls.Add(oySay);
                        of.Controls.Add(ortOy);
                        of.Controls.Add(rate);
                        of.Controls.Add(cx);
                        of.Controls.Add(oyVer);
                        of.Controls.Add(btnNext);
                        of.Controls.Add(btnBack);
                        of.Controls.Add(rtxt);
                        of.Show();
                        kitapOku();
                    }
                }

                else if (i < boyut && fark == 3)
                {
                    if (önKit[i, 1] == lb.Text && fark == 3)
                    {

                        dnm = i;
                        of.Size = new Size(830, 700);
                        PictureBox pc = new PictureBox
                        {
                            Name = "pc1",
                            SizeMode = PictureBoxSizeMode.AutoSize,
                            Location = new Point(650, 10),
                            MaximumSize = new Size(150, 155),
                            ImageLocation = önKit[i, 2],
                        };
                        Label kitapAdi = new Label
                        {
                            Name = "label1",
                            Location = new Point(620, 180),
                            MaximumSize = new Size(150, 40),
                            AutoSize = true,
                            Text = "Kitap Adı:  " + lb.Text,
                        };
                        kitapAdi.Font = new Font("Arial", 10, FontStyle.Bold);
                        Label basimYili = new Label
                        {
                            Name = "label2",
                            Location = new Point(620, 225),
                            AutoSize = true,
                            Text = "Basım Yılı:  " + önKit[i, 4],
                        };
                        basimYili.Font = new Font("Arial", 10, FontStyle.Bold);
                        Label yazar = new Label
                        {
                            Name = "label3",
                            Location = new Point(620, 250),
                            AutoSize = true,
                            MaximumSize = new Size(200, 40),
                            Text = "Yazar :  " + önKit[i, 3],
                        };
                        yazar.Font = new Font("Arial", 10, FontStyle.Bold);
                        Label yayinci = new Label
                        {
                            Name = "label4",
                            Location = new Point(620, 295),
                            AutoSize = true,
                            MaximumSize = new Size(200, 40),
                            Text = "Yayınevi :  " + önKit[i, 5],
                        };
                        yayinci.Font = new Font("Arial", 10, FontStyle.Bold);
                        Label rate = new Label
                        {
                            Name = "label5",
                            Location = new Point(620, 335),
                            AutoSize = true,
                            Text = "Kitabı Oylayınız :  ",
                        };
                        rate.Font = new Font("Arial", 10, FontStyle.Bold);
                        cx = new ComboBox
                        {
                            Name = "cbx",
                            Location = new Point(750, 335),
                            MaximumSize = new Size(40, 15),
                            Text = "0",
                        };
                        ArrayList row = new ArrayList { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", };
                        cx.Items.AddRange(row.ToArray());
                        Button oyVer = new Button
                        {
                            Location = new Point(620, 360),
                            Size = new Size(100, 15),
                            AutoSize = true,
                            Text = "Oy Ver",
                        };
                        oyVer.Font = new Font("Arial", 10, FontStyle.Bold);
                        rtxt = new RichTextBox
                        {
                            Location = new Point(10, 10),
                            Size = new Size(600, 600),
                        };
                        Button btnNext = new Button
                        {
                            Location = new Point(540, 620),
                            AutoSize = true,
                            Text = ">",
                        };
                        Button btnBack = new Button
                        {
                            Location = new Point(10, 620),
                            AutoSize = true,
                            Text = "<",
                        };

                        oyVer.Click += new System.EventHandler(btnOyver_Click);
                        btnNext.Click += new System.EventHandler(btnNext_Click);
                        btnBack.Click += new System.EventHandler(btnBack_Click);
                        of.Controls.Add(pc);
                        of.Controls.Add(kitapAdi);
                        of.Controls.Add(basimYili);
                        of.Controls.Add(yazar);
                        of.Controls.Add(yayinci);
                        of.Controls.Add(rate);
                        of.Controls.Add(cx);
                        of.Controls.Add(oyVer);
                        of.Controls.Add(btnNext);
                        of.Controls.Add(btnBack);
                        of.Controls.Add(rtxt);
                        of.Show();
                        kitapOku();

                    }
                }

                else if (i < 5 && fark == 4)
                {
                    if (yeni[i, 1] == lb.Text && fark == 4)
                    {
                        dnm = i;
                        of.Size = new Size(830, 700);
                        PictureBox pc = new PictureBox
                        {
                            Name = "pc1",
                            SizeMode = PictureBoxSizeMode.AutoSize,
                            Location = new Point(650, 10),
                            MaximumSize = new Size(150, 155),
                            ImageLocation = yeni[i, 6],
                        };
                        Label kitapAdi = new Label
                        {
                            Name = "label1",
                            Location = new Point(620, 180),
                            MaximumSize = new Size(150, 40),
                            AutoSize = true,
                            Text = "Kitap Adı:  " + lb.Text,
                        };
                        kitapAdi.Font = new Font("Arial", 10, FontStyle.Bold);
                        Label basimYili = new Label
                        {
                            Name = "label2",
                            Location = new Point(620, 225),
                            AutoSize = true,
                            Text = "Basım Yılı:  " + yeni[i, 3],
                        };
                        basimYili.Font = new Font("Arial", 10, FontStyle.Bold);
                        Label yazar = new Label
                        {
                            Name = "label3",
                            Location = new Point(620, 250),
                            AutoSize = true,
                            MaximumSize = new Size(200, 40),
                            Text = "Yazar :  " + yeni[i, 2],
                        };
                        yazar.Font = new Font("Arial", 10, FontStyle.Bold);
                        Label yayinci = new Label
                        {
                            Name = "label4",
                            Location = new Point(620, 295),
                            AutoSize = true,
                            MaximumSize = new Size(200, 40),
                            Text = "Yayınevi :  " + yeni[i, 4],
                        };
                        yayinci.Font = new Font("Arial", 10, FontStyle.Bold);
                        Label rate = new Label
                        {
                            Name = "label5",
                            Location = new Point(620, 335),
                            AutoSize = true,
                            Text = "Kitabı Oylayınız :  ",
                        };
                        rate.Font = new Font("Arial", 10, FontStyle.Bold);
                        cx = new ComboBox
                        {
                            Name = "cbx",
                            Location = new Point(750, 335),
                            MaximumSize = new Size(40, 15),
                            Text = "0",
                        };
                        ArrayList row = new ArrayList { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", };
                        cx.Items.AddRange(row.ToArray());
                        Button oyVer = new Button
                        {
                            Location = new Point(620, 360),
                            Size = new Size(100, 15),
                            AutoSize = true,
                            Text = "Oy Ver",
                        };
                        oyVer.Font = new Font("Arial", 10, FontStyle.Bold);
                        rtxt = new RichTextBox
                        {
                            Location = new Point(10, 10),
                            Size = new Size(600, 600),
                        };
                        Button btnNext = new Button
                        {
                            Location = new Point(540, 620),
                            AutoSize = true,
                            Text = ">",
                        };
                        Button btnBack = new Button
                        {
                            Location = new Point(10, 620),
                            AutoSize = true,
                            Text = "<",
                        };

                        oyVer.Click += new System.EventHandler(btnOyver_Click);
                        btnNext.Click += new System.EventHandler(btnNext_Click);
                        btnBack.Click += new System.EventHandler(btnBack_Click);
                        of.Controls.Add(pc);
                        of.Controls.Add(kitapAdi);
                        of.Controls.Add(basimYili);
                        of.Controls.Add(yazar);
                        of.Controls.Add(yayinci);
                        of.Controls.Add(rate);
                        of.Controls.Add(cx);
                        of.Controls.Add(oyVer);
                        of.Controls.Add(btnNext);
                        of.Controls.Add(btnBack);
                        of.Controls.Add(rtxt);
                        of.Show();
                        kitapOku();
                    }
                }
            }
        }

        public void refreshList(int islem)//Sayfalandırma işlemleri..
        {
            //Bir sonraki sayfaya geçer..
            if (islem == 1 && sayfa >= 0)
            {
                if (sayfa < (rowCount / 16))
                {
                    sayfa++;
                }
            }
            //ilk sayfaya gönderir..
            else if (islem == 3)
            {
                sayfa = 0;
            }
            //son sayfaya gönderir..
            else if (islem == 4)
            {
                sayfa = (rowCount / 16);
            }
            //Bir önceki sayfaya geçirir..
            else if (islem == -1)
            {
                if (sayfa > 0)
                {
                    sayfa--;
                }
            }

            if (sayfa >= 0)
            {               
                vbRead();
                kitapResim();
                if (sayfa == (rowCount / 16))
                {
                    lblSayfa.Text = (sayfa) + "/" + (rowCount / 16);
                }
                else
                {
                    lblSayfa.Text = (sayfa + 1) + "/" + (rowCount / 16);
                }
            }
        }

        public double cosOnerme()//cosinüs benzerliği..
        {
            kisiList = kisiList.OrderBy(kitapOy => kitapOy.User_Id).ToList();
            önerme = new List<kitapOy>();
            int id = -1;
            double kare = 0;
            double userKare = 0;
            int ortak = 0;
            int kes = 0;
            if (newUser != -1)  
            {
                mysqlbaglan.Open();
                string sql = "SELECT * FROM `bx-book-ratings` WHERE `User-ID` = " + newUser + "";
                MySqlCommand cmd = new MySqlCommand(sql, mysqlbaglan);
                cmd.ExecuteNonQuery();
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, mysqlbaglan);
                var vR = cmd.ExecuteReader();
                while (vR.Read())
                {
                    kitapOy user = new kitapOy();
                    user.Kitap_Id = vR.GetValue(1).ToString();
                    user.Oy = Int32.Parse(vR.GetValue(2).ToString());
                    user.OyKare = Math.Pow(user.Oy, 2);
                    oyVerme.Add(user);
                }
                vR.Close();
                mysqlbaglan.Close();
            }
            
            foreach (kitapOy user in oyVerme)
            {
                foreach (kitapOy kitap in kisiList)
                {
                    //bu kullanıcıyla hiç ortak oyladıkları kitap yok
                    if (id != kitap.User_Id && kes == 0)
                    {
                        kare = 0;
                        ortak = 0;
                    }
                    //Bu kullanıcıyla en az 1 tane ortak var
                    else if (id != kitap.User_Id && kes == 1)
                    {
                        kitapOy öneri = new kitapOy();
                        öneri.User_Id = id;
                        öneri.OyKare = kare;
                        öneri.Kesisim = ortak;
                        kare = 0;
                        kes = 0;
                        ortak = 0;
                        önerme.Add(öneri);
                    }
                    kare += kitap.OyKare;
                    if (user.Kitap_Id == kitap.Kitap_Id)
                    {
                        kes = 1;
                        ortak += kitap.Oy * user.Oy;
                    }
                    id = kitap.User_Id;
                }
                userKare += user.OyKare; 
            }
            return userKare;          
        }

        public void onerilenler()//kullanıcı tabanlı işbirlikçi filtreleme yöntemi..
        {
            double userKare = cosOnerme();
            double temp = -1;
            int id = -1;
            int i = 0;
            foreach (kitapOy öneri in önerme)
            {
                double benzerlik = öneri.Kesisim / Math.Sqrt(öneri.OyKare * userKare);
                if (temp < benzerlik)
                {
                    temp = benzerlik;
                    id = öneri.User_Id;
                }
            }
            önerme.Clear();
            foreach (kitapOy kisi in kisiList)
            {
                if (id == kisi.User_Id)
                {
                    foreach (kitapOy kitap in kitapBilgi)
                    {
                        if (kisi.Kitap_Id == kitap.Kitap_Id)
                        {
                            kitapOy öneriKitap = new kitapOy();
                            öneriKitap.OyKare = kisi.Oy * temp;
                            öneriKitap.Kitap_Id = kitap.Kitap_Id;
                            öneriKitap.Image = kitap.Image;
                            öneriKitap.Kitap_Ad = kitap.Kitap_Ad;
                            öneriKitap.YayinEv = kitap.YayinEv;
                            öneriKitap.Yazar = kitap.Yazar;
                            öneriKitap.Basim_Yil = kitap.Basim_Yil;
                            önerme.Add(öneriKitap);
                            i++;
                        }
                    }
                }
            }
            boyut = i;
            önKit = new string[i, 7];
            i = 0;
            önerme = önerme.OrderBy(kitapOy => kitapOy.OyKare).ToList();
            foreach (kitapOy son in önerme)
            {
                önKit[i, 0] = son.Kitap_Id;
                önKit[i, 1] = son.Kitap_Ad;
                önKit[i, 2] = son.Image;
                önKit[i, 3] = son.Yazar;
                önKit[i, 4] = son.Basim_Yil.ToString();
                önKit[i, 5] = son.YayinEv;
                önKit[i, 6] = son.OyKare.ToString();
                Console.WriteLine("Önerilen kitap: " + önKit[i, 1]);
                i++;
            }
        }

        private void kitapÖnerileriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fark = 3;
            onerilenler();

            panel1.Hide();
            panelBtn.Hide();
            panel1 = new Panel
            {
                Name = "panel2",
                MinimumSize = new Size(1000, 460),
                Location = new Point(10, 35),

            };
            this.Controls.Add(panel1);

            int sizeX = 100, sizeY = 160;//860-460
            int i = 0;
            PictureBox picture;
            Label kitapAdi;           
            for (int x = 25; x + sizeX + 10 < 1000; x += sizeX + 10)
            {
                for (int y = 15; y + sizeY + 10 < 460; y += sizeY + 60)
                {
                    if (boyut > i && i < 16) 
                    {

                        picture = new PictureBox
                        {
                            SizeMode = PictureBoxSizeMode.AutoSize,
                            Location = new Point(x, y),
                            MaximumSize = new Size(100, 155),
                            ImageLocation = önKit[i, 2],
                        };
                        panel1.Controls.Add(picture);
                        kitapAdi = new Label
                        {
                            Location = new Point(x, y + sizeY),
                            MaximumSize = new Size(105, 40),
                            AutoSize = true,
                            Text = önKit[i, 1],
                        };
                        panel1.Controls.Add(kitapAdi);
                        kitapAdi.Click += new System.EventHandler(lb_Click);
                        i++;
                    }
                    else
                        break;
                }
            }
        }

        private void btnOyver_Click(object sender, EventArgs e)//Kullanıcıların verdiği oyların dbye gönderilmesi..
        {
            kitapOy user = new kitapOy();
            string isbn = null;
            if (fark == 0)//tüm kitaplar
            {
                isbn = array[dnm, 0];
                Console.WriteLine(array[dnm, 1]);
            }
            else if (fark == 1)//popkit
            {
                isbn = popKit[2, dnm];
            }
            else if (fark == 2)//enkit
            {
                isbn = enKit[2, dnm];
            }
            else if (fark == 3)//önkit
            {
                isbn = önKit[dnm, 0];
            }
            else if (fark == 4)//önkit
            {
                isbn = yeni[dnm, 0];
            }
            Button btn = sender as Button;
            btn.Hide();
            yeniUser++;
            int rate = Convert.ToInt32(cx.Text);
            user.Kitap_Id = isbn;
            user.Oy = rate;
            user.OyKare = Math.Pow(rate, 2);
            oyVerme.Add(user);
            if (deneme == 0)
            {
                mysqlbaglan.Open();
                string sql = "INSERT INTO `bx-book-ratings`(`User-ID`, `ISBN`, `Book-Rating`) VALUES ('" + newUser + "','" + user.Kitap_Id + "','" + user.Oy + "')";
                MySqlCommand cmd = new MySqlCommand(sql, mysqlbaglan);
                cmd.ExecuteNonQuery();
                mysqlbaglan.Close();
            }
        }

        private void btnOy_Click(object sender, EventArgs e)//Kitap oylamaları tamamlıktan sonra kaydı tamamlar..
        {
            if (yeniUser >= 10)  {
                deneme = 0;
                btnOy.Hide();
                kategoriler.Show();
                refreshList(2);
                mysqlbaglan.Open();
                string sql = "INSERT INTO `bx-users`(`User-ID`, `Location`, `Age`, `password`, `user_name`) VALUES (null,'" + newLocation + "','" + newAge + "','" + newPass + "','" + newName + "')";
                MySqlCommand cmd = new MySqlCommand(sql, mysqlbaglan);
                cmd.ExecuteNonQuery();
                sql = "SELECT * FROM `bx-users`ORDER BY `bx-users`.`User-ID`  DESC LIMIT 1";
                cmd = new MySqlCommand(sql, mysqlbaglan);
                cmd.ExecuteNonQuery();
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, mysqlbaglan);
                var vR = cmd.ExecuteReader();
                while (vR.Read())
                {
                    newUser = Int32.Parse(vR.GetValue(0).ToString());
                }
                vR.Close();

                foreach(kitapOy bookRate in oyVerme)
                {                 
                    sql = "INSERT INTO `bx-book-ratings`(`User-ID`, `ISBN`, `Book-Rating`) VALUES ('" + newUser + "','" + bookRate.Kitap_Id + "','" + bookRate.Oy + "')";
                    cmd = new MySqlCommand(sql, mysqlbaglan);
                    cmd.ExecuteNonQuery();
                }
                mysqlbaglan.Close();
            }
        }

        private void tümKitaplarToolStripMenuItem_Click(object sender, EventArgs e)//Tüm kitaplar tekrar açılır..
        {
            fark = 0;
            refreshList(2);
            panelBtn.Show();
        }

        private void enPopülerKitaplarToolStripMenuItem_Click(object sender, EventArgs e)//En çok oy alan kitaplar..
        {
            fark = 1;
            panel1.Hide();
            panelBtn.Hide();
            panel1 = new Panel
            {
                Name = "panel2",
                MinimumSize = new Size(1000, 460),
                Location = new Point(10, 35),

            };
            this.Controls.Add(panel1);

            int sizeX = 100, sizeY = 160;//860-460
            int i = 0;
            PictureBox picture;
            Label kitapAdi;
            for (int x = 100; x + sizeX + 10 < 1000; x += sizeX + 50)
            {
                for (int y = 15; y + sizeY + 10 < 460; y += sizeY + 60)
                {
                    if (i < 10)
                    {
                        picture = new PictureBox
                        {
                            Name = "pictureBox",
                            SizeMode = PictureBoxSizeMode.AutoSize,
                            Location = new Point(x, y),
                            MaximumSize = new Size(100, 155),
                            ImageLocation = popKit[0, i],
                        };
                        panel1.Controls.Add(picture);
                        kitapAdi = new Label
                        {
                            Name = "label1",
                            Location = new Point(x, y + sizeY),
                            MaximumSize = new Size(105, 40),
                            AutoSize = true,
                            Text = popKit[1, i],
                        };
                        panel1.Controls.Add(kitapAdi);
                        kitapAdi.Click += new System.EventHandler(lb_Click);
                    }
                    else
                    {
                        break;
                    }
                    i++;
                }
            } 
        }

        private void enIyiKitaplar_Click(object sender, EventArgs e)//En yüksek ortalama oya sahip kitaplar gösterilir..
        {
            fark = 2;
            panel1.Hide();
            panelBtn.Hide();
            panel1 = new Panel
            {
                Name = "panel2",
                MinimumSize = new Size(1000, 460),
                Location = new Point(10, 35),

            };
            this.Controls.Add(panel1);

            int sizeX = 100, sizeY = 160;//860-460
            int i = 0;
            for (int x = 100; x + sizeX + 10 < 1000; x += sizeX + 50)
            {
                for (int y = 15; y + sizeY + 10 < 460; y += sizeY + 60)
                {
                    if (i < 10)
                    {
                        PictureBox picture = new PictureBox
                        {
                            Name = "pictureBox",
                            SizeMode = PictureBoxSizeMode.AutoSize,
                            Location = new Point(x, y),
                            MaximumSize = new Size(100, 155),
                            ImageLocation = enKit[0, i],
                        };
                        panel1.Controls.Add(picture);
                        Label kitapAdi = new Label
                        {
                            Name = "label1",
                            Location = new Point(x, y + sizeY),
                            MaximumSize = new Size(105, 40),
                            Text = enKit[1, i],
                            AutoSize = true,
                        };
                        panel1.Controls.Add(kitapAdi);
                        kitapAdi.Click += new System.EventHandler(lb_Click);
                    }
                    else
                    {
                        break;
                    }
                    i++;
                }
            }
        }

        private void yeniEklenenKitaplar_Click(object sender, EventArgs e)//Adminin eklediği son 5 kitap..
        {
            fark = 4;
            mysqlbaglan.Open();
            string sql = "SELECT * FROM `bx-books` WHERE `new_book`=1";
            MySqlCommand cmd = new MySqlCommand(sql, mysqlbaglan);
            cmd.ExecuteNonQuery();
            cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, mysqlbaglan);
            var vR = cmd.ExecuteReader();
            yeni = new string[5, 9];
            int index = 0;
            while (vR.Read())
            {                
                if (vR.GetValue(8).ToString().Equals("1")&&index<5)
                {
                    yeni[index, 0] = vR.GetValue(0).ToString();
                    yeni[index, 1] = vR.GetValue(1).ToString();
                    yeni[index, 2] = vR.GetValue(2).ToString();
                    yeni[index, 3] = vR.GetValue(3).ToString();
                    yeni[index, 4] = vR.GetValue(4).ToString();
                    yeni[index, 5] = vR.GetValue(5).ToString();
                    yeni[index, 6] = vR.GetValue(6).ToString();
                    yeni[index, 7] = vR.GetValue(7).ToString();
                    index++;
                }
                else
                {
                    break;
                }
            }
            mysqlbaglan.Close();
            panel1.Hide();
            panelBtn.Hide();
            panel1 = new Panel
            {
                Name = "panel2",
                MinimumSize = new Size(1000, 460),
                Location = new Point(10, 35),

            };
            this.Controls.Add(panel1);
            int sizeX = 100, sizeY = 160;//860-460
            int i = 0;
            for (int x = 100; x + sizeX + 10 < 1000; x += sizeX + 10)
            {
                for (int y = 15; y + sizeY + 10 < 460; y += sizeY + 60)
                {
                    if (i < 5)
                    {
                        PictureBox picture = new PictureBox
                        {
                            Name = "pictureBox",
                            SizeMode = PictureBoxSizeMode.AutoSize,
                            Location = new Point(x, y),
                            MaximumSize = new Size(100, 155),
                            ImageLocation = yeni[i, 6],
                        };
                        panel1.Controls.Add(picture);
                        Label kitapAdi = new Label
                        {
                            Name = "label1",
                            Location = new Point(x, y + sizeY),
                            MaximumSize = new Size(105, 40),
                            Text = yeni[i, 1],
                            AutoSize = true,
                        };
                        panel1.Controls.Add(kitapAdi);
                        kitapAdi.Click += new System.EventHandler(lb_Click);
                    }
                    else
                    {
                        break;
                    }
                    i++;
                }
            }
        }

        private void btnGeri_Click(object sender, EventArgs e)//Seçim ekranında bir ileri sayfaya geçirir..
        {
            refreshList(-1);
        }

        private void btnIleri_Click(object sender, EventArgs e)//Seçim ekranında bir geri sayfaya geçirir..
        {
            refreshList(1);
        }

        private void btnIlk_Click(object sender, EventArgs e)//Seçim ekranında ilk sayfaya geçirir..
        {
            refreshList(3);
        }

        private void btnSon_Click(object sender, EventArgs e)//Seçim ekranında son sayfaya geçirir..
        {
            refreshList(4);
        }

        void btnNext_Click(object sender, EventArgs e)//Kitap okurken ileri sayfaya geçirir..
        {
            kitapSayfa++;
            kitapOku();
        }

        void btnBack_Click(object sender, EventArgs e)//Kitap okurken önceki sayfaya geçirir..
        {
            kitapSayfa--;
            kitapOku();
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}