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

namespace zg_netflix //zümrüt gemici
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-8O3I53L;Initial Catalog=netflix;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader dr;
        string kullanıcı;
        public static string kim;
        int sonuc;
        List<string> strad = new List<string>();
        List<string> admin = new List<string>();
        void haydiizle()
        {
            Form5 frm5 = new Form5();
            frm5.Show();
            this.Hide();
        }
        void tedarikci()
        {
            cekyaz();
            string aranan = Form1.isim;
            //örneğin 5 tane tedarikci varsa 5 ninde kullanıcı adı buttonlara yazdırılacak
            cmd = new SqlCommand("Select COUNT(tedarikci) from kullanıcı2 WHERE tedarikci='" + aranan + "'", con);
            con.Open();
            sonuc = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
           // MessageBox.Show("kişi sayısı: " + sonuc.ToString());
            if(button2.Text!="ekle")
            {
                button2.Enabled = true;
            }
            if (button3.Text != "ekle")
            {
                button3.Enabled = true;
            }
            if (button4.Text != "ekle")
            {
                button4.Enabled = true;
            }
        }
        void cekyaz()
        {
            /*            cmd = new SqlCommand("Select * from film", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while(dr.Read())
            {
               strList.Add(dr["resim"].ToString());
            }
            con.Close();*/
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText= "Select username From kullanıcı2 where tedarikci = '" + Form1.isim + "'";
            //cmd.CommandText = "Select * from kullanıcı2";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                admin.Add(dr["username"].ToString());
            }
            con.Close();
            // MessageBox.Show(admin[0].ToString());
            //MessageBox.Show(admin[1].ToString());
           int sayi2 = admin.Count;
            //MessageBox.Show("adminin eleman sayısı :"+admin.Count.ToString());
            /*for(int i=0;i<admin.Count;i++)
            {
                button2.Text = admin[i];
                button3.Text = admin[i];
                button4.Text = admin[i];
                button5.Text = admin[i];
            }*/
            if(sayi2==1)
            {
                button2.Text = admin[0];
            }
            if (sayi2 == 2)
            {
                button2.Text = admin[0];
                button3.Text = admin[1];
            }
            if (sayi2 == 3)
            {
                button2.Text = admin[0];
                button3.Text = admin[1];
                button4.Text = admin[2];
            }
            if (sayi2 == 4)
            {
                button2.Text = admin[0];
                button3.Text = admin[1];
                button4.Text = admin[2];
                button5.Text = admin[3];
            }


        }
        void tedarikci2()
        {
            //buton 2 dolu
            //button 3 ve 4 dolacak
           // cekyaz();
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "Select username From kullanıcı2 where tedarikci = '" + Form1.isim + "'";
            //cmd.CommandText = "Select * from kullanıcı2";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                admin.Add(dr["username"].ToString());
            }
            con.Close();
            int sayi2 = admin.Count;
           // MessageBox.Show("adminin eleman sayısı :" + admin.Count.ToString());
            if (sayi2 == 1)
            {
                button3.Text = admin[0];
                button3.Enabled = true;
            }
            if (sayi2 == 2)
            {
                button3.Text = admin[0];
                button4.Text = admin[1];
                button3.Enabled = true;
                button4.Enabled = true;
            }
            if (sayi2 == 3)
            {
                button2.Text = admin[0];
                button3.Text = admin[1];
                button4.Text = admin[2];
                button3.Enabled = true;
                button4.Enabled = true;
                button2.Enabled = true;
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            button1.Visible = button2.Visible = button3.Visible = button4.Visible = button5.Visible = false;
            label3.Visible = false;
            groupBox1.Visible = false;
            cmd = new SqlCommand();
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
            string btt = strad[0].ToString();
          // MessageBox.Show(strad[0].ToString());

            if(btt=="1")
            {
                button3.Text = Form1.isim;
                button3.Visible = true;
            }
            else if(btt=="3")
            {
                label3.Visible = true;
                button2.Visible = button3.Visible = button4.Visible = true;
                button3.Enabled = button4.Enabled = false;
                button2.Text = kullanıcı;
                tedarikci2();
                //tedarikci();
                //button3.Text = "ekle";
               // button4.Text = "ekle";
                // button2.Text = strad[1];
                // button3.Text = strad[2];
            }
            else if(btt=="5")
            {
                label3.Visible = true;
                // MessageBox.Show("doğru giriş");
                button2.Visible = button3.Visible = button4.Visible = button1.Visible = button5.Visible = true;
               // button1.Text = kullanıcı;
                button3.Enabled = button4.Enabled = button2.Enabled = button5.Enabled = false;
                button1.Text = kullanıcı;
                /*button2.Text = "ekle";
                button3.Text = "ekle";
                button4.Text = "ekle";
                button5.Text = "ekle";*/
            tedarikci();

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            kim = button3.Text;
            haydiizle();//tek kişilik girişlerde

        }
        string sayi ,isim2;
       /* void kontrol()
        {
            if (button2.Text == "ekle")
            {
                button2.Text = isim2;
                isim2 = "ekle";
                button2.Enabled = true;
            }
            if (button3.Text == "ekle")
            {
                button3.Text = isim2;
                button3.Enabled = true;
                isim2 = "ekle";
            }
            if (button4.Text == "ekle")
            {
                button4.Text = isim2;
                button4.Enabled = true;
                isim2 = "ekle";
            }
            if (button5.Text == "ekle")
            {
                button5.Text = isim2;
                button5.Enabled = true;
                isim2 = "ekle";
            }
        }*/
        private void label3_Click(object sender, EventArgs e)
        {
            //bir den fazla izleyici kaydi 
            //kullanıcı için gerekenler
            //kullanıcı adı ,kiminsayesindeburda
            //izlediği filmadı ,turu,
            //tedarikçi: form1 de isim (textbox1 yazılan kullanıcı adına sahip kişi )
           // groupBox1.Visible = true;
            if (button2.Text=="ekle")
            {
                sayi = "2";
                groupBox1.Visible = true;
            }
            if(button3.Text=="ekle")
            {
                sayi = "3";
                groupBox1.Visible = true;
            }
            if (button4.Text == "ekle")
            {
                sayi = "4";
                groupBox1.Visible = true;
            }
            if (button5.Text == "ekle")
            {
                sayi = "5";
                groupBox1.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            kim = button1.Text;
            haydiizle();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            kim = button2.Text;
            haydiizle();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            kim = button4.Text;
            haydiizle();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            kim = button5.Text;
            haydiizle();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //kaydet yapılacak alan
            //if(textBox1.Text!="")
            // {
            if (e.KeyCode == Keys.Enter)
            {
                con.Open();
                cmd.Connection = con;
                cmd = new SqlCommand("insert into kullanıcı2(username,tedarikci) values(@ad,@kimden)", con);
                cmd.Parameters.AddWithValue("@ad", textBox1.Text);
                cmd.Parameters.AddWithValue("@kimden", Form1.isim);
                cmd.ExecuteNonQuery();
                //cmd.Clone();
                con.Close(); 
                MessageBox.Show("Kayıt eklendi");
                isim2 = textBox1.Text;
                textBox1.Text = "";
               // kontrol();
                groupBox1.Visible = false;
                admin.Clear();
                tedarikci();
            }
           /* }
            else
            {
                MessageBox.Show("Please Enter Username");
            }*/
    }
}
}
