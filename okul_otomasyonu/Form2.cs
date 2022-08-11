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
using System.Net;
using System.Net.Mail;

namespace okul_otomasyonu
{
    public partial class Form2 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=schoolDatabase;Integrated Security=True");
        public Form2()
        {
            InitializeComponent();
        }


        public void listing_lectures()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("Select lectures.L_id as 'Ders ID',teaching_staff.name as 'Ö.Görevlisi',lectures.L_code as 'Ders Kodu', lectures.l_name as 'Ders Adı',programs.P_name as 'Program' from lectures " +
                "inner join teaching_staff on lectures.tstaff_id = teaching_staff.tstaff_id inner join programs on lectures.P_id = programs.P_id ", con);
                DataSet ds2 = new DataSet();

                adapter.Fill(ds2, "lectures");
                dataGridView4.DataSource = ds2.Tables["lectures"];
                adapter.Dispose();
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata");
            }

        }
        public void listing_tsaff()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("", con);
                DataSet ds2 = new DataSet();

                adapter.Fill(ds2, "teaching_staff");
                dataGridView4.DataSource = ds2.Tables["teaching_staff"];
                adapter.Dispose();
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
          

            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "INSERT INTO lectures(L_code,l_name,tstaff_id,P_id) VALUES('" + textBox9.Text+"','"+textBox10.Text+"','"+ Convert.ToInt32(comboBox3.SelectedValue)+"','"+Convert.ToInt32(comboBox4.SelectedValue)+"')";
                command.Connection = con;
                command.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Ders Eklenmiştir.");
            }
            catch (Exception hata)
            {
                MessageBox.Show("Ders Eklenemedi Hata Oluştu" + hata);
            }
            


        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'schoolDatabaseDataSet2.teaching_staff' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.teaching_staffTableAdapter1.Fill(this.schoolDatabaseDataSet2.teaching_staff);
            // TODO: Bu kod satırı 'schoolDatabaseDataSet1.season' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.seasonTableAdapter.Fill(this.schoolDatabaseDataSet1.season);

            // TODO: Bu kod satırı 'schoolDatabaseDataSet1.programs' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.programsTableAdapter.Fill(this.schoolDatabaseDataSet1.programs);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel1.Hide();
            panel2.Hide();
            panel3.Hide();
            panel4.Show();
            listing_lectures();
            this.Form2_Load(null, null);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                if(con.State == ConnectionState.Closed)
                con.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = con;
                command.CommandText = "Delete from lectures where L_id ='" + Convert.ToInt32(textBox11.Text) + "'";
                command.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(textBox11.Text + " İd Numaralı Ders Başarılı Bir Şekilde Silinmiştir.");
            }
            catch (Exception hata)
            {
                MessageBox.Show("Lütfen Geçerli Bir İd Numarası Giriniz !!");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = con;
                command.CommandText = "Delete from teaching_staff where tsaff_id =='" + Convert.ToInt32(textBox4.Text) + "'";
                command.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(textBox4.Text + " İd Numaralı Öğretim Görevlisi Başarılı Bir Şekilde Silinmiştir.");
            }
            catch (Exception hata)
            {
                MessageBox.Show("Lütfen Geçerli Bir İd Numarası Giriniz !!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Hide();
            panel2.Show();
            panel3.Hide();
            panel4.Hide();

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("Select teaching_staff.tstaff_id as 'Ö.Görevlisi ID', registration_state.state as 'Kayıt durumu',role.roles as 'Görevi',teaching_staff.name as 'Ad Soyad',teaching_staff.email as 'E-posta Adresi',teaching_staff.tel_num as 'Telefon Numarası',teaching_staff.birthday as 'Doğum Tarihi',teaching_staff.gender as 'Cinsiyeti' from teaching_staff " +
                "inner join role on role.roleid = teaching_staff.roleid " +
                "inner join registration_state on registration_state.reg_id = teaching_staff.reg_id where teaching_staff.roleid = 2", con);
                DataSet ds2 = new DataSet();
                con.Open();
                adapter.Fill(ds2, "teaching_staff");
                dataGridView2.DataSource = ds2.Tables["teaching_staff"];
                adapter.Dispose();
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Bir ID Numarası Giriniz");
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("Select lectures.L_id as 'Ders ID',teaching_staff.name as 'Ö.Görevlisi',lectures.L_code as 'Ders Kodu', lectures.l_name as 'Ders Adı',programs.p_name as 'Program' from lectures " +
            "inner join teaching_staff on lectures.tstaff_id = teaching_staff.tstaff_id inner join programs on lectures.P_id = programs.P_id where teaching_staff.tstaff_id='"+Convert.ToInt32(textBox12.Text)+"' ", con);
            DataSet ds2 = new DataSet();
            con.Open();
            adapter.Fill(ds2, "lectures");
            dataGridView5.DataSource = ds2.Tables["lectures"];
            adapter.Dispose();
            con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Bir ID Numarası Giriniz");
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("Select teaching_staff.tstaff_id as 'Ö.Görevlisi ID',teaching_staff.name 'Ad Soyad',days.day_name as 'Günler' from tstaff_days " +
            "inner join teaching_staff on teaching_staff.tstaff_id = tstaff_days.tstaff_id " +
            "inner join days on days.day_id = tstaff_days.day_id  where teaching_staff.tstaff_id='" + Convert.ToInt32(textBox13.Text) + "' ", con);
                DataSet ds2 = new DataSet();
                con.Open();
                adapter.Fill(ds2, "tstaff_days");
                dataGridView6.DataSource = ds2.Tables["tstaff_days"];
                adapter.Dispose();
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Bir ID Numarası Giriniz");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Random Rnd = new Random();
            StringBuilder StrBuild = new StringBuilder();
            for (int i = 0; i < 8; i++)
            {
                int ASCII = Rnd.Next(32, 127);
                char Karakter = Convert.ToChar(ASCII);
                StrBuild.Append(Karakter);
            }
            label13.Text = StrBuild.ToString();
            try
            {
                if(con.State == ConnectionState.Closed)
                con.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "INSERT INTO teaching_staff(roleid,name,email,pass,gender,reg_id) VALUES('2','" + textBox1.Text + "','" + textBox3.Text + "','" + label13.Text + "','" + comboBox1.SelectedItem + "','2')";
                command.Connection = con;

                MailMessage message = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential("mustafatsdis@gmail.com","Mustafa123.");
                message.To.Add(textBox3.Text);
                message.From = new MailAddress("mustafatsdis@gmail.com");
                message.Subject = "MCBÜ SMYO İlk Kayıt Parolanız";
                message.Body = "Sisteme ilk Giriş Parolanız : '" + label13.Text + "' Lütfen Bu Parolayı Kimseyle Paylaşmayınız.Bu Parola İle Mcbü-salihli Myo Yöenetim Paneline İlk Kaydınızı Geçekleştirebilirsiniz.";
                smtpClient.Send(message);

                command.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Öğretim Görevlisi Eklendi Ve İlgili E-Postaya Gönderildi");
            }
            catch(Exception hata)
            {
                MessageBox.Show("Kayıt Eklenemedi Hata Oluştu, E-Posta Alanı ve Diğer Alanlar Boş Bırakılmaz !!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Hide();
            panel2.Hide();
            panel3.Show();
            panel4.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int season_state = comboBox5.SelectedIndex + 1;

            con.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.CommandText = "Update season set season_state = '" + season_state + "'";
            command.ExecuteNonQuery();
            SqlCommand command2 = new SqlCommand();
            command2.Connection = con;
            command2.CommandText = "Select season_state from season";
            SqlDataReader dr = command2.ExecuteReader();

            while (dr.Read())
            {
                switch (dr["season_state"])
                {
                    case "1":
                        label7.Text = "Sezon Açık";
                        break;
                    case "2":
                        label7.Text = "Sezon Kapalı";
                        break;
                }
            }
            

            dr.Close();
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Show();
            panel2.Hide();
            panel3.Hide();
            panel4.Hide();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select exam.exam_id as 'Sınav ID',lectures.l_name as 'Ders Adı',programs.p_name as 'Program Adı',teaching_staff.name as 'Ö.Görevlisi',exam_type.type_name as 'Sınav Tipi',exam.time as 'Sınav Süresi' from exam " +
            "inner join lectures on lectures.L_id = exam.L_id "+
            "inner join programs on programs.P_id = exam.P_id "+
            "inner join teaching_staff on teaching_staff.tstaff_id = exam.tstaff_id "+
            "inner join exam_type on exam_type.type_id = exam.type_id ", con);
            DataSet ds2 = new DataSet();
            adapter.Fill(ds2, "exam");
            dataGridView1.DataSource = ds2.Tables["exam"];
            adapter.Dispose();
            con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult dialog = new DialogResult();
                dialog = MessageBox.Show("Sınav Listesi Excel Dosyasına aktarılacaktır, İşlem Veri Sayısına Göre Hızlı/Yavaş Sürebilir.", "EXCEL'E AKTARMA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialog == DialogResult.Yes)
                {
                    Microsoft.Office.Interop.Excel.Application uyg = new Microsoft.Office.Interop.Excel.Application();
                    uyg.Visible = true;
                    Microsoft.Office.Interop.Excel.Workbook kitap = uyg.Workbooks.Add(System.Reflection.Missing.Value);
                    Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)kitap.Sheets[1];
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[1, i + 1];
                        myRange.Value2 = dataGridView1.Columns[i].HeaderText;
                    }

                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        for (int j = 0; j < dataGridView1.Rows.Count; j++)
                        {
                            Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                            myRange.Value2 = dataGridView1[i, j].Value;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("İŞLEM İPTAL EDİLDİ.", "İşlem Sonucu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("İŞLEM TAMAMLANMADAN EXCEL PENCERESİNİ KAPATTINIZ.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label40_Click(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {

            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "INSERT INTO programs(p_name) VALUES('"+textBox2.Text+"')";
                command.Connection = con;
                command.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Program Eklenmiştir.");
            }
            catch (Exception )
            {
                MessageBox.Show("Program Eklenemedi");
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult dialog = new DialogResult();
                dialog = MessageBox.Show("Öğretim Görevlileri Geldiği Günler Listesi Excel Dosyasına aktarılacaktır, İşlem Veri Sayısına Göre Hızlı/Yavaş Sürebilir.", "EXCEL'E AKTARMA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialog == DialogResult.Yes)
                {
                    Microsoft.Office.Interop.Excel.Application uyg = new Microsoft.Office.Interop.Excel.Application();
                    uyg.Visible = true;
                    Microsoft.Office.Interop.Excel.Workbook kitap = uyg.Workbooks.Add(System.Reflection.Missing.Value);
                    Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)kitap.Sheets[1];
                    for (int i = 0; i < dataGridView6.Columns.Count; i++)
                    {
                        Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[1, i + 1];
                        myRange.Value2 = dataGridView6.Columns[i].HeaderText;
                    }

                    for (int i = 0; i < dataGridView6.Columns.Count; i++)
                    {
                        for (int j = 0; j < dataGridView6.Rows.Count; j++)
                        {
                            Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                            myRange.Value2 = dataGridView6[i, j].Value;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("İŞLEM İPTAL EDİLDİ.", "İşlem Sonucu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("İŞLEM TAMAMLANMADAN EXCEL PENCERESİNİ KAPATTINIZ.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }
    }
}
