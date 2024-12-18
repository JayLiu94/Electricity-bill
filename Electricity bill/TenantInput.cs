using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Electricity_bill
{
    public partial class TenantInputDialog : Window
    {
        public string TenantName { get; private set; }
        public double StartReading { get; private set; }
        public double EndReading { get; private set; }

        public TenantInputDialog()
        {
            InitializeComponent();


            // 設定視窗顯示在中間偏右下
            this.WindowStartupLocation = WindowStartupLocation.Manual;

            // 取得螢幕寬度和高度
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            // 設定視窗位置在中間偏右下 (調整為所需百分比)
            this.Left = (screenWidth * 0.6) - (this.Width / 2);  // 中間偏右
            this.Top = (screenHeight * 0.6) - (this.Height / 2); // 中間偏下

            // 確保使用者可以手動拖動視窗
            this.WindowStyle = WindowStyle.SingleBorderWindow;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            // 驗證使用者輸入
            TenantName = NameTextBox.Text;

            if (string.IsNullOrEmpty(TenantName) ||
                !double.TryParse(StartReadingTextBox.Text, out double startReading) ||
                !double.TryParse(EndReadingTextBox.Text, out double endReading) ||
                endReading < startReading)
            {
                MessageBox.Show("請輸入有效的租戶名稱與讀數！", "輸入錯誤", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            StartReading = startReading;
            EndReading = endReading;
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}


