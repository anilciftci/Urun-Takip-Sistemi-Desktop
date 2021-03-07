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

namespace TeknikServis.Formlar
{
    public partial class FrmCariİller : Form
    {
        public FrmCariİller()
        {
            InitializeComponent();
        }
        DbTeknikServisEntities db = new DbTeknikServisEntities();
        SqlConnection bgl = new SqlConnection(@"Data Source=DESKTOP-VQIM9S3\SQLEXPRESS;Initial Catalog=DbTeknikServis;Integrated Security=True");
        
        private void FrmCariİller_Load(object sender, EventArgs e)
        {
           // chartControl1.Series["Series 1"].Points.AddPoint("Ankara", 22);
          //  chartControl1.Series["Series 1"].Points.AddPoint("İstanbul ", 14);
            //chartControl1.Series["Series 1"].Points.AddPoint("Bursa", 7);
            //chartControl1.Series["Series 1"].Points.AddPoint("Kocaeli", 24);
            //chartControl1.Series["Series 1"].Points.AddPoint("Ağrı", 34);
            //chartControl1.Series["Series 1"].Points.AddPoint("Trabzon", 12);


            gridControl1.DataSource = db.TBLCARI.OrderBy(x => x.IL).
                GroupBy(y => y.IL).
                Select(z => new { İl = z.Key, TOPLAM = z.Count() }).ToList();

            bgl.Open(); //asp mimarisi 46*
            SqlCommand komut = new SqlCommand("Select IL, COUNT(*) FROM TBLCARI group by IL ", bgl);
            SqlDataReader dr = komut.ExecuteReader();

            while (dr.Read())
            {
                chartControl1.Series["Series 1"].Points.AddPoint(Convert.ToString(dr[0]), int.Parse(dr[1].ToString()));
            }
            bgl.Close();

        }
    }
}
