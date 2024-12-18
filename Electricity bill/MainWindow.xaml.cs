using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Linq;


using System;
using System.Collections.ObjectModel;
using System.Windows;

using ClosedXML.Excel;
using System.IO;

namespace Electricity_bill
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Tenant> Tenants { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Tenants = new ObservableCollection<Tenant>();
            TenantGrid.ItemsSource = Tenants; // Bind DataGrid
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 讀取每度電費率
                if (!double.TryParse(RatePerKwh.Text, out double ratePerKwh) || ratePerKwh <= 0)
                {
                    MessageBox.Show("請輸入有效的電費率！", "錯誤", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                double totalUsage = 0;
                double totalCost = 0;

                // 計算每位租戶的電費
                foreach (var tenant in Tenants)
                {
                    if (tenant.Usage <= 0)
                    {
                        tenant.Amount = 0;
                        continue;
                    }

                    tenant.Amount = tenant.Usage * ratePerKwh;
                    totalUsage += tenant.Usage;
                    totalCost += tenant.Amount;
                }

                // 更新總用電量與總電費
                TotalUsage.Text = $"{totalUsage} 度";
                TotalCost.Text = $"{totalCost:C2} 元";

                TenantGrid.Items.Refresh(); // 刷新 DataGrid
            }
            catch (Exception ex)
            {
                MessageBox.Show($"計算時發生錯誤：{ex.Message}", "錯誤", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddTenantButton_Click(object sender, RoutedEventArgs e)
        {
            // 開啟輸入對話框
            var inputDialog = new TenantInputDialog();
            if (inputDialog.ShowDialog() == true)
            {
                // 新增租戶並自動計算用電度數
                Tenants.Add(new Tenant
                {
                    Name = inputDialog.TenantName,
                    StartReading = inputDialog.StartReading,
                    EndReading = inputDialog.EndReading,
                    Amount = 0 // 稍後計算電費
                });
            }
        }


        private void ExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 創建一個新的工作簿
                var workbook = new XLWorkbook();

                // 添加工作表
                var worksheet = workbook.AddWorksheet("租戶資料");

                // 設定標題行
                var headerRow = worksheet.Row(1);
                worksheet.Cell(1, 1).Value = "租戶名稱";
                worksheet.Cell(1, 2).Value = "起始度數";
                worksheet.Cell(1, 3).Value = "當前度數";
                worksheet.Cell(1, 4).Value = "用電度數 (度)";
                worksheet.Cell(1, 5).Value = "應繳電費 (元)";


                // 設置標題行的背景顏色為淺藍色
                headerRow.Style.Fill.SetBackgroundColor(XLColor.LightBlue);
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // 填入租戶資料
                int row = 2;
                foreach (var tenant in Tenants)
                {

                    worksheet.Cell(row, 1).Value = tenant.Name;
                    worksheet.Cell(row, 2).Value = tenant.StartReading.ToString("0.00"); // 顯示兩位小數
                    worksheet.Cell(row, 3).Value = tenant.EndReading.ToString("0.00"); 
                    worksheet.Cell(row, 4).Value = tenant.Usage.ToString("0.00"); 
                    worksheet.Cell(row, 5).Value = tenant.Amount.ToString("0.00"); 


                    // 設置交替行顏色
                    if (row % 2 == 0) // 偶數行
                    {
                        worksheet.Row(row).Style.Fill.SetBackgroundColor(XLColor.FromArgb(240, 240, 240)); // 淺灰色
                    }
                    else // 奇數行
                    {
                        worksheet.Row(row).Style.Fill.SetBackgroundColor(XLColor.White); // 白色
                    }
                    row++;
                }



                // 微調特定欄位的寬度，以避免擠到字
                worksheet.Column(1).Width = 20;
                worksheet.Column(2).Width = 20;
                worksheet.Column(3).Width = 20;
                worksheet.Column(4).Width = 20; // 用電度數 (度) 欄寬
                worksheet.Column(5).Width = 20; // 應繳電費 (元) 欄寬
                // 設定檔案儲存的路徑
                var saveFileDialog = new Microsoft.Win32.SaveFileDialog
                {
                    Filter = "Excel Files (*.xlsx)|*.xlsx",
                    FileName = "租戶用電.xlsx"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    string filePath = saveFileDialog.FileName;

                    // 儲存工作簿為 Excel 檔案
                    workbook.SaveAs(filePath);

                    MessageBox.Show("匯出成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"匯出過程中發生錯誤：{ex.Message}", "錯誤", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class Tenant
    {
        public string Name { get; set; }           // 租戶名稱
        public double StartReading { get; set; }   // 起始讀數
        public double EndReading { get; set; }     // 結束讀數
        public double Usage => EndReading - StartReading;  // 用電度數
        public double Amount { get; set; }         // 應繳電費
    }

}


