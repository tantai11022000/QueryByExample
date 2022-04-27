using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
namespace QueryByExample
{
    public partial class HomePage : System.Web.UI.Page
    {
        public static List<String> listTableName = new List<string>();
        public static List<String> listColumnName = new List<string>();
        public static Dictionary<int, string> listColumnNameTemp1 = new Dictionary<int, string>();
        public static Dictionary<int, string> listTableNameTemp1 = new Dictionary<int, string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetTableName();
            }
        }

        protected void CheckBoxListTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckBoxListColumn.Items.Clear();
            listTableName.Clear();
            TextBox1.Text = "";
            GridView1.Controls.Clear();
            foreach (ListItem item in CheckBoxListTable.Items)
            {
                if (item.Selected)
                {
                    listTableName.Add(item.Text);
                    
                }
            }
            for (int i = 0; i < listTableName.Count; i++)
            {
                GetColumnName(listTableName[i].ToString());
                
            }
            
        }

        protected void CheckBoxListColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            int position = 0;
            foreach (ListItem item in CheckBoxListColumn.Items)
            {
                
                if (item.Selected)
                {
                    if (!listColumnNameTemp1.ContainsKey(position))
                    {
                        listColumnNameTemp1.Add(position, item.Value.ToString());
                        listTableNameTemp1.Add(position, item.Text.ToString().Split('.')[0]);
                        
                    }
                }
                if (!item.Selected)
                {
                    if (listColumnNameTemp1.ContainsKey(position))
                    {
                        listColumnNameTemp1.Remove(position);
                        listTableNameTemp1.Remove(position);
                    }    
                }    
                position++;
            }

            DataTable dt = new DataTable();

            dt.Columns.Add("Tên Cột", Type.GetType("System.String"));
            dt.Columns.Add("Tên Bảng", Type.GetType("System.String"));
            int positionRow = 0;
            foreach (KeyValuePair<int, string> item in listColumnNameTemp1)
            {
                dt.Rows.Add();
                dt.Rows[positionRow]["Tên Cột"] = item.Value;
                positionRow++;
            }
            positionRow = 0;
            foreach (KeyValuePair<int, string> item in listTableNameTemp1)
            {
                dt.Rows[positionRow]["Tên Bảng"] = item.Value;
                positionRow++;
            }


            
           

            GridView1.DataSource = dt;
            GridView1.DataBind();
            

        }
       
        protected void ButtonClearColumn_Click(object sender, EventArgs e)
        {
            CheckBoxListColumn.Items.Clear();
            listColumnName.Clear();
            listColumnNameTemp1.Clear();
            listTableNameTemp1.Clear();
            GridView1.Controls.Clear();
            for (int i = 0; i < listTableName.Count; i++)
            {
                GetColumnName(listTableName[i].ToString());

            }
            TextBox1.Text = "";
        }
       

        protected void ButtonQuery_Click(object sender, EventArgs e)
        {
            lbltxt.Text = "";
            if (GridView1.Controls.Count <= 0)
            {
                lbltxt.Text = "Bạn chưa chọn dữ liệu";
                return;
            }    
            string mess = "";
            string groupBy = "";
            string having = "";
            string orderBy = "";
            string tableName = string.Join(", ", listTableName);
            String columnName = "";
            mess = "SELECT ";
            String dk = "";
            Boolean isGetGroupBy = false;
            Boolean isGetHaving = false;
            Boolean isFirstDK = true;
            Boolean isFirstHaving = true;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                TextBox strBang = new TextBox();
                TextBox strCot = new TextBox();
                TextBox dieuKien = (TextBox)GridView1.Rows[i].Cells[2].FindControl("TextBoxDieuKien");
                DropDownList function = (DropDownList)GridView1.Rows[i].Cells[0].FindControl("DropDownList1");
                DropDownList sort = (DropDownList)GridView1.Rows[i].Cells[1].FindControl("DropDownList2");
                Boolean isHaving = false;
                

                strBang.Text = GridView1.Rows[i].Cells[4].Text;
                strCot.Text = GridView1.Rows[i].Cells[3].Text;

                if (!function.SelectedItem.Value.ToString().Equals("")) // Xử lý xem có chọn function nào không để sinh tên tạm cho trường đã chọn
                {
                    isGetGroupBy = true;
                    isHaving = true;
                    columnName = function.SelectedItem.Value.ToString() + "(" + strBang.Text.ToString() + "." + strCot.Text.ToString() + ") AS "
                        + function.SelectedItem.Value.ToString() + strCot.Text.ToString();
                    if (!sort.SelectedItem.Value.ToString().Equals("")) // xử lý xem có chọn sort không nhưng ở trường hợp là có đặt tên temp cho trường đã chọn
                    {
                        if (!orderBy.Equals(""))
                            orderBy += ", " + function.SelectedItem.Value.ToString() + strCot.Text.ToString() + " " + sort.SelectedItem.Value.ToString();
                        else
                            orderBy += " " + function.SelectedItem.Value.ToString() + strCot.Text.ToString() + " " + sort.SelectedItem.Value.ToString();
                    }

                }

                else
                {
                    columnName = strBang.Text.ToString() + "." + strCot.Text.ToString();
                    if (!groupBy.Equals(""))
                        groupBy += ", " + columnName;
                    else
                        groupBy += " " + columnName;
                    if (!sort.SelectedItem.Value.ToString().Equals("")) //xử lý xem có chọn sort không nhưng ở trường hợp bình thường
                    {
                        if (!orderBy.Equals(""))
                            orderBy += ", " + columnName +" " + sort.SelectedItem.Value.ToString();
                        else
                            orderBy += " " + columnName + " " + sort.SelectedItem.Value.ToString();
                    }
                }
                if (dieuKien.Text.ToString() != "")
                {
                    if (isHaving)
                    {
                        isGetHaving = true;
                        if (isFirstHaving)
                        {
                            having += function.SelectedItem.Value.ToString() + "(" + strBang.Text.ToString() + "." + strCot.Text.ToString() + ") " + dieuKien.Text.ToString();
                            isFirstHaving = false;
                        }
                        else
                            having += " AND " + function.SelectedItem.Value.ToString() + "(" + strBang.Text.ToString() + "." + strCot.Text.ToString() + ") " + dieuKien.Text.ToString();
                    }    
                    else 
                    {
                        if (isFirstDK)
                        {
                            dk += strBang.Text.ToString() + "." + strCot.Text.ToString() + " " + dieuKien.Text.ToString();
                            isFirstDK = false;
                        }
                        else
                            dk += " AND " + strBang.Text.ToString() + "." + strCot.Text.ToString() + " " + dieuKien.Text.ToString();
                    }
                }


                if (i < GridView1.Rows.Count - 1)
                {
                    columnName += ", ";
                }
                mess += columnName;

            }
            // xử lý các khóa chính khóa ngoại nếu có 2 bảng được chọn trở lên
            String where = "";
            if (listTableName.Count >= 2)
            {
                string query = "";
                Boolean isFirst = true;
                for (int i = 0; i < listTableName.Count - 1; i++)
                {
                    for (int j = i + 1; j < listTableName.Count; j++)
                    {
                        query = "EXEC SP_GETKEYBYTABLE @Table1 = '" + listTableName[i].ToString()
                            + "' , @Table2 = '" + listTableName[j].ToString() + "'";
                        string temp = GetKeyByTable(query);
                        if (isFirst && temp != "")
                        {
                            where += temp;
                            isFirst = false;
                        }
                        else
                        {
                            if (temp != "")
                                where += " AND " + temp;
                        }
                    }
                }
            }
            if (!isFirstDK) // kt xem có ít nhất 1 đk đã tồn tại
            {
                where += " AND ";
            }

            if (!where.Equals("") || !dk.Equals(""))
            {
                where = " WHERE " + where;
            }
            if (isGetGroupBy)
            {
                if (isGetHaving)
                {
                    if (!orderBy.Equals(""))
                        mess += " FROM " + tableName + where + dk + " Group By" + groupBy + " Having "+ having +" Order By" + orderBy;
                    else
                        mess += " FROM " + tableName + where + dk + " Group By" + groupBy + " Having " + having;
                }
                else
                {
                    if (!orderBy.Equals(""))
                        mess += " FROM " + tableName + where + dk + " Group By" + groupBy + " Order By" + orderBy;
                    else
                        mess += " FROM " + tableName + where + dk + " Group By" + groupBy;
                }
            }
            else
            {
                if (!orderBy.Equals(""))
                    mess += " FROM " + tableName + where + dk + " Order BY" + orderBy;
                else
                    mess += " FROM " + tableName + where + dk;
            }
            TextBox1.Text = mess;
            
        }

        protected void ButtonReport_Click(object sender, EventArgs e)
        {
            lbltxt.Text = "";
            String query = TextBox1.Text;
            if (query == "")
            {
                lbltxt.Text = "Bạn chưa chọn query";
                return;
            }    
                
            try
            {
                SqlConnection cnn = new SqlConnection();
                SqlCommand cmd = new SqlCommand();
                String connect = "Data Source=DESKTOP-3R9F5Q4;Initial Catalog=QLDSV_HTC;Integrated Security=True";
                cnn.ConnectionString = connect;
                cnn.Open();
                DataSet dt = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(query, cnn);
                da.Fill(dt);
                cnn.Close();
                Session["title"] = TextBoxNhapTieuDe.Text;
                Session["query"] = query;
                Response.Redirect("Report.aspx");
                Server.Execute("Report.aspx");
            }
            catch
            {
                lbltxt.Text = "Đã có lỗi trong câu Query";
                TextBox1.Focus();
            }
        }
        private void GetTableName()
     {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager
                        .ConnectionStrings["dbstring"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME NOT LIKE 'sys%' AND TABLE_NAME NOT LIKE 'MS%' ORDER BY TABLE_NAME";

                    cmd.CommandText = query;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            ListItem item = new ListItem();
                            item.Text = sdr["TABLE_NAME"].ToString();
                            CheckBoxListTable.Items.Add(item);
                            CheckBoxListTable.RepeatColumns = 5;
                            CheckBoxListTable.AutoPostBack = true;
                        }
                        
                    }
                    conn.Close();
                }
            }
        }
        private void GetColumnName(String tableName)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager
                        .ConnectionStrings["dbstring"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + tableName + "' AND COLUMN_NAME NOT LIKE 'rowguid%'";

                    cmd.CommandText = query;
                    cmd.Connection = conn;
                    conn.Open();
                    
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            ListItem item = new ListItem();
                            item.Text = tableName.ToString() + "." +sdr["COLUMN_NAME"].ToString();
                            item.Value = sdr["COLUMN_NAME"].ToString();

                            CheckBoxListColumn.Items.Add(item);
                            CheckBoxListColumn.RepeatColumns = 5;
                            CheckBoxListColumn.AutoPostBack = true;
                            
                        }
                    }
                    conn.Close();
                }
            }
        }
        private string GetKeyByTable(string query)
        {
            string key = "";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager
                        .ConnectionStrings["dbstring"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    cmd.Connection = conn;
                    conn.Open();
                    Boolean isFirst = true;
                    
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            
                           
                            
                            if (isFirst)
                                {
                                    key += sdr["table_name"].ToString() + "." + sdr["constraint_column_name"].ToString() 
                                        + " = " + sdr["referenced_object"].ToString() + "." + sdr["referenced_column_name"].ToString();
                                    isFirst = false;
                                }                                       
                            else
                                    key += " AND "+sdr["table_name"].ToString() + "." + sdr["constraint_column_name"].ToString()
                                        + " = " + sdr["referenced_object"].ToString() + "." + sdr["referenced_column_name"].ToString();
                        }
                    }
                    conn.Close();
                    return key;
                }
            }
        }
    }
}