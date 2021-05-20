using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MakeUpParser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ReadRepository _site = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var items = _site.GetItems(10, true, true);

            DataTable table = new DataTable("Парфюмерия");
            DataColumn column;
            DataRow row;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ссылка на товар";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Название";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "Цена";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Характеристики";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Описание";
            table.Columns.Add(column);

            foreach (var item in items)
            {
                row = table.NewRow();
                row[0] = item.GetLink();
                row[1] = item.GetName();
                row[2] = item.GetPrice();
                row[3] = item.GetInfo()?.Characteristic ?? "";
                row[4] = item.GetInfo()?.Description ?? "";

                table.Rows.Add(row);
            }

            XLWorkbook workbook = new XLWorkbook();
            workbook.Worksheets.Add(table);
            workbook.SaveAs(@"E:\test.xlsx");
        }
    }
}
