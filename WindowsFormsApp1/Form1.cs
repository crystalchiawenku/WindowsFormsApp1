using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//----自己寫的（宣告)----
using System.Configuration;//加入參考
//using System.Data;
using System.Data.SqlClient;
//----自己寫的（宣告)----

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
            // TODO: 這行程式碼會將資料載入 'testDataSet.test' 資料表。您可以視需要進行移動或移除。
            this.testTableAdapter.Fill(this.testDataSet.test);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //=======微軟SDK文件的範本=======

            //----上面已經事先寫好NameSpace --  using System.Web.Configuration; ----     
            // Web.Config設定檔裡面的資料庫連結字串（ConnectionString），此連線名為 testConnectionString
            SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["WindowsFormsApp1.Properties.Settings.TestConnectionString"].ConnectionString);

            //== 第一，連結資料庫。
            Conn.Open();   //---- 開啟連結。這時候才連結DB

            //== 第二，執行SQL指令。
            SqlDataReader dr = null;
            SqlCommand cmd = new SqlCommand("select [id],[test_time],[summary],[author] from [test] with(nolock)", Conn);
            dr = cmd.ExecuteReader();   //---- 執行SQL指令（Select，搜尋、查詢）取出資料

            //==第三，自由發揮，把執行後的結果呈現到畫面上。
            //GridView1.DataSource = dr;
            //GridView1.DataBind();    //--資料繫結

            ////==也可以自己寫迴圈，展示每一筆記錄與其中的欄位==
            while (dr.Read())
            {
                //Response.Write(dr["author"] + "<br />");
                //Label1.Text += dr["author"] + "<br />";
                textBox1.Text += dr["author"] + "/r/n";
            }

            // == 第四，釋放資源、關閉資料庫的連結。       
            if (dr != null)
            {
                cmd.Cancel();
                //----關閉DataReader之前，一定要先「取消」SqlCommand
                //參考資料： http://blog.darkthread.net/blogs/darkthreadtw/archive/2007/04/23/737.aspx
                dr.Close();
            }
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }


        }
    }
}
