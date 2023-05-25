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

namespace Klinika
{
    public partial class Prijem : Form
    {
        public Prijem()
        {
            InitializeComponent();
            DisplayRec();

        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\KlinikaDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void DelBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Molimo odaberite korisnika.");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from ReceptionistTbl where RecepId = @RKey", Con);
                    
                    cmd.Parameters.AddWithValue("@RKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Korisnik izbrisan");
                    Con.Close();
                    DisplayRec();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }

        private void DisplayRec() {
            Con.Open();
            string Query = "Select * from ReceptionistTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ReceptionistDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (RNameTbl.Text == "" || RPassword.Text == "" || RPhoneTbl.Text == "" || RAddressTbl.Text == "")
            {
                MessageBox.Show("Molimo unesite sve podatke.");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update ReceptionistTbl set RecepName=@RN,RecepPhone=@RP,RecepAdd=@RA,RecepPass=@RPA where RecepId=@RKey", Con);
                    cmd.Parameters.AddWithValue("@RN", RNameTbl.Text);
                    cmd.Parameters.AddWithValue("@RP", RPhoneTbl.Text);
                    cmd.Parameters.AddWithValue("@RA", RAddressTbl.Text);
                    cmd.Parameters.AddWithValue("@RPA", RPassword.Text);
                    cmd.Parameters.AddWithValue("@RKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Podaci ažurirani");
                    Con.Close();
                    DisplayRec();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
    }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (RNameTbl.Text == "" || RPassword.Text == "" || RPhoneTbl.Text == "" || RAddressTbl.Text == "")
            {
                MessageBox.Show("Molimo unesite sve podatke.");
            }
            else {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ReceptionistTbl(RecepName,RecepPhone,RecepAdd,RecepPass)values(@RN,@RP,@RA,@RPA)", Con);
                    cmd.Parameters.AddWithValue("@RN", RNameTbl.Text);
                    cmd.Parameters.AddWithValue("@RP", RPhoneTbl.Text);
                    cmd.Parameters.AddWithValue("@RA", RAddressTbl.Text);
                    cmd.Parameters.AddWithValue("@RPA", RPassword.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Korisnik dodan");
                    Con.Close();
                    DisplayRec();
                    Clear();
                }
                catch (Exception Ex) {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        int Key = 0;
        private void ReceptionistDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            RNameTbl.Text = ReceptionistDGV.SelectedRows[0].Cells[1].Value.ToString();
            RPhoneTbl.Text = ReceptionistDGV.SelectedRows[0].Cells[2].Value.ToString();
            RAddressTbl.Text = ReceptionistDGV.SelectedRows[0].Cells[3].Value.ToString();
            RPassword.Text = ReceptionistDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (RNameTbl.Text == "")
            {
                Key = 0;
            }
            else {
                Key = Convert.ToInt32(RNameTbl.Text = ReceptionistDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
        private void Clear() {
            RNameTbl.Text = "";
            RPassword.Text = "";
            RPhoneTbl.Text = "";
            RAddressTbl.Text = "";
            Key = 0;
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
