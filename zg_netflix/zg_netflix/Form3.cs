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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        string fiyat, drm, person, crdnumber, date;
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-8O3I53L;Initial Catalog=netflix;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter dr=new SqlDataAdapter();
        void kayıt()
        {//kullanıcı tablo adı
            crdnumber = textBox6.Text + textBox7.Text + textBox8.Text + textBox9.Text;
            date = textBox10.Text + textBox11.Text;
            con.Open();
            //cmd.Connection = con;
            cmd = new SqlCommand("insert into kullanıcı(name_surname,phone,username,password,security_ques,security_ans,cardnumber,cardnamesurname,date,cvv,ucret,kackısı,drm) values(" +
                "@ad,@tel,@user,@pass,@ques,@ans,@cardn,@cardsurn,@dat,@cv,@fiyat,@kisi,@durum)",con);
            cmd.Parameters.AddWithValue("@ad", textBox1.Text);
            cmd.Parameters.AddWithValue("@tel", textBox2.Text);
            cmd.Parameters.AddWithValue("@user", textBox3.Text);
            cmd.Parameters.AddWithValue("@pass", textBox4.Text);
            cmd.Parameters.AddWithValue("@ques", comboBox1.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@ans", textBox5.Text);
            cmd.Parameters.AddWithValue("@cardn", crdnumber);
            cmd.Parameters.AddWithValue("@cardsurn", textBox12.Text);
            cmd.Parameters.AddWithValue("@dat", date);
            cmd.Parameters.AddWithValue("@cv", textBox13.Text);
            cmd.Parameters.AddWithValue("@fiyat", fiyat);
            cmd.Parameters.AddWithValue("@kisi", person);
            cmd.Parameters.AddWithValue("@durum", drm);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Queen Netflix Welcome");
            con.Close();
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }
        public int varmı(string aranan) //aynı kişi olamaz
        {
            int sonuc;
            cmd = new SqlCommand("Select COUNT(username) from kullanıcı WHERE username='" + aranan + "'", con);
            con.Open();
            sonuc = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return sonuc;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            //kaydetme ve ödeme işlemi 
            if(textBox6.Text!=""&&textBox7.Text!=""&&textBox8.Text!=""&&textBox9.Text!=""&&textBox10.Text!=""&&textBox11.Text!=""&&textBox12.Text!=""&&textBox13.Text!="")
            {
                //kaydet
                if (checkBox1.Checked == true)
                {
                    //kaydet yapılacak
                    if (varmı(textBox3.Text) != 0)
                    {
                        MessageBox.Show("it has a username");
                        textBox3.Text = "";
                    }
                    else
                    {
                        kayıt();
                    }

                }
                else
                {
                    MessageBox.Show("Accept The Contract");
                }

            }
            else
            {
                MessageBox.Show("Please Fill In Your Full Information");
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            if(textBox1.Text!=""&&textBox2.Text!=""&&textBox3.Text!=""&&textBox4.Text!=""&&textBox5.Text!=""&&comboBox1.Text!="")
            {
                groupBox2.Enabled = true;
            }
            else
            {
                MessageBox.Show("Please Fill In Your Full Information");
                groupBox1.Enabled = true;
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        { //from yüklendiği
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            comboBox1.Items.Add("What's your mother's maiden name?");
            comboBox1.Items.Add("What is the name of your first pet?");
            comboBox1.Items.Add("What is the model of your first car ?");
            comboBox1.Items.Add("Which city do you live in?");
            comboBox1.Items.Add("What's your father's middle name?");

        }

        private void button6_Click(object sender, EventArgs e)
        {
            groupBox2.Enabled = true;
            label24.Text = "price";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //basic seçim 
            groupBox3.Enabled = true;
            groupBox2.Enabled = false;
            fiyat = label10.Text;
            drm = label11.Text;
            person = label12.Text;
            label24.Text = fiyat;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //standart paket
            groupBox3.Enabled = true;
            groupBox2.Enabled = false;
            fiyat = label15.Text;
            drm = label14.Text;
            person = label13.Text;
            label24.Text = fiyat;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //secial paket
            groupBox3.Enabled = true;
            groupBox2.Enabled = false;
            fiyat = label18.Text;
            drm = label17.Text;
            person = label16.Text;
            label24.Text = fiyat;
        }
    }
}
