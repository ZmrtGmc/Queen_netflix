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

namespace zg_netflix
{
    public partial class Form2 : Form//zümrüt gemici
    {
        public Form2()
        {
            InitializeComponent();
           // axWindowsMediaPlayer1.uiMode = "none";

        }
        string resimpath;
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-8O3I53L;Initial Catalog=netflix;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt = new DataTable();
        int id;
        

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }
        void listele()
        {
            //con.Open();
            dt.Clear();
            cmd = new SqlCommand("Select * From video",con);
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //resim ekle
            openFileDialog1.Title = "Resim Aç";
             openFileDialog1.Filter = "Jpeg Dosyası (*.jpg)|*.jpg|Gif Dosyası (*.gif)|*.gif|Png Dosyası (*.png)|*.png|Tif Dosyası (*.tif)|*.tif";
             if (openFileDialog1.ShowDialog() == DialogResult.OK)
             {
                 pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                 resimpath = openFileDialog1.FileName.ToString();
                // label3.Text = resimpath;

             }
            //axWindowsMediaPlayer1.Ctlcontrols.play();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //kaydet
            
            id = (dataGridView1.Rows.Count - 1) + 1;
           
            con.Open();
            cmd = new SqlCommand("insert into film(id,adi,tur,resim) values(@p1,@p2,@p3,@p4)",con);
            cmd.Parameters.AddWithValue("@p1", id.ToString());//id
            cmd.Parameters.AddWithValue("@p2", textBox1.Text);//filmadi
            cmd.Parameters.AddWithValue("@p3", textBox2.Text);//turu
            cmd.Parameters.AddWithValue("@p4", resimpath.ToString());//uzantı
            cmd.ExecuteNonQuery();
            cmd.Clone();
            MessageBox.Show("Kayıt eklendi");
            //listele();
            //textBox1.Text = "";
            //textBox2.Text = "";
            //pictureBox1.Image = null;
           // label3.Text = "image";
        }
       
        private void Form2_Load(object sender, EventArgs e)
        {
            listele();
            textBox3.Enabled = false;

        }
        string _url;
        private void button4_Click(object sender, EventArgs e)
        {
            //video eklenecek
            try
            {
                
                openFileDialog2.Title = "Video Seçiniz";//filedialogda sol üstte görünen bilgi mesajı
                openFileDialog2.InitialDirectory = @"C:\Users\zmrtg\OneDrive\Masaüstü\queen_netflix";//başlangıç veri yolu belirlenir
                openFileDialog2.Filter = "Sadece Video Dosyaları (*.mp4,*m4v,*.mp4v,*.avi,*.wmv,*m2ts) | *.mp4;*.m4v;*.avi;*.wmv;*.m2ts";
                //geçerli bağlantılar belirlenir
                if(openFileDialog2.ShowDialog()==DialogResult.OK)
                {
                    textBox1.Text = openFileDialog2.SafeFileName;//video adı dosya adı olarak aktarılır
                    _url = openFileDialog2.FileName.ToString();
                    textBox3.Text = _url.ToString();
                    //onaybuton();
                }
                else//seçim tamamlanmadığında
                {
                    MessageBox.Show("Video seçimi yapamdınız.");//uyarı verir
                }


            }
            catch(Exception r)
            {
                MessageBox.Show("Hata! " + r.ToString()); //hata mesajı
            }

        }

        void onaybuton()
        {
            if(textBox1.Text==""||textBox2.Text=="")//alaların dolu olup olmadığını kontrol ediyoruz
            {
                MessageBox.Show("Boş Alan bırakmayın");
            }
            else //metin kutuları boş değilse 
            {
                //exits ile dosyanın var olup olmadığını kontrol ediyoruz
                //aynı isimden dosya varsa ekleme işlemini yaptırmıyoruz
                if(File.Exists(@"videolar\"+textBox1.Text.ToString()))
                {
                    MessageBox.Show("Aynı isimli bir video veritabnında mevcut!", "Aynı Videoyu Ekleme Çalışıyorsunuz", MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                else //aynı isimli dosya bulunmuyorsa ekleme işlemi yapılır
                {//C:\Users\zmrtg\source\repos\WindowsFormsApp6
                    string veriyolu = "videolar\\" + textBox1.Text.ToString() ;
                    con.Open();
                    File.Copy(openFileDialog2.FileName, @"C:\Users\zmrtg\source\repos\zg_netflix\videolar\" + "" + textBox1.Text.ToString());//şurda sorun var
                    cmd = new SqlCommand("insert into video(video_ad,video_acıklama,video_turu,video_dosya_adi,video_veriyolu) values('" + textBox1.Text + "','" + richTextBox1.Text.ToString() + "'," +
                        "'" + textBox2.Text + "','" + textBox1.Text + "','" + veriyolu.ToString() + "')", con);
                    //veritabanına diğer verilerin girişini sağlyırouz
                    int d = cmd.ExecuteNonQuery(); //komut çalışır
                    //ExecuteNonQuery() fonksiyonu integer bir değer döndürü
                    //eğer fonksiyondan true bilgisi gelirse d değişkeni 1 
                    //false bilgisi gelirse d değişkeni -1 alacak
                    if(d>0)
                    {
                        MessageBox.Show("Video Ekleme İşlemi Tamamlandı! ", "Onay", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        listele();
                        con.Close();
                    }
                    else
                    {
                        MessageBox.Show("Hata! Ekleme işlemi başarısız");
                    }

          

                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            onaybuton();
        }
    }
}
