   M S B A M L   `   `   `  ����C  ?Session2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Session2._1Session2._1  5   	       Session2._1.Director   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Session2._1     5   	   -   5          � Title$#  Экран директора��5   	   $	��450��6!   $	��800��6.   .��5
      �  � RowDefinitions 5   
   �� 5      $	��26* �6   �� 5      $	��23* �6   �� 5      $
��168* �6   .+�5   
   �� $��Период:��6   $��10,10,699,12q�6"   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ 	DateFirst��5   ,   $��114,10,569,12q�6   6,     5   
   -   5        $ 
DateSecond��5   ,   $��248,10,435,12q�6   6,   �� 5   
   -   5         ��Name$ 
SerachDate��5   6   $��
Найти��6   $��387,8,304,9q�6!   6J   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ Equipfails_View��5   (   $��41,35,40,53q�6   $��2��6A      
IsReadOnly$
 True��6N   �� 5   
   -   5         ��Name$ Reason��5   (   $��14,4,470,11q�6   $��1��68   .@�5      �� $��Никакое��6   6/   �� 5      $)��#Естественный износ��6   �� 5      $-��'Механическая поломка��6   �� 5      $)��#Превышена нагрузка��6   �� 5      $>��8Перепады питающего напряжения��6   5      �� 5   
   -   5         ��Name$ ItogTime��5   v   $#��Суммарное время��6   $��17(�6+   $��Center=�69   $��352,4,16,10q�6T   $��1��6i   6v   �� 5   
   -   5        $
 Exit��5      $��
ВЫХОД��6   $��
702,9,16,2q�6/   6C   �� 5   
   -	   5        $ Export��5   7   $��Печать��6   $��542,8,149,9q�6"   6G   5       5!      