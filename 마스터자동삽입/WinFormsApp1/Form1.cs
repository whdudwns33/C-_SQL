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

        // 테이블 이름에 해당하는 컬럼을 조회하는 메서드
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
                    return dt; // 조회된 컬럼 정보
                }
            }
        }

        // Excel 파일 데이터를 읽어서 List<Dictionary<string, object>>로 변환하는 메서드
        private List<Dictionary<string, object>> ReadExcelData(string filePath)
        {
            var excelData = new List<Dictionary<string, object>>();

            using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                // 첫 번째 행은 헤더(컬럼명)
                List<string> headers = new List<string>();
                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                {
                    headers.Add(worksheet.Cells[1, col].Value.ToString());
                }

                // 데이터 읽기 (두 번째 행부터)
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

        // Excel 데이터를 비동기로 데이터베이스에 삽입하는 메서드
        private async Task InsertExcelDataAsync(string tableName, List<Dictionary<string, object>> excelData)
        {
            string connectionString = "Data Source=your_server_name;Initial Catalog=your_database_name;User ID=your_username;Password=your_password;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();

                foreach (var row in excelData)
                {
                    // INSERT 쿼리 생성
                    string columns = string.Join(",", row.Keys);
                    string parameters = string.Join(",", row.Keys.Select(key => "@" + key));
                    string query = $"INSERT INTO {tableName} ({columns}) VALUES ({parameters})";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // 파라미터 추가
                        foreach (var key in row.Keys)
                        {
                            cmd.Parameters.AddWithValue("@" + key, row[key]);
                        }

                        // 비동기 INSERT 실행
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
        }

        // 테이블 컬럼 로드 버튼 클릭 이벤트
        private void btnLoadColumns_Click(object sender, EventArgs e)
        {
            //string tableName = txtTableName.Text; // 사용자가 입력한 테이블 이름
            //DataTable columns = GetTableColumns(tableName);
            //
            //// 컬럼 정보를 리스트에 표시
            //lstColumns.Items.Clear();
            //foreach (DataRow row in columns.Rows)
            //{
            //    lstColumns.Items.Add(row["COLUMN_NAME"].ToString());
            //}
        }

        // 데이터 삽입 버튼 클릭 이벤트
        private async void btnInsertData_Click(object sender, EventArgs e)
        {
            //string tableName = txtTableName.Text; // 선택한 테이블 이름
            //string excelFilePath = txtExcelFilePath.Text; // Excel 파일 경로
            //
            //// Excel 데이터를 읽어서 List<Dictionary<string, object>>로 변환
            //var excelData = ReadExcelData(excelFilePath);
            //
            //// 비동기 삽입 시작
            //await InsertExcelDataAsync(tableName, excelData);
            //
            //MessageBox.Show("데이터가 성공적으로 삽입되었습니다.");
        }

        private void label1_Input(object sender, EventArgs e)
        {

        }
    }
}
