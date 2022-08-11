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
    
    public partial class Form4 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=schoolDatabase;Integrated Security=True");
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT email,rec_quesiton FROM teaching_staff";
            command.Connection = con;
            command.ExecuteNonQuery();
            SqlDataReader dr = command.ExecuteReader();
            int dön = 1;
            while(dr.Read())
            {
                string email = dr["email"].ToString();
                string rec_question = dr["rec_quesiton"].ToString();
                
                
                if(textBox1.Text == email && textBox2.Text == rec_question)
                {
                    label10.Visible = true;
                    label11.Visible = true;
                    textBox3.Visible = true;
                    button2.Visible = true;
                    checkBox1.Visible = true;
                    
                }
                dön++;
                if(textBox1.Text != email && textBox2.Text != rec_question)
                {
                    MessageBox.Show("Kurtarma Kelimeniz Veya E-Posta Adresi Hatalı, Tekrar Deneyiniz !!");
                }
                
            }
            dr.Close();
            con.Close();
        }
    
            

            private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "Update teaching_staff set pass = '"+textBox3.Text+"' where email = '"+textBox1.Text+"'" ;
            command.Connection = con;
            command.ExecuteNonQuery();
            MessageBox.Show("Parolanız Değiştirilmiştir .");
            this.Close();
            con.Close();
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //checkBox işaretli ise
            if (checkBox1.Checked)
            {
                //karakteri göster.
                textBox3.PasswordChar = '\0';
            }
            //değilse karakterlerin yerine * koy.
            else
            {
                textBox3.PasswordChar = '*';
            }
        }
    }
}
