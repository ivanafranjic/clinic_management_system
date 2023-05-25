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

namespace Klinika
{
    public partial class Prijava : Form
    {
        public Prijava()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            RoleCb.SelectedIndex = 0;
            UNameTb.Text = "";
            PassTb.Text = "";
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\KlinikaDB.mdf;Integrated Security=True;Connect Timeout=30");

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (RoleCb.SelectedIndex == -1)
            {
                MessageBox.Show("Odaberi");
            }
            else if (RoleCb.SelectedIndex == 0)
            {
                if (UNameTb.Text == "" || PassTb.Text == "")
                {
                    MessageBox.Show("Upišite korisničko ime i lozinku.");
                }
                else if (UNameTb.Text == "Admin" && PassTb.Text == "Password")
                {
                    Pacijenti Obj = new Pacijenti();
                    Obj.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Pogrešno korisničko ime ili lozinka.");
                }
            }
            else if (RoleCb.SelectedIndex == 1)
            {
                    if (UNameTb.Text == "" || PassTb.Text == "")
                    {
                        MessageBox.Show("Upišite korisničko ime i lozinku.");
                    }
                    else
                    {
                        Con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from DoctorTbl where DocName='" + UNameTb.Text + "' and DocPass='" + PassTb.Text + "'", Con);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows[0][0].ToString() == "1")
                        {
                            Recepti Obj = new Recepti();
                            Obj.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Liječnik nije pronađen");
                        }
                        Con.Close();
                    }
            }
           
            else
            {
               if (RoleCb.SelectedIndex == 0)
                {
                    if (UNameTb.Text == "" || PassTb.Text == "")
                    {
                        MessageBox.Show("Upišite korisničko ime i lozinku.");
                    }
                    else
                    {
                        Con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from ReceptionistTbl where RecepName='" + UNameTb.Text + "' and RecepPass='" + PassTb.Text + "'", Con);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows[0][0].ToString() == "1")
                        {
                            Pocetna Obj = new Pocetna();
                            Obj.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Djelatnik nije pronađen");
                        }
                        Con.Close();
                    }
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
    
        
 
