using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace okul_otomasyonu
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=schoolDatabase;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }
        public static string email_for_form;


        private void button1_Click(object sender, EventArgs e)
        {
            
            con.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT tstaff_id,roleid,email,pass,reg_id FROM teaching_staff";
            command.Connection = con;
            command.ExecuteNonQuery();
            SqlDataReader dr = command.ExecuteReader();

            while(dr.Read())
            {
                string email = dr["email"].ToString();
                string pass = dr["pass"].ToString();
                string role_id = dr["roleid"].ToString();
                string reg_id = dr["reg_id"].ToString();
                
                while (textBox1.Text == email && textBox2.Text == pass)
                {
                    email_for_form = textBox1.Text;
                    if (role_id == "1"  && reg_id == "1")
                    {
                        Form2 admin_form = new Form2();
                        admin_form.Show();
                        this.Hide();
                    }
                    else if (role_id == "2" && reg_id == "2")
                    {
                        Form5 first_sign = new Form5();
                        first_sign.Show();
                        
                    }
                    else if (role_id == "2" && reg_id == "1")
                    {
                        Form3 tsaff_form = new Form3();
                        tsaff_form.Show();
                        this.Hide();

                    }
                    break;
                }
            }
            
                    dr.Close();
                    con.Close();               
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form4 forgetpass_form = new Form4();
            forgetpass_form.Show();
        }
    }
}
