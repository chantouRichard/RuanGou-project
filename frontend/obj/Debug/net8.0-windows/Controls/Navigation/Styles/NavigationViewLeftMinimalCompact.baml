   M S B A M L   `   `   `  ����C  ?frontend, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:frontend.Controlsfrontend.Controls  5      R NNHotkey.Wpf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a76ffb086700b11cW SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� MergedDictionaries �� Source 0  ,LeftMinimalCompactNavigationViewItemTemplate �� 
TargetType*    $frontend.Controls.NavigationViewItem � RowDefinitions ��Name �� Padding �� CornerRadius � ColumnDefinitions ��Name	    Icon  TextFillColorPrimaryBrush
 ��Name J� Opacity �� RadiusX �� RadiusY  SystemAccentColorSecondary �� Color �Name"   frontend.Controls.SymbolIcon  Name ! NavigationViewItemChevronSize    frontend.Controls.FontIcon   FontSize J� RenderTransformOrigin   Symbol �� Angle#   frontend.Controls.IconElement   
Foreground  TextFillColorPrimary ��Name    	MenuItems-  'System.Windows.Input.KeyboardNavigation   DirectionalNavigation K� IsItemsHost � 	Resources " DefaultNavigationViewItemStyle �� BasedOn �� 
TargetType �� Property �� Value  NullToVisibilityConverter �� 	Converter  �� Triggers! P� Property" P� Value# �� 
TargetName  ControlFillColorDefault$ �� Opacity% N� EnterActions& �� 
TargetName' �� TargetProperty( W� From	) W� To* g� Duration+ N� ExitActions, ]� 
Conditions- �� Property. �� Value�� 5      J 9http://schemas.microsoft.com/winfx/2006/xaml/presentation       :x,http://schemas.microsoft.com/winfx/2006/xaml     5      ,controlsclr-namespace:frontend.Controls  5        5      �� 5   
   $n hpack://application:,,,/frontend;component/Controls/Navigation/Styles/DefaultNavigationViewItemStyle.xaml?�6   �� 5   
   $g apack://application:,,,/frontend;component/Controls/Navigation/Styles/NavigationViewConstants.xaml?�6   %l  &	        7  7  7  �� 5
      "   6K   .R�5   
   �  5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5      �� $ 
MainBorder��5      $��0��5      $	��230��5      $��40��5      $��0q�5      $ 0q�5      
������ #��z��5      $��1q�5      # z 5      ._�5      �  5      �� 5      $��40 �6+   �� 5      $��* �6+   �� 5      $
��Auto �6+   .+�5"      � $��0��6   .+�5#      �� $ IconContentPresenter��5$      $��-1,0,0,0q�5%      $��Center=�5&      $��Center=�5'      #��z	 5(      $|�False��5)      #���  5*      5+      �� 5-      $
 ActiveRectangle��5.      $��0��5/      $��3��50      $��16��51      $��0q�52      $
��Left=�53      $��Center=�54      $	 0.0Q�55      $ 2��56      $ 2��57      ��58      �� 59      # �  6.   5;      �� 5=      $ ElementContentPresenter��5>      $��1��5?      $
��Left=�5@      $��Center=�5A      #��z��5B      #��z��5C      � 5E      $ PART_ChevronGrid��5F      $��2��5G      $��0��5H      $y�	Collapsed=�5I      .+�5K        $ ChevronIcon��5L      $��0q�5M      $��Center=�5N      $��Center=�5O      8   $ 0.5, 0.5&�5Q      $ ChevronDown24=�5R      z�5S      �� 5T   "   $ 0Q�62    5V      �� 5W   "   # �  62   5Y      5Z      5[      5\      �� 5]      $ MenuItemsPresenter��5^      $��1��5_      $|�False��5`      #��z 5a      $ 	Contained=�5b      $	 0.0Q�5c      $��Disabled=�5d      $��Disabled=�5e      $y�	Collapsed=�5f      ��5g      �� 5h      .R�5i      �� $��0q�6%   $
 True��60   5j       5l      ���� 5m      8  '         "   6V   .�5n      ��  � �� 5o      �� 5p   "   "   62   .R�5q   &   �� $ 
MainBorder��5r   )   $��40��5s   )   $��Stretch=�5t   )   $��1q�5u   )   $ 4��5v   )   ._�5w   *   � $��30,0,0,0q�6/   $��Stretch=�6A    5x   .   �� 5y   2   $
��Auto �6C   �� 5z   2   $
��Auto �6C   �� 5{   2   $��* �6C   $	��180��6M   .+�5~   .   �� $
 ActiveRectangle��5   1   $��0��5�   1   $	��3.5��5�   1   $��16��5�   1   $��0q�5�   1   $
��Left=�5�   1   $��Center=�5�   1   $	 0.0Q�5�   1   $ 2��5�   1   $ 2��5�   1   ��5�   2   �� 5�   6   # �  6F   5�   /   �� 5�   .   $ IconContentPresenter��5�   1   $��1��5�   1   $��6,0,3,0q�5�   1   $��Center=�5�   1   $��Center=�5�   1   #��z	 5�   1   $|�False��5�   1   #���  5�   1   y�5�   1   �� *Icon+8  �� 5�   .   $ ElementContentPresenter��5�   1   $��2��5�   1   $��10,0,0,0q�5�   1   $��Stretch=�5�   1   $��Center=�5�   1   #��z��5�   1   #��z��5�   1   5�   +   5�   '     5�   &   P� 5�   *   ! �@  IsActive$
" True��6F   . �5�   .   �� $# ActiveRectangle��65    �@J�Opacity$	 1.0Q�6e   �� 5�   .   $# 
MainBorder��65    � �� 5�   2   �� 5�   6   # �  6F   5�   /   5�   +   P� 5�   *   ! �@J�IsMouseOver$
" True��6I   . �5�   .   �� $# 
MainBorder��65    � �� 5�   2   �� 5�   6   $	$ 0.5Q�6F   # �  6T   5�   /   5�   +   5�   #   5�      5�      5�      5�      5�        5�   
   P� 5�      ! �@  HasMenuItems$
" True��6.   . �5�      �� $# PART_ChevronGrid��6    � y�$ Visible=�6M   �� 5�      $# PART_ChevronGrid��6    � ��$ 40��6H   5�      P� 5�      ! �@  
IsExpanded$
" True��6,   . �5�      �� $# MenuItemsPresenter��6    � y�$ Visible=�6O   % 5�      �� 5�      .��5�      �� .��5�      W� $& MenuItemsPresenter��5�   !   $' (ItemsControl.Opacity)�5�   !   ( 0.05�   !   ) 1.05�   !   $* 00:00:00.167D�5�   !   W� 5�      $& ChevronIcon��5�   !   $7' 1(Control.RenderTransform).(RotateTransform.Angle)�5�   !   ) -1805�   !   $* 00:00:00.167D�5�   !   5�      5�      + 5�      �� 5�      .��5�      �� .��5�      W� $& MenuItemsPresenter��5�   !   $' (ItemsControl.Opacity)�5�   !   ( 1.05�   !   ) 0.05�   !   $* 00:00:00.167D�5�   !   W� 5�      $& ChevronIcon��5�   !   $7' 1(Control.RenderTransform).(RotateTransform.Angle)�5�   !   ) 05�   !   $* 00:00:00.167D�5�   !   5�      5�      5�      P� 5�      ! �@  IsActive$
" True��6*   . �5�      �� $# ActiveRectangle��6    �@J�Opacity$	 1.0Q�6I   �� 5�      $# 
MainBorder��6    � �� 5�      �� 5�      # �  6*   5�      5�      P� 5�      " 6&   Z� ! �@  Icon. �5�      �� $# IconContentPresenter��6    � y�$ 	Collapsed=�6Q   5�      P� 5�      ! �@J�IsMouseOver$
" True��6-   . �5�      �� $# 
MainBorder��6    � �� 5�      �� 5�      $	$ 0.5Q�6*   # �  68   5�      5�      ]� 5�      , 5�      �� 5�      - �@J�IsMouseOver$
. True��67   �� 5�      - �@  IsActive$. False��64   /�5�      �� 5�      $# IconContentPresenter��6    � �� 5      �� 5     # �  6.   5     �� 5     $# ElementContentPresenter��6    � �� 5     �� 5     # �  6.   5     5
     5     5     