using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace json_csv_convert
{
    class FileIO
    {
        public static int DO_NOTHING = 0;
        public static int OPEN = 1;

        public static void SaveTo(DataTable data, int fileaction)
        {
            String prefix = "converted";

            String DefaultSaveName = prefix + "_" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss") + ".xlsx";

            SaveFileDialog sfd = new SaveFileDialog();
            //sfd.Filter = "Excel Spreadsheet|*.xlsx|Comma Separated Values|*.csv";
            sfd.Filter = "Excel Spreadsheet|*.xlsx";
            sfd.RestoreDirectory = true;
            sfd.Title = "Save As...";
            sfd.FileName = DefaultSaveName;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                String path = Path.GetFullPath(sfd.FileName);

                SaveToXLSX(data, path);

                if (fileaction == OPEN)
                {
                    System.Diagnostics.Process.Start(path);
                }
            }
        }

        static void SaveToXLSX(DataTable data, String filepath)
        {
            ExcelPackage package = new ExcelPackage(new FileInfo(filepath));
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Converted Data");

            int title_colindex = 1;
            foreach (DataColumn c in data.Columns)
            {
                worksheet.Cells[1, title_colindex].Value = c.ColumnName;
                worksheet.Column(title_colindex).AutoFit();
                title_colindex++;
            }

            worksheet.Row(1).Style.Font.Bold = true;

            int rowindex = 2;
            foreach (DataRow r in data.Rows)
            {
                int colindex = 1;
                foreach (var v in r.ItemArray)
                {
                    worksheet.Cells[rowindex, colindex].Value = v.ToString();

                    colindex++;
                }
                rowindex++;
            }

            title_colindex = 1;
            foreach (DataColumn c in data.Columns)
            {
                worksheet.Column(title_colindex).AutoFit();
                title_colindex++;
            }

            package.Save();
        }
    }
}
