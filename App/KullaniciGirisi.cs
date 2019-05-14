using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Yazlab
{
    public partial class KullaniciGirisi : Form
    {
        private static string sIp = "127.0.0.1"; // Sunucu IP Adresi
        private static string sDB = "books"; // Veritabanı
        private static string sKA = "root"; // Kullanıcı Adı
        private static string sSifre = "root"; // Şifre
        public KullaniciGirisi()
        {
            InitializeComponent();
        }

        private void giris_Click(object sender, EventArgs e)
        {
            MySqlConnection mysqlbaglan = new MySqlConnection("Server=" + sIp + ";Database=" + sDB + ";Uid=" + sKA + ";Pwd='" + sSifre + "';SslMode=none");

            mysqlbaglan.Open();
            string sql = "select* from  `bx-users` where user_name like '" + user_name.Text + "' AND password like '" + user_password.Text + "'";
            MySqlCommand cmd = new MySqlCommand(sql, mysqlbaglan);
            cmd.ExecuteNonQuery();
            cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, mysqlbaglan);
            if (user_name.Text.Equals("admin") && user_password.Text.Equals("password"))
            {
                AdminSayfası aS = new AdminSayfası();
                aS.Show();
                this.Hide();
            }
            else
            {
                var dr = cmd.ExecuteReader();
                SecimEkrani tk = new SecimEkrani();
                if (dr.HasRows)
                {
                    dr.Close();
                    sql = "SELECT * FROM `bx-users` WHERE `user_name` = \"" + user_name.Text + "\"";
                    MySqlCommand cmd2 = new MySqlCommand(sql, mysqlbaglan);
                    cmd2 = new MySql.Data.MySqlClient.MySqlCommand(sql, mysqlbaglan);
                    var vR = cmd2.ExecuteReader();
                    while (vR.Read())
                    {
                        tk.newUser = Int32.Parse(vR.GetValue(0).ToString());
                    }
                    vR.Close();
                    tk.Show();
                }
                else
                {
                    MessageBox.Show("Lütfen bilgilerinizi kontrol ediniz.");
                }
            }
            mysqlbaglan.Close();
            
        }

        private void kayit_Click(object sender, EventArgs e)
        {
            KayitForm kf = new KayitForm();
            kf.Show();
            this.Hide();
        }
    }
}
