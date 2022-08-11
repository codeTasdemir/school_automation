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
    public partial class Form3 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=schoolDatabase;Integrated Security=True");
        public Form3()
        {
            InitializeComponent();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void tstaff_exam_himself()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select exam.exam_id as 'Sınav ID',lectures.l_name as 'Ders Adı',programs.p_name as 'Program Adı',teaching_staff.name as 'Ö.Görevlisi',exam_type.type_name as 'Sınav Tipi',exam.time as 'Sınav Süresi' from exam inner join lectures on lectures.L_id = exam.L_id  inner join programs on programs.P_id = exam.P_id inner join exam_type on exam_type.type_id = exam.type_id inner join teaching_staff on teaching_staff.tstaff_id = exam.tstaff_id  where teaching_staff.tstaff_id = '" + Convert.ToInt32(label10.Text) + "' ", con);
            DataSet ds2 = new DataSet();
            
            adapter.Fill(ds2, "exam");
            dataGridView1.DataSource = ds2.Tables["exam"];
            adapter.Dispose();
            con.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata");
            }


        }

        public void what_is_my_id()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "Select tstaff_id,email from teaching_staff where email = '"+Form1.email_for_form+"'";
            command.Connection = con;
            SqlDataReader dr = command.ExecuteReader();
            dr.Read();
            label10.Text = dr[0].ToString();
            dr.Read();
            con.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {

            string metin = comboBox1.SelectedItem.ToString();
            var sonMetin = Convert.ToInt32(metin.Substring(0, 1));
            string metin2 = comboBox2.SelectedItem.ToString();
            var sonMetin2 = Convert.ToInt32(metin2.Substring(0, 1));

            
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "INSERT INTO exam(L_id,P_id,type_id,time,tstaff_id) VALUES('" + sonMetin + "','" + sonMetin2 + "','" + Convert.ToInt32(comboBox3.SelectedValue) + "','"+comboBox4.SelectedItem+"','"+Convert.ToInt32(label10.Text)+"')";
                command.Connection = con;
                command.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Sınav Sisteme Eklenmiştir.");
            }
            catch (Exception hata)
            {
                MessageBox.Show("Sınav Eklenemedi Eklenemedi Hata Oluştu" + hata);
            }
            
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            what_is_my_id();
            // TODO: Bu kod satırı 'schoolDatabaseDataSet1.exam_type' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.exam_typeTableAdapter.Fill(this.schoolDatabaseDataSet1.exam_type);
            // TODO: Bu kod satırı 'schoolDatabaseDataSet1.programs' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.programsTableAdapter.Fill(this.schoolDatabaseDataSet1.programs);
            // TODO: Bu kod satırı 'schoolDatabaseDataSet1.lectures' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.lecturesTableAdapter.Fill(this.schoolDatabaseDataSet1.lectures);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Show();
            panel2.Hide();
            panel3.Hide();

            tstaff_exam_himself();


            comboBox1.Items.Clear();
            con.Open();
            SqlCommand komut = new SqlCommand("Select lectures.L_name,lectures.L_id from lectures inner join teaching_staff on lectures.tstaff_id = teaching_staff.tstaff_id where teaching_staff.email = '" + Form1.email_for_form + "' ", con);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["L_id"].ToString() + " - " + dr["L_name"].ToString());
            }
            con.Close();
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string metin = comboBox1.SelectedItem.ToString();
            var sonMetin = Convert.ToInt32(metin.Substring(0, 2));
            
            comboBox2.Items.Clear();
            
            con.Open();
            SqlCommand komut = new SqlCommand("Select lectures.L_name,programs.p_name,programs.P_id from lectures inner join teaching_staff on lectures.tstaff_id = teaching_staff.tstaff_id inner join programs on lectures.P_id = programs.P_id where teaching_staff.email = '"+Form1.email_for_form+"' and lectures.L_id = @p1 ", con);
            komut.Parameters.AddWithValue("@p1",sonMetin);
            SqlDataReader dr = komut.ExecuteReader();

            while (dr.Read())
            {
                comboBox2.Items.Add(dr["P_id"].ToString() +" - " + dr["p_name"].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Show();
            panel1.Hide();
            panel3.Hide();

            try
            {
                if(con.State == ConnectionState.Closed)
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("Select teaching_staff.tstaff_id as 'Ö.Görevlisi ID',role.roles as 'Görevi',teaching_staff.name as 'Ad Soyad',teaching_staff.email as 'E-posta Adresi',teaching_staff.tel_num as 'Telefon Numarası',teaching_staff.birthday as 'Doğum Tarihi',teaching_staff.gender as 'Cinsiyeti' from teaching_staff inner join role on role.roleid = teaching_staff.roleid where teaching_staff.email = '"+Form1.email_for_form+"' ", con);
                DataSet ds2 = new DataSet();
                adapter.Fill(ds2, "teaching_staff");
                dataGridView2.DataSource = ds2.Tables["teaching_staff"];
                adapter.Dispose();
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if(con.State == ConnectionState.Closed)
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select tstaff_days.tstaff_id as 'Kişisel ID', days.day_name as 'Günlerim' from tstaff_days inner join teaching_staff on teaching_staff.tstaff_id = tstaff_days.tstaff_id inner join days on days.day_id = tstaff_days.day_id where teaching_staff.email ='" + Form1.email_for_form + "'", con);
                DataSet ds2 = new DataSet();
                adapter.Fill(ds2, "tstaff_days");
                dataGridView3.DataSource = ds2.Tables["tstaff_days"];
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
                dialog = MessageBox.Show("Öğretim Görevlileri Geldiği Günler Listesi Excel Dosyasına aktarılacaktır, İşlem Veri Sayısına Göre Hızlı/Yavaş Sürebilir.", "EXCEL'E AKTARMA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialog == DialogResult.Yes)
                {
                    Microsoft.Office.Interop.Excel.Application uyg = new Microsoft.Office.Interop.Excel.Application();
                    uyg.Visible = true;
                    Microsoft.Office.Interop.Excel.Workbook kitap = uyg.Workbooks.Add(System.Reflection.Missing.Value);
                    Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)kitap.Sheets[1];
                    for (int i = 0; i < dataGridView3.Columns.Count; i++)
                    {
                        Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[1, i + 1];
                        myRange.Value2 = dataGridView3.Columns[i].HeaderText;
                    }

                    for (int i = 0; i < dataGridView3.Columns.Count; i++)
                    {
                        for (int j = 0; j < dataGridView3.Rows.Count; j++)
                        {
                            Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                            myRange.Value2 = dataGridView3[i, j].Value;
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

        private void button6_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                if (textBox3.Text.Length !=0 || textBox2.Text.Length != 0)
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "Update teaching_staff set pass ='" + textBox3.Text + "' where pass = '" + textBox2.Text + "' and email = '" + Form1.email_for_form + "' ";
                    command.Connection = con;
                    command.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Şifreniz Değiştirildi, Yeni Şifreniz = '" + textBox3.Text + "'");
                }
                else
                {
                    MessageBox.Show("Lütfen İstenilen Alanları Doldurunuz");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Bir Hata Oluştu" );
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                if (textBox4.Text.Length != 0 || textBox5.Text.Length != 0)
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "Update teaching_staff set rec_quesiton = '" + textBox4.Text + "' where  rec_quesiton = '" + textBox5.Text + "' and  email = '" + Form1.email_for_form + "' ";
                    command.Connection = con;
                    command.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Kurtarma Kelimeniz Değiştirildi, Yeni Kelimeniz = '" + textBox4.Text + "'");
                }
                else
                {
                    MessageBox.Show("Lütfen İstenilen Alanları Doldurunuz");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Kelimeniz Değiştirilemedi Hata Oluştu." );
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            con.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.CommandText = "Select rec_quesiton from teaching_staff where email = '" + Form1.email_for_form + "' ";
            SqlDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                MessageBox.Show(dr[0].ToString());
            }
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel3.Show();
            panel2.Hide();
            panel1.Hide();

        }
    }
}
