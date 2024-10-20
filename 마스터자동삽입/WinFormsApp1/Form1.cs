using System.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using System.IO;
using Microsoft.Data.SqlClient;


namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // ���̺� �̸��� �ش��ϴ� �÷��� ��ȸ�ϴ� �޼���
        private DataTable GetTableColumns(string tableName)
        {
            string connectionString = "Data Source=13.0.4001;Initial Catalog=DCSPS;User ID=sa;Password=ps25gs$;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @TableName";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TableName", tableName);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt; // ��ȸ�� �÷� ����
                }
            }
        }

        // Excel ���� �����͸� �о List<Dictionary<string, object>>�� ��ȯ�ϴ� �޼���
        private List<Dictionary<string, object>> ReadExcelData(string filePath)
        {
            var excelData = new List<Dictionary<string, object>>();

            using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                // ù ��° ���� ���(�÷���)
                List<string> headers = new List<string>();
                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                {
                    headers.Add(worksheet.Cells[1, col].Value.ToString());
                }

                // ������ �б� (�� ��° �����)
                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    var rowData = new Dictionary<string, object>();
                    for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                    {
                        rowData[headers[col - 1]] = worksheet.Cells[row, col].Value;
                    }
                    excelData.Add(rowData);
                }
            }

            return excelData;
        }

        // Excel �����͸� �񵿱�� �����ͺ��̽��� �����ϴ� �޼���
        private async Task InsertExcelDataAsync(string tableName, List<Dictionary<string, object>> excelData)
        {
            string connectionString = "Data Source=your_server_name;Initial Catalog=your_database_name;User ID=your_username;Password=your_password;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();

                foreach (var row in excelData)
                {
                    // INSERT ���� ����
                    string columns = string.Join(",", row.Keys);
                    string parameters = string.Join(",", row.Keys.Select(key => "@" + key));
                    string query = $"INSERT INTO {tableName} ({columns}) VALUES ({parameters})";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // �Ķ���� �߰�
                        foreach (var key in row.Keys)
                        {
                            cmd.Parameters.AddWithValue("@" + key, row[key]);
                        }

                        // �񵿱� INSERT ����
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
        }

        // ���̺� �÷� �ε� ��ư Ŭ�� �̺�Ʈ
        private void btnLoadColumns_Click(object sender, EventArgs e)
        {
            //string tableName = txtTableName.Text; // ����ڰ� �Է��� ���̺� �̸�
            //DataTable columns = GetTableColumns(tableName);
            //
            //// �÷� ������ ����Ʈ�� ǥ��
            //lstColumns.Items.Clear();
            //foreach (DataRow row in columns.Rows)
            //{
            //    lstColumns.Items.Add(row["COLUMN_NAME"].ToString());
            //}
        }

        // ������ ���� ��ư Ŭ�� �̺�Ʈ
        private async void btnInsertData_Click(object sender, EventArgs e)
        {
            //string tableName = txtTableName.Text; // ������ ���̺� �̸�
            //string excelFilePath = txtExcelFilePath.Text; // Excel ���� ���
            //
            //// Excel �����͸� �о List<Dictionary<string, object>>�� ��ȯ
            //var excelData = ReadExcelData(excelFilePath);
            //
            //// �񵿱� ���� ����
            //await InsertExcelDataAsync(tableName, excelData);
            //
            //MessageBox.Show("�����Ͱ� ���������� ���ԵǾ����ϴ�.");
        }

        private void label1_Input(object sender, EventArgs e)
        {

        }
    }
}
