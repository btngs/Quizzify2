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
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
            txtREpassword.UseSystemPasswordChar = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtEmail.Text) ||
                string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtREpassword.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtPassword.Text != txtREpassword.Text)
            {
                MessageBox.Show("Passwords do not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int genderValue = 0;
            if (rbMale.Checked)
            {
                genderValue = 1;
            }
            else if (rbFemale.Checked)
            {
                genderValue = 2;
            }
            else
            {
                MessageBox.Show("Please select a gender.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int roleValue = 2;

            using (SqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open();

                    string Query = "INSERT INTO [User] (FullName, Email, Password, Gender, Role, BirthDate, IsActive) " +
                                   "VALUES (@name, @email, @password, @gender, @role, @dob, 1)";

                    SqlCommand cmd = new SqlCommand(Query, conn);
                    cmd.Parameters.AddWithValue("@name", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@gender", genderValue);
                    cmd.Parameters.AddWithValue("@role", roleValue); 
                    cmd.Parameters.AddWithValue("@dob", dtpDateOfBirth.Value);


                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Registration Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoginForm login = new LoginForm();
                        login.Show();
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message);
                }
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e) { }
        private void rbFemale_CheckedChanged(object sender, EventArgs e) { }
        private void rbMale_CheckedChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e){ }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();  
            login.Show();
            this.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Close();
        }
    }
}