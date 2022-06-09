using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.OleDb;

namespace LibrarySystem
{
    public partial class Form3 : Form
    {
        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\BookDatabase2.mdb");
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = "Select t.id as ID, s.student_fname as Firstname, s.student_lname as LastName, b.Title as Title, b.category as Category, b.Author as Author, t.date_borrowed as DateBorrowed, t.return_date as ReturnDate, t.penalty as Penalty, t.status as Status from ((transactions t inner join students s on t.student_id = s.student_id) inner join books b on b.AccessionNumber = t.book_id) order by t.id desc";
            OleDbDataAdapter da = new OleDbDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CrystalReport1 crp = new CrystalReport1();
            crp.SetDataSource(dt);
            crystalReportViewer1.ReportSource = crp;
            crystalReportViewer1.Refresh();
            connection.Close();
        }
    }
}
