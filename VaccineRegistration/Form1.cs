using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VaccineRegistration
{
    public partial class Form1 : Form
    {
        SqlCommand cmd;
        SqlDataReader dr;

        private static string myConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
        SqlConnection cn = new SqlConnection(myConnString);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cn.Open();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtID.Text != string.Empty || txtSurname.Text != string.Empty || txtFirstname.Text != string.Empty)
            {
                cmd = new SqlCommand("select * from [dbo].[Patients] where [ID]='" + txtID.Text + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();
                    MessageBox.Show("ID already registered, please try another ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    dr.Close();
                    cmd = new SqlCommand("insert into [dbo].[Patients] values(@ID,@Surname,@Firstname,@Gender,@Province)", cn);
                    cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(txtID.Text));
                    cmd.Parameters.AddWithValue("@Surname", txtSurname.Text);
                    cmd.Parameters.AddWithValue("@Firstname", txtFirstname.Text);
                    cmd.Parameters.AddWithValue("@Gender", cboGender.SelectedItem);
                    cmd.Parameters.AddWithValue("@Province", cboProvince.SelectedItem);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("You have successfully regisered for the vaccine", "Success", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    txtID.Text = "";
                    txtSurname.Text = "";
                    txtFirstname.Text = "";
                    txtID.Focus();
                }
            }
            else
            {
                MessageBox.Show("Please complete all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
