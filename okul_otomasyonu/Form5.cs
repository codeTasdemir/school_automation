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

namespace okul_otomasyonu
{
    public partial class Form5 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=schoolDatabase;Integrated Security=True");
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'schoolDatabaseDataSet1.days' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.daysTableAdapter.Fill(this.schoolDatabaseDataSet1.days);
            MessageBox.Show("Değerli Öğretim Üyemiz Biraz Sonra Göreceğiniz Ekranı, İlk Defa Giriş Yapacağınız İçin Göreceksiniz. Lütfen İstenen Bilgileri Eksiksiz Girerek Kayıt İşleminizi Tamamlayınız.");
            label3.Text = Form1.email_for_form;
            
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (con.State == ConnectionState.Closed)
                con.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = con;
                command.CommandText = "UPDATE  teaching_staff set pass='" + textBox1.Text + "' , rec_quesiton ='" + textBox2.Text + "' , tel_num='"  + Convert.ToString(maskedTextBox1.Text) + "' , birthday = '" + dateTimePicker1.Text + "' , reg_id=1 where email='"+Form1.email_for_form+"' ";
                command.ExecuteNonQuery();
                MessageBox.Show("Bilgileriniz Kaydedilmiştir, Güvenle Giriş Yapabilirsiniz..");
                this.Close();
                

            }
            catch (Exception hata)
            {
                MessageBox.Show("Alanları tekrar kontrol ediniz " + hata);
            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand command2 = new SqlCommand();
            command2.Connection = con;
            command2.CommandText = "Select teaching_staff.tstaff_id from teaching_staff where teaching_staff.email ='" + Form1.email_for_form + "'";
            command2.ExecuteNonQuery();
            SqlDataReader dr = command2.ExecuteReader();
            dr.Read();
            label4.Text = Convert.ToString(dr[0]);
            int id = Convert.ToInt32(label4.Text);
            dr.Close();
            con.Close();
            listBox1.Items.Add(comboBox1.SelectedValue.ToString());
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "INSERT INTO tstaff_days(tstaff_id,day_id) VALUES('"+id+"','"+Convert.ToInt32(comboBox1.SelectedValue)+"')";
                command.Connection = con;
                command.ExecuteNonQuery();
                
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Gün Eklenemedi , Aynı Gün Tekrar Eklenemez");
            }
        }
    }
}
