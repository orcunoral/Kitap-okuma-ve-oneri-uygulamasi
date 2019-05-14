using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yazlab
{
    public partial class AdminSayfası : Form
    {
        MySqlConnection mysqlbaglan;

        int menuItemPosition = 0;
        int rowCount;
        int sayfa = 0;
        Panel panel1 = new Panel();
        string[,] array;

        public AdminSayfası()
        {
            mySqlBaglantisi();
            InitializeComponent();
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

        void kitapResim()//Veritabanından alınan kitap bilgilerini arayüzde gösterir..
        {
            panel1.Hide();
            panel1 = new Panel
            {
                Name = "panel1",
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
                            Name = "pictureBox",
                            SizeMode = PictureBoxSizeMode.AutoSize,
                            Location = new Point(x, y),
                            MaximumSize = new Size(100, 155),
                            ImageLocation = array[i, 6],
                        };
                        Label kitapAdi = new Label
                        {
                            Name = "label1",
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

        void lb_Click(object sender, EventArgs e)//İsmine tıklanan kitabı açar..
        {
            Label lb = sender as Label;

            for (int i = 0; i < 16; i++)
            {
                if (array[i, 1] == lb.Text)
                {
                    DialogResult dialog = MessageBox.Show("Kitabi Silmek Istiyor Musunuz ?",
                        "Exit", MessageBoxButtons.YesNo);

                    if (dialog == DialogResult.Yes)
                    {

                        mysqlbaglan.Open();
                        string sql = "DELETE FROM `bx-books` WHERE `bx-books`.`Book-Title` = '" + (lb.Text) + "'";
                        MySqlCommand cmd = new MySqlCommand(sql, mysqlbaglan);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show(lb.Text + " Silindi !");

                        mysqlbaglan.Close();
                        refreshList(2);


                    }
                    else if (dialog == DialogResult.No)
                    {

                    }


                }
            }




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
            mysqlbaglan.Close();
        }

        private void tümKitaplarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuItemPosition = 0;
            dataGridView1.Hide();
            panelKitapEkle.Hide();
            refreshList(2);
            panelBtn.Show();
        }

        private void AdminSayfası_Load(object sender, EventArgs e)
        {
            dataGridView1.Hide();

            panelKitapEkle.Hide();
            refreshList(2);

        }

        private void btnGeri_Click(object sender, EventArgs e)//Seçim ekranında bir ileri sayfaya geçirir..
        {
            if (menuItemPosition == 0)
            {
                refreshList(-1);
            }
            else
            {
                refreshListUser(-1);

            }
        }

        private void btnIleri_Click(object sender, EventArgs e)//Seçim ekranında bir geri sayfaya geçirir..
        {
            if (menuItemPosition == 0)
            {
                refreshList(1);
            }
            else
            {
                refreshListUser(1);

            }
        }

        private void btnIlk_Click(object sender, EventArgs e)//Seçim ekranında ilk sayfaya geçirir..
        {
            if (menuItemPosition == 0)
            {
                refreshList(3);
            }
            else
            {
                refreshListUser(3);

            }
        }

        private void btnSon_Click(object sender, EventArgs e)//Seçim ekranında son sayfaya geçirir..
        {
            if (menuItemPosition == 0)
            {
                refreshList(4);
            }
            else
            {
                refreshListUser(4);

            }
        }

        private void kitapEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Hide();

            panel1.Hide();
            panelKitapEkle.Show();
            panelBtn.Hide();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            mysqlbaglan.Open();


            if (textBox1.Text.Equals("") || textBox2.Text.Equals("") || textBox3.Text.Equals("") || textBox4.Text.Equals("") || textBox5.Text.Equals("") || textBox6.Text.Equals("") || textBox7.Text.Equals("") || textBox8.Text.Equals(""))
            {
                // MessageBox.Show("Butun alanlari doldurunuz...");
                MessageBox.Show("Lutfen butun alanlari doldurunuz...");

            }
            else
            {
                string sql = "INSERT INTO `bx-books` (`ISBN`, `Book-Title`, `Book-Author`, `Year-Of-Publication`, `Publisher`, `Image-URL-S`, `Image-URL-M`, `Image-URL-L`, `new_book`) VALUES ('" +
                    textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text +
                    "', '" + textBox6.Text + "', '" + textBox7.Text + "', '" + textBox8.Text + "','1');";

                MySqlCommand cmd = new MySqlCommand(sql, mysqlbaglan);
                cmd.ExecuteNonQuery();
                MessageBox.Show(("('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text +
                    "', '" + textBox6.Text + "', '" + textBox7.Text + "', '" + textBox8.Text + "');" + " basariyla eklendi."));

                mysqlbaglan.Close();
            }
        }

        private void kullanıcılarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Hide();
            dataGridView1.Show();

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Refresh();
            menuItemPosition = 2;
            panelBtn.Show();

            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            DataGridViewTextBoxColumn one = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn two = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn three = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn four = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn five = new DataGridViewTextBoxColumn();

            button.HeaderText = "Islem";
            button.Name = "buttonSil";
            button.Text = "Sil";
            button.UseColumnTextForButtonValue = true;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AllowUserToAddRows = false;

            one.HeaderText = "User-ID";
            two.HeaderText = "Location";
            three.HeaderText = "Age";
            four.HeaderText = "password";
            five.HeaderText = "user_name";

            dataGridView1.Columns.Add(one);
            dataGridView1.Columns.Add(two);
            dataGridView1.Columns.Add(three);
            dataGridView1.Columns.Add(four);
            dataGridView1.Columns.Add(five);
            dataGridView1.Columns.Add(button);

            mysqlbaglan.Open();

            string sql = "SELECT COUNT(*) FROM `bx-users`";
            MySqlCommand cmd = new MySqlCommand(sql, mysqlbaglan);
            cmd.ExecuteNonQuery();
            cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, mysqlbaglan);
            var dr = cmd.ExecuteReader();

            int count = dr.FieldCount;
            while (dr.Read())
            {
                rowCount = Int32.Parse(dr.GetValue(0).ToString());
                Console.WriteLine(rowCount);
            }

            dr.Close();
            mysqlbaglan.Close();
            refreshListUser(2);



        }

        public void refreshListUser(int islem)
        {
            if (islem == 1 && sayfa >= 0)
            {
                if (sayfa < (rowCount / 25))
                {
                    sayfa++;
                }
            }
            else if (islem == 2)
            {

            }
            else if (islem == 3)
            {
                sayfa = 0;
            }
            else if (islem == 4)
            {
                sayfa = rowCount / 25;
            }
            else
            {
                if (sayfa > 0)
                {
                    sayfa--;
                }
            }

            if (sayfa >= 0)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();



                mysqlbaglan.Open();


                string sql = "SELECT * FROM `bx-users` LIMIT " + (sayfa * 25) + ", 25";
                MySqlCommand cmd = new MySqlCommand(sql, mysqlbaglan);
                cmd.ExecuteNonQuery();

                string roboIp = "";

                cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, mysqlbaglan);
                var dr = cmd.ExecuteReader();
                // Now check if any rows returned.

                int count = dr.FieldCount;
                while (dr.Read())
                {
                    String[] array = new String[9];
                    for (int i = 0; i < count; i++)
                    {
                        Console.WriteLine(dr.GetValue(i));
                        array[i] = dr.GetValue(i).ToString();
                    }
                    dataGridView1.Rows.Add(array[0], array[1], array[2], array[3], array[4]);


                }


                dr.Close();// Close reader.
                if (sayfa == (rowCount / 25))
                {
                    lblSayfa.Text = (sayfa) + "/" + (rowCount / 25);

                }
                else
                {
                    lblSayfa.Text = (sayfa + 1) + "/" + (rowCount / 25);

                }


                mysqlbaglan.Close();
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {

                DialogResult dialog = MessageBox.Show(dataGridView1[0, e.RowIndex].Value.ToString() + " Id Kullanicisini Silmek Istiyor Musunuz ?",
                    "Exit", MessageBoxButtons.YesNo);

                if (dialog == DialogResult.Yes)
                {

                    mysqlbaglan.Open();
                    string sql = "DELETE FROM `bx-users` WHERE `bx-users`.`User-ID` = '" + (dataGridView1[0, e.RowIndex].Value) + "'";
                    MySqlCommand cmd = new MySqlCommand(sql, mysqlbaglan);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show(dataGridView1[0, e.RowIndex].Value.ToString() + " Id Kullanicisi Silindi !");

                    mysqlbaglan.Close();
                    refreshListUser(2);


                }
                else if (dialog == DialogResult.No)
                {

                }
            }
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }
    }
}
