   M S B A M L   `   `   `  ����  Electricity bill!    Electricity_bill.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Electricity bill Tool��5   	   $	��450��6'   $	��800��64   .��5      � $��10q�6   .+�5   
   �� $��每度電費率：��6   $	��Top=�6"   $
��Left=�6:   $��	10,10,0,0q�6U   �� 5   
   -   5        $��
RatePerKwh��5      $	��100��6$   $
��Left=�60   $��
120,10,0,0q�6K   $	��Top=�6_   &   System.Windows.Controls.DataGrid  5   
   -   5        $��
TenantGrid��5         AutoGenerateColumns$ False��6%      CanUserAddRows$
 True��6A   $��10,50,10,150q�6W      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 租戶名稱��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6L   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   �� *Name+6L     5      $ 起始度數��6%   $ * 6T    63   �� *StartReading+6T     5      $ 當前度數��6%   $ * 6R    63   �� *
EndReading+6R     5      $ 用電度數 (度)��6%   $ * 6Q      
IsReadOnly$
 True��6[    67   �� *Usage+6[     5      $ 應繳電費 (元)��6%   $ * 6R   $
 True��6\    67   �� *Amount+6\   5      �� 5   
   -   5        $��計算電費��5      $��Right=�6    $��	0,10,10,0q�6<   $	��Top=�6O   $	��100��6g   6s   �� 5   
   -   5        $��匯出到 Excel��5      $��Right=�6%   $��
0,10,120,0q�6A   $	��Top=�6U   $	��100��6m   6y   �� 5   
   -   5        $��新增租戶��5      $
��Left=�6    $��	10,31,0,0q�6;   $	��Top=�6N   $	��100��6f   6r   �� 5   
   $��總用電量：��6   $��Bottom=�6!   $
��Left=�6<   $��	10,0,0,50q�6W   �� 5   
   -   5        $��
TotalUsage��5      $��Bottom=�6&   $
��Left=�6A   $��
100,0,0,50q�6\   �� 5   
   $��總電費：��6   $��Bottom=�6    $
��Left=�6;   $��	10,0,0,20q�6V   �� 5   
   -   5        $��	TotalCost��5      $��Bottom=�6%   $
��Left=�6@   $��
100,0,0,20q�6[   5       5!      