   M S B A M L   `   `   `  ����C  ?frontend, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:frontend.Controlsfrontend.Controls  5      R NNHotkey.Wpf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a76ffb086700b11cW SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� MergedDictionaries �� Source (  $LeftFluentNavigationViewItemTemplate �� 
TargetType*    $frontend.Controls.NavigationViewItem ��Name  PaneFluentButtonWidth  PaneFluentButtonHeight �� Padding �� CornerRadius ��Name J� Opacity �� RadiusX	 �� RadiusY  SystemAccentColorSecondary
 �� Color � RowDefinitions ��Name    Icon   NavigationViewFluentIconSize  TextFillColorPrimaryBrush �Name �� Triggers P� Property P� Value �� 
TargetName �� Property �� Value  ControlFillColorDefault N� EnterActions �� 
TargetName �� TargetProperty W� From	 W� To g� Duration N� ExitActions �� Opacity ]� 
Conditions �� Property �� Value  TextFillColorPrimary�� 5      J 9http://schemas.microsoft.com/winfx/2006/xaml/presentation       :x,http://schemas.microsoft.com/winfx/2006/xaml     5      ,controlsclr-namespace:frontend.Controls  5        5      �� 5   
   $g apack://application:,,,/frontend;component/Controls/Navigation/Styles/NavigationViewConstants.xaml?�6   %g  &	        7  7  7  �� 5	      "   6C   .R�5
   
   �� $ 
MainBorder��5      8��  8�� $ 4q�5      $��Stretch=�5      
������ #��z��5      $��1q�5      $ 4��5      ._�5      � .+�5      �� $ ActiveRectangle��5      $��4��5      $��24��5      $��-4,0,0,0q�5      $
��Left=�5      $��Center=�5      $	 0.0Q�5      $ 2��5      $	 2��5      ��5      �� 5       #
 �  6*   5"      � 5$      $��Stretch=�6    5%      �� 5&      $��* �6(   �� 5'      $
��Auto �6(   .+�5*      �� $ IconContentPresenter��5+      $��0��5,      $��0q�5-      #��z 5.      8�� #���  50      � 52      $ ContentGrid��53      $��1��54      $��15��55      $��0,2,0,0q�56      .+�57      �� $ ElementContentPresenter��58      $��Center=�59      #��z��5:      #��z��5;      #��z��5<      5=      5>      5?      5@       5A   
   P� 5B       �@  IsActive$
 True��6*   . �5C      �� $ ActiveRectangle��6    �@J�Opacity$	 1.0Q�6I   �� 5D      $ 
MainBorder��6    � �� 5E      �� 5F      #
 �  6*   5H      �� 5I      $ IconContentPresenter��6    � �� 5J      �� 5K      #
 �  6*   5M      �� 5N      $ ElementContentPresenter��6    � ��$ 0q�6P    5O      �� 5P      .��5Q      �� .��5R      W� $ ContentGrid��5S   !   $ Height�5T   !    155U   !    05V   !   $ 0:0:.16D�5W   !   W� 5X      $ ContentGrid��5Y   !   $ Opacity�5Z   !    15[   !    05\   !   $ 0:0:.16D�5]   !   W� 5^      $ 
MainBorder��5_   !   $4 .(Border.BorderBrush).(SolidColorBrush.Opacity)�5`   !    05a   !    15b   !   $ 0:0:.16D�5c   !   5d      5e       5g      �� 5h      .��5i      �� .��5j      W� $ ContentGrid��5k   !   $ Height�5l   !    05m   !    155n   !   $ 0:0:.16D�5o   !   W� 5p      $ ContentGrid��5q   !   $ Opacity�5r   !    05s   !    15t   !   $ 0:0:.16D�5u   !   W� 5v      $ 
MainBorder��5w   !   $4 .(Border.BorderBrush).(SolidColorBrush.Opacity)�5x   !    15y   !    05z   !   $ 0:0:.16D�5{   !   5|      5}      5      P� 5�       6&   Z�  �@  Icon. �5�      �� $ IconContentPresenter��6    � y�$ 	Collapsed=�6Q   5�      P� 5�       �@J�IsMouseOver$
 True��6-   . �5�      �� $ 
MainBorder��6    � �� 5�      �� 5�      $	 0.5Q�6*   #
 �  68   5�      5�      ]� 5�       5�      �� 5�       �@J�IsMouseOver$
 True��67   �� 5�       �@  IsActive$ False��64   /�5�      �� 5�      $ IconContentPresenter��6    � �� 5�      �� 5�      #
 �  6.   5�      �� 5�      $ ContentGrid��6    � �� 5�      �� 5�      #
 �  6.   5�      5�      5�      5�      