   M S B A M L   `   `   `  ����  frontend(    "frontend.Views.HotkeyBindingWindow   5      R NNHotkey.Wpf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a76ffb086700b11cW SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35J 9http://schemas.microsoft.com/winfx/2006/xaml/presentation       5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        N� Title$  快捷键绑定��5       N� 
Background �#F0F4F7 N� 
FontFamily$ Microsoft YaHei)�5       N� FontSize$ 14(�5      .��5	      �� $
��Auto=�6   .��5
   
   �� $��20q�6   .+�5      �� $��0 0 0 20q�6   
������� �� Padding$ 16q�6:    �� CornerRadius$ 8��6G   ._�5      �� .+�5      �� $��绑定快捷键��6    $��16(�6-   $
��Bold"�6;   ���#005A9E$��0,0,0,12q�6b   �� 5       �� Orientation$ 
Horizontal=�6!   $��0 0 0 8q�6:   .+�5      �� $��动作名称：��6    $��80��60   �� 5      -   5         ��Name$ ActionNameBox��5   "   $	��200��69   5      �� 5      $ 
Horizontal=�6!   $��0 0 0 8q�6:   .+�5      �� $��动作类型：��6    $��80��60   �� 5      -   5         ��Name$ ActionComboBox��5   #   $	��200��6;   .@�5      �� $��关机��6+   �� 5      $��重启��6+   �� 5      $��待机��6+   �� 5      $��自定义程序��6+   5      5      �� 5       $ 
Horizontal=�6!   $��0 0 0 8q�6:   .+�5!      �� $��程序路径：��6    $��80��60   �� 5"      -   5        $ CustomPathBox��5"   "   $	��300��69   �� 5#      -   5        $��浏览��5#   !   $��8,0,0,0q�6.   6?   5$      �� 5&      $ 
Horizontal=�6!   $��0 0 0 8q�6:   .+�5'      �� $��修饰键：��6    $��80��6/   �� 5(      -   5        	 ��Name$	 	CtrlCheck��5(   #   $
��Ctrl��66   �� 5)      -   5        $	 AltCheck��5)   #   $	��Alt��65   �� 5*      -   5        $	 
ShiftCheck��5*   #   $��Shift��67   �� 5+      -   5        $	 WinCheck��5+   #   $	��Win��65   5,      �� 5.      $ 
Horizontal=�6!   $��0 0 0 8q�6:   .+�5/      �� $��	主键：��6    $��80��6.   �� 50      -	   5        $ KeyComboBox��50   #   $	��200��68   .@�51      �� $��A��6+   �� 52      $��B��6+   �� 53      $��C��6+   �� 54      $��F1��6+   �� 55      $��F2��6+   �� 56      $��F3��6+   �� 57      $��F4��6+   �� 58      $��F5��6+   5:      5;      �� 5=      -
   5        
 ��Name$
 
BindButton��5=      $��绑定快捷键��61   $	��160��6Z   $��0,10,0,0q�6f   5>      5?      �� 5B      
�������$ 16q�6(   $ 8��65   ._�5C      �� .+�5D      �� $��已绑定快捷键��6    $��16(�6.   $
��Bold"�6<   ���#005A9E$��0,0,0,12q�6c   �� 5E      -   5         ��Name$ HotkeyListView��5E      $	��300��67   $��0,0,0,0q�6D    �� View 5F      �� 5G      .P�5H   "   �� $��名称��61    �� Width$	 120��6=    �� DisplayMemberBinding 6I   �� *
ActionName+�� 5I   "   $��类型��61   $	 100��6=    6I   �� *
ActionType+�� 5J   "   $��	组合键��61   $	 150��6>    �� CellTemplate 5K   &   �� 5L   *   .R�5M   .   �� �� 5N   2   �66   �� *	Modifiers+ �� 5O   2   $	� + ��66    �� 5P   2   �66   �� *Key+5Q   /   5R   +   5T   #   �� 5U   "   $��程序路径��61   $	 200��6?    6K   �� *
CustomPath+�� 5V   "   $��操作��61   $ 80��6=    5W   &   �� 5X   *   .R�5Y   .   �� -   5        $��删除��5Y   5   
 � Tag 6]   �� *
ActionName+5Z   +   5\   #   5]      5_      5`      5a      5c      5d      5e      