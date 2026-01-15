using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Desktop;
using Microsoft.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtPass.UseSystemPasswordChar = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPass.Text) || string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection connection = Koneksi.GetConn())
            {
                try
                {
                    connection.Open();

                    string query = "SELECT * FROM [User] WHERE Email = @email AND Password = @pass";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@email", txtEmail.Text);
                    command.Parameters.AddWithValue("@pass", txtPass.Text);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        bool isActive = Convert.ToBoolean(reader["isActive"]);
                        if (isActive) {
                            MainForm main = new MainForm();
                            main.Show();
                            this.Hide();
                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
        }

        private void txtPass_TextChanged(object sender, EventArgs e){}
        private void txtEmail_TextChanged(object sender, EventArgs e){}
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtPass.UseSystemPasswordChar = !checkBox1.Checked;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                RegisterForm registerForm = new RegisterForm();
                registerForm.Show();
                this.Hide();
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
