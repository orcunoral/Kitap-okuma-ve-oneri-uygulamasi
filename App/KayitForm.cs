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
using System.Diagnostics;

namespace Yazlab
{
    public partial class KayitForm : Form
    {

        private static string sIp = "127.0.0.1"; // Sunucu IP Adresi
        private static string sDB = "books"; // Veritabanı
        private static string sKA = "root"; // Kullanıcı Adı
        private static string sSifre = "root"; // Şifre
        public KayitForm()
        {
            InitializeComponent();
        }

        private void user_kayit_Click(object sender, EventArgs e)
        {
            MySqlConnection mysqlbaglan = new MySqlConnection("Server=" + sIp + ";Database=" + sDB + ";Uid=" + sKA + ";Pwd='" + sSifre + "';SslMode=none");
            mysqlbaglan.Open();
            if (user_name.Text.Equals("") || user_password.Text.Equals("") || user_location.Text.Equals("") || user_location.Text.Equals("") || user_age.Text.Equals(""))
            {
                MessageBox.Show("Butun alanlari doldurunuz...");
            }
            else
            {/*
                string sql = "INSERT INTO `bx-users`(`User-ID`, `Location`, `Age`, `password`, `user_name`) VALUES (null,'" + user_location.Text + "','" + user_age.Text + "','" + user_password.Text + "','" + user_name.Text + "')";
                MySqlCommand cmd = new MySqlCommand(sql, mysqlbaglan);
                cmd.ExecuteNonQuery();
                sql = "SELECT * FROM `bx-users`ORDER BY `bx-users`.`User-ID`  DESC LIMIT 1";
                cmd = new MySqlCommand(sql, mysqlbaglan);
                cmd.ExecuteNonQuery();
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, mysqlbaglan);
                var vR = cmd.ExecuteReader();
                while (vR.Read())
                {
                    tk.newUser = Int32.Parse(vR.GetValue(0).ToString());
                }*/
                mysqlbaglan.Close();
                SecimEkrani tk = new SecimEkrani();
                tk.deneme = 1;
                tk.newAge = user_age.Text;
                tk.newLocation = user_location.Text;
                tk.newName = user_name.Text;
                tk.newPass = user_password.Text;
                tk.Show();
                this.Close();
            }
        }
    }
}
