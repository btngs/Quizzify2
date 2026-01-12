using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            // 1. Validasi: Semua field harus diisi
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtEmail.Text) ||
                string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtREpassword.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Validasi: Password harus sama dengan Re-password
            if (txtPassword.Text != txtREpassword.Text)
            {
                MessageBox.Show("Passwords do not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. Logika Gender (Sesuai database Anda: 1 = Male, 2 = Female)
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

            // 4. Role Default (Sesuai database Anda: 2 = Student)
            int roleValue = 2;

            // 5. Connection String (Disamakan dengan LoginForm Anda)
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Quizify_DB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Nama kolom disesuaikan dengan screenshot SSMS: FullName, Email, Password, Gender, Role, BirthDate, IsActive
                    string Query = "INSERT INTO [User] (FullName, Email, Password, Gender, Role, BirthDate, IsActive) " +
                                   "VALUES (@name, @email, @password, @gender, @role, @dob, 1)";

                    SqlCommand cmd = new SqlCommand(Query, conn);
                    cmd.Parameters.AddWithValue("@name", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@gender", genderValue);
                    cmd.Parameters.AddWithValue("@role", roleValue); // Menambahkan parameter Role yang tadinya error NULL
                    cmd.Parameters.AddWithValue("@dob", dtpDateOfBirth.Value);

                    // 6. Eksekusi perintah ke database
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Registration Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Kembali ke LoginForm
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

        // Event handler lainnya dibiarkan kosong jika tidak digunakan
        private void txtUsername_TextChanged(object sender, EventArgs e) { }
        private void rbFemale_CheckedChanged(object sender, EventArgs e) { }
        private void rbMale_CheckedChanged(object sender, EventArgs e) { }
    }
}