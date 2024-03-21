using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Media;
using WMPLib;

namespace zg_netflix
{
    public partial class Form5 : Form//zümrüt gemici
    {
        public Form5()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-8O3I53L;Initial Catalog=netflix;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader dr;
        int sayi;
        Random rnd = new Random();
       // string dizi = "dram";
        List<string> strList = new List<string>();
        List<string> strList2 = new List<string>();
        void rnd2()
        {
            sayi = rnd.Next(1, 4);
        }
        void filmcek(string dizi)
        {
            //"Select * From kullanıcı where username='" + textBox1.Text + "'";
            cmd = new SqlCommand("Select * From film where tur='" +dizi+ "'", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                strList.Add(dr["resim"].ToString());
                strList2.Add(dr["adi"].ToString());
            }
            con.Close();
        }
        string pc1, pc2, pc3, pc4, pc9, pc10,tur,tur2;
        private void Form5_Load(object sender, EventArgs e)
        {
            filmcek("Korku");
            pictureBox1.ImageLocation= strList[0];
            pictureBox2.ImageLocation = strList[1];
            pictureBox3.ImageLocation = strList[2];
            pictureBox4.ImageLocation = strList[3];
            pc1 = strList2[0].ToString();
            pc2 = strList2[1].ToString();
            pc3 = strList2[2].ToString();
            pc4 = strList2[3].ToString();
            label2.Text = "Korku";
            tur = label2.Text;
            strList.Clear();
            filmcek("Aksiyon");
            pictureBox9.ImageLocation = strList[0];
            pictureBox10.ImageLocation = strList[1];
            pc9 = strList2[0].ToString();
            pc10 = strList2[1].ToString();
            tur2 = label4.Text;
            label4.Text = "Aksiyon";
            label3.Text = Form4.kim + " Keep Watching";
            oynatbakalım();
            //C:\Users\zmrtg\OneDrive\Masaüstü\queen_netflix\video\1.mp4
            //MessageBox.Show(yol);
            //axWindowsMediaPlayer1.URL = @"C:\Users\zmrtg\source\repos\zg_netflix\videolar\"+yol.ToString();
            // string yol_ur= @"C:\Users\zmrtg\source\repos\zg_netflix\videolar\" + yol.ToString();
            //  string yol3 = @"videolar\" + yol;
            //MessageBox.Show(yol3);
            //axWindowsMediaPlayer1.URL = @"videolar\" + yol.ToString();
            comboBox1.Items.Add("Çocuk");
            comboBox1.Items.Add("Fantastik");
            comboBox1.Items.Add("Aksiyon");
            comboBox1.Items.Add("Korku");
            comboBox1.Items.Add("Dram");
            comboBox1.Items.Add("Gerilim");
            comboBox1.Items.Add("Polisiye");
            comboBox1.Items.Add("Gençlik Dizisi");


        }
        string yol = "";
        void oynatbakalım()
        {
             
            /* cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "Select * From kullanıcı where username='" + Form1.isim + "'";
            dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                kullanıcı = dr["name_surname"].ToString();
                strad.Add(dr["kackısı"].ToString());      
            }
            con.Close();
            string btt = strad[0].ToString();*/
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "Select * From video";
            dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                yol = dr["video_ad"].ToString();
               // yol2 = dr["video_veriyolu"].ToString();
                richTextBox1.Text = dr["video_acıklama"].ToString();
                label5.Text = dr["video_ad"]+"/Subject";
            }
            con.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            izle2 = "geldi2";
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            izle3 = "geldi3";
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            izle4 = "geldi4";
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            izle9 = "geldi9";
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            izle10 = "geldi10";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //oturumu kapat 
            MessageBox.Show("Closing Session");
            Form4 frm4 = new Form4();
            frm4.Show();
            this.Hide();

        }
        string ara;


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            strList.Clear();
            ara = comboBox1.Text;
            label4.Text = ara.ToString();
            MessageBox.Show(ara.ToString());
            filmcek(comboBox1.Text);
            //con.Open();
            /*cmd = new SqlCommand("Select * From film where tur='" + ara + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                strList.Add(dr["resim"].ToString());
            }
            con.Close();*/
            pictureBox9.ImageLocation = strList[0];
            pictureBox10.ImageLocation = strList[1];
            pc9 = strList[0].ToString();
            pc10 = strList[1].ToString();
            tur2 = ara;
            //strList.Clear();

        }
        string izle1,izle2,izle3,izle4,izle9,izle10="";
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            izle1 = "geldi1";
            izlemeyedevamke();
        }

        void izlemeyedevamke()
        {
            if(izle1=="geldi1")
            {
                //ekleme yapılacak veritabanına 
                MessageBox.Show(pc1);
                MessageBox.Show(tur);
                con.Open();
                cmd = new SqlCommand("Update kullanıcı2 set filmadi=@statu,filmturu=@hot where username=@idd",con);
                cmd.Parameters.AddWithValue("@statu", pc1.ToString());
                cmd.Parameters.AddWithValue("@hot", tur.ToString());
                cmd.Parameters.AddWithValue("@idd", Form4.kim);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Kayıt Güncellendi");





                //"Select * From kullanıcı where username='" + textBox1.Text + "'";
                cmd = new SqlCommand("Select * From film where tur='" + tur + "'", con);
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    strList.Add(dr["resim"].ToString());
                }
                con.Close();
                pictureBox8.ImageLocation = strList[0];

            }
        }
    }
}
