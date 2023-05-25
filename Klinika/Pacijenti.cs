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
    public partial class Pacijenti : Form
    {
        public Pacijenti()
        {
            InitializeComponent();
            DisplayPat();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\KlinikaDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplayPat()

        {
            Con.Open();
            string Query = "Select * from PatientTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            PatientDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        int Key = 0;

        public static Pacijenti Obj { get; internal set; }

        private void Clear()
        {
            PatName.Text = "";
            PatPhoneTb.Text = "";
            PatAddTb.Text = "";
            PatAlergTb.Text = "";
            PatGenCb.SelectedIndex = 0;
            PatHivCb.SelectedIndex = 0;
            Key = 0;
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (PatName.Text == "" || PatAlergTb.Text == "" || PatPhoneTb.Text == "" || PatAddTb.Text == "" || PatGenCb.SelectedIndex == -1 || PatHivCb.SelectedIndex == -1)
            {
                MessageBox.Show("Molimo unesite sve podatke.");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into PatientTbl(PatName,PatGen,PatDOB, PatAdd,PatPhone , PatCovid, PatAll)values(@PN,@PG,@PD,@PA, @PP, @PH, @PAl)", Con);
                    cmd.Parameters.AddWithValue("@PN", PatName.Text);
                    cmd.Parameters.AddWithValue("@PG", PatGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PD", PatDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@PA", PatAddTb.Text);
                    cmd.Parameters.AddWithValue("@PP", PatPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@PH", PatHivCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PAl", PatAlergTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Pacijent dodan");
                    Con.Close();
                    DisplayPat();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {

            if (PatName.Text == "" || PatAlergTb.Text == "" || PatPhoneTb.Text == "" || PatAddTb.Text == "" || PatGenCb.SelectedIndex == -1 || PatHivCb.SelectedIndex == -1)
            {
                MessageBox.Show("Molimo unesite sve podatke.");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update PatientTbl set PatName=@PN,PatDOB=@PD,PatGen=@PG,PatAdd=@PA,PatPhone=@PP,PatAll=@PAl, PatCovid=@PH where PatId=@PKey", Con);
                    cmd.Parameters.AddWithValue("@PN", PatName.Text);
                    cmd.Parameters.AddWithValue("@PD", PatDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@PG", PatGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PH", PatHivCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PP", PatPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@PA", PatAddTb.Text);
                    cmd.Parameters.AddWithValue("@PAl", PatAlergTb.Text);
                    cmd.Parameters.AddWithValue("@PKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Podaci ažurirani.");
                    Con.Close();
                    DisplayPat();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Molimo odaberite pacijenta.");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from PatientTbl where PatId = @PKey", Con);

                    cmd.Parameters.AddWithValue("@PKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Pacijent izbrisan.");
                    Con.Close();
                    DisplayPat();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        
        private void PatientDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PatName.Text = PatientDGV.SelectedRows[0].Cells[1].Value.ToString(); 
            PatGenCb.SelectedItem = PatientDGV.SelectedRows[0].Cells[2].Value.ToString();
            PatDOB.Text = PatientDGV.SelectedRows[0].Cells[3].Value.ToString();
            PatAddTb.Text = PatientDGV.SelectedRows[0].Cells[4].Value.ToString();
            PatPhoneTb.Text = PatientDGV.SelectedRows[0].Cells[5].Value.ToString();
            PatHivCb.SelectedItem = PatientDGV.SelectedRows[0].Cells[6].Value.ToString();
            PatAlergTb.Text = PatientDGV.SelectedRows[0].Cells[7].Value.ToString();
            if (PatName.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(PatientDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}
