using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Configuration;
//using System.Data;
using System.Data.SqlClient;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: 這行程式碼會將資料載入 'testDataSet.test' 資料表。
            this.testTableAdapter.Fill(this.testDataSet.test);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["WindowsFormsApp1.Properties.Settings.TestConnectionString"].ConnectionString);

            
            Conn.Open(); 

            
            SqlDataReader dr = null;
            SqlCommand cmd = new SqlCommand("select [id],[test_time],[summary],[author] from [test] with(nolock)", Conn);
            dr = cmd.ExecuteReader();   

            //方法一:如果是Web Form可以用GridView再把資料繫結到畫面上
            //GridView1.DataSource = dr;
            //GridView1.DataBind();    =

            //方法二:自己寫迴圈
            while (dr.Read())
            {
                //Response.Write(dr["author"] + "<br />");
                //Label1.Text += dr["author"] + "<br />";
                textBox1.Text += dr["author"] + "/r/n";
            }

                
            if (dr != null)
            {
                cmd.Cancel();
                dr.Close();
            }
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }

        }
    }
}
