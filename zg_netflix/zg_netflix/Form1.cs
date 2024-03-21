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

namespace zg_netflix
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-8O3I53L;Initial Catalog=netflix;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader dr;
        public static string isim;
        List<string> strList = new List<string>();
        void filmcek()
        {
            cmd = new SqlCommand("Select * from film", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while(dr.Read())
            {
               strList.Add(dr["resim"].ToString());
            }
            con.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {
           // groupBox1.Visible = true;
            textBox1.Enabled = false;
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            filmcek();
            pictureBox1.ImageLocation = strList[0];
            pictureBox2.ImageLocation = strList[1];
            pictureBox3.ImageLocation = strList[2];
            pictureBox4.ImageLocation = strList[3];
            pictureBox5.ImageLocation = strList[4];
            pictureBox6.ImageLocation = strList[5];
            pictureBox7.ImageLocation = strList[6];
            pictureBox8.ImageLocation = strList[7];
        }

        private void label4_Click(object sender, EventArgs e)
        {
            //kayıt yapılcak forma git
            Form3 frm3 = new Form3();
            frm3.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "Select * From user where name='" + textBox2.Text + "'";
            dr = cmd.ExecuteReader();
            if(dr.Read())
            {
                Form2 frm2 = new Form2();
                frm2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("the username or password is incorrect.");
            }
            con.Close();*/
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //enter basılınca işlem
                cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "Select * From kullanıcı where username='" + textBox1.Text + "'";
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    isim = textBox1.Text;
                    Form4 frm4 = new Form4();
                    frm4.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("the username is incorrect.");
                }
                con.Close();
            }
        }
    }
}
