   M S B A M L   `   `   `  ����C  ?frontend, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:frontend.Controlsfrontend.Controls  5      R NNHotkey.Wpf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a76ffb086700b11cW SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089S OHandyControl, Version=3.5.1.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b  �� MergedDictionaries �� Source   LeftNavigationViewTemplate �� 
TargetType&     frontend.Controls.NavigationView � ColumnDefinitions �Name    OpenPaneLength ��Name � RowDefinitions ��Name	    IsBackEnabled  BasePaneButtonStyle - )BackButtonVisibilityToVisibilityConverter
 �� 	Converter"   frontend.Controls.SymbolIcon   Symbol  BoolToVisibilityConverter    
PaneHeader ��Name-  'System.Windows.Input.KeyboardNavigation   DirectionalNavigation K� IsItemsHost    
PaneFooter  ApplicationBackgroundBrush �� CornerRadius    Header    HeaderVisibility6   0frontend.Controls.NavigationViewContentPresenter  Name    TransitionDuration   TransitionDuration    TransitionType   TransitionType    ContentOverlay'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup   Name   States   System.Windows.VisualState   Name   
Storyboard g� AccelerationRatio  �� 
TargetName! �� TargetProperty" W� From	# W� To$ g� Duration% �� Triggers& P� Property' P� Value( �� 
TargetName) �� Property* �� Value ) %LeftCompactNavigationViewItemTemplate*   $frontend.Controls.NavigationViewItem+ �� 
TargetType, �� Triggers- �� Opacity  ControlFillColorDefault. �� Color/ ��Name0   Icon  TextFillColorPrimaryBrush1 ��Name2 J� Opacity3 �� RadiusX4 �� RadiusY  SystemAccentColorSecondary5  Name !	 NavigationViewItemChevronSize    frontend.Controls.FontIcon6   FontSize7 J� RenderTransformOrigin8 �� Angle#	   frontend.Controls.IconElement9 	  
Foreground 
 TextFillColorPrimary:   	MenuItems; � 	Resources " DefaultNavigationViewItemStyle< �� BasedOn  NullToVisibilityConverter= N� EnterActions> N� ExitActions? ]� 
Conditions@ �� PropertyA �� Value�� 5      J 9http://schemas.microsoft.com/winfx/2006/xaml/presentation       :x,http://schemas.microsoft.com/winfx/2006/xaml     5      ,controlsclr-namespace:frontend.Controls  5      0hc'https://handyorg.github.io/handycontrol  5        5      �� 5   
   $q kpack://application:,,,/frontend;component/Controls/Navigation/Styles/NavigationViewBasePaneButtonStyle.xaml?�6   �� 5   
   $n hpack://application:,,,/frontend;component/Controls/Navigation/Styles/DefaultNavigationViewItemStyle.xaml?�6   �� 5	   
   $g apack://application:,,,/frontend;component/Controls/Navigation/Styles/NavigationViewConstants.xaml?�6   %�   &	        7  7  7  7  &	 �    7 	 7  7  �� 5      "   69   .R�5   
   �  5      �� 5      $
��Auto �6#   �� 5      $��* �6#   .+�5      � $ PaneGrid��5      $��0��5      #��z 5      $	��4,0q�5      .+�5      �� $ 
PaneBorder��6   ._�5      �  5      �� 5      $
��Auto �6,   �� 5      $��* �6,   �� 5      $
��Auto �6,   .+�5       � $��0��6    5!      �� 5"   "   $
��Auto �60   �� 5#   "   $
��Auto �60   .+�5%      ]� $��False��6(   .+�5&   "   �� $ PART_BackButton��5'   %   $
��Left=�5(   %   #{�z	 5)   %   8��  y�5+   %   �� *IsBackButtonVisible+8
  .��5-   &     $ ArrowLeft48=�6:   5.   #   �� 5/   "   $ PART_ToggleButton��50   %   $��Right=�51   %   8�� y�53   %   �� *IsPaneToggleVisible+8
  .��55   &     $ LineHorizontal320=�6:   56   #   57      �� 59      $��2��5:   !   $��0q�5;   !   #��z 5<   !   5K      �� 5N      $��1��5O      $��0,0,-4,0q�5P      $��0,0,4,0q�5Q      $
��True��5R      $��Disabled=�5S      $
��Auto=�5T      .��5U      �  5V   "   �� 5W   &   $��* �64   �� 5X   &   $
��Auto �64   .+�5Z   "   �� $  PART_MenuItemsItemsControl��5[   %   $��0��5\   %   $|�False��5]   %   $ 	Contained=�5^   %   $��Disabled=�5_   %   $��Disabled=�5`   %   ��5a   &   �� 5b   *   .R�5c   .   �� $��0q�69   $
 True��6D   5d   +   5f   #   �� 5g   "   $&  PART_FooterMenuItemsItemsControl��5h   %   $��1��5i   %   $��0,0,0,4q�5j   %   $|�False��5k   %   $ 	Contained=�5l   %   $��Disabled=�5m   %   $��Disabled=�5n   %   ��5o   &   �� 5p   *   .R�5q   .   �� $��0q�69   $
 True��6D   5r   +   5t   #   5u      5v      �� 5y      $��2��5z      $��0q�5{      #��z 5|      5}      5~      5      �� 5�      $��1��5�      $��
0,10,10,10q�5�      #���  5�      $ 10��5�      ._�5�      �  5�      �� 5�      $
��Auto �6(   �� 5�      $��* �6(   .+�5�      �� $��0��5�      $��0q�5�      #��z 5�      #y�z 5�        5�      $) #PART_NavigationViewContentPresenter��5�      $��1��5�      #��z��5�      # z 5�      # z 5�      �� 5�      $��0��5�      $��2��5�      $��0q�5�      #��z 5�      5�      5�       5�        5�      $ 
PaneStates��6#   . 5�        $ PaneOpen��6"   . 5�      �� .��5�      W� $	 0.4Q�5�   !   $  PaneGrid��5�   !   $! Width�5�   !   " 405�   !   ## z 5�   !   $$ 0:0:.16D�5�   !   5�      5�        5�      $ PaneCompact��6"   . 5�      �� .��5�      W� $	 0.4Q�5�   !   $  PaneGrid��5�   !   $! Width�5�   !   #" z 5�   !   # 405�   !   $$ 0:0:.16D�5�   !   5�      5�      5�      5�      % 5�   
   P� 5�      & �@  
IsPaneOpen$' False��6,   . �5�      �� $( PART_BackButton��6   ) � ��$	* Top=�6P   �� 5�      $( PART_ToggleButton��6   ) � ��$* Bottom=�6R   5�      5�      �� 5�      "  6D   .R�5�   
   �  5�      �� 5�      $
��Auto �6    �� 5�      $
��Auto �6    .+�5�      �� $ 
MainBorder��5�      $��0��5�      $	��230��5�      $��40��5�      $��0q�5�      #��z��5�      $��1q�5�      # z 5�      ��5�      �� 5�      "+ ��6   .�5�      �� ) � ��
* ���� 67   , 5�      P� 5�      & �@J�IsMouseOver$
' True��6=   . �5�   "   �� ) � ��* 5�   &   �� 5�   *   $	- 0.5Q�6:   #. �  6H   5�   #   5�      5�      ._�5�      � 
������  5�      �� 5�      $��40 �6+   �� 5�      $��* �6+   �� 5�      $
��Auto �6+   .+�5�      � $��0��6   .+�5�      �� $/ IconContentPresenter��5�      $��-1,0,0,0q�5�      $��Center=�5�      $��Center=�5�      #��z0 5�      $|�False��5�      $��16(�5�      #���  5�      5      �� 5     $1 ActiveRectangle��5     $��0��5     $	��3.5��5     $��16��5     $��0q�5     $
��Left=�5     $��Center=�5	     $	2 0.0Q�5
     $3 2��5     $4 2��5     ��5     �� 5     #. �  6.   5     �� 5     $/ ElementContentPresenter��5     $��1��5     $
��Left=�5     $��Center=�5     #��z��5     $��14(�5     #��z��5     � 5     $ PART_ChevronGrid��5     $��2��5     $��0��5     $y�	Collapsed=�5     .+�5       $5 ChevronIcon��5      $��0q�5!     $��Center=�5"     $��Center=�5#     86   $7 0.5, 0.5&�5%     $ ChevronDown24=�5&     z�5'     �� 5(  "   $8 0Q�62   9 5*     �� 5+  "   #. � 
 62   5-     5.     5/     50     �� 52     $ MenuItemsPresenter��53     $��1��54     $|�False��55     #��z: 56     $ 	Contained=�57     $	2 0.0Q�58     $��Disabled=�59     $��Disabled=�5:     $y�	Collapsed=�5;     ��5<     �� 5=     .R�5>     �� $��0q�6%   $
 True��60   5?     ; 5A     ���� 5B     8<  '        "+  6V   .�5C     �� ) � ��* 5D     �� 5E  "   "  62   .R�5F  &   �� $ 
MainBorder��5G  )   $��40��5H  )   $��Stretch=�5I  )   $��1q�5J  )   $ 4��5K  )   ._�5L  *   � $��30,0,0,0q�6/   $��Stretch=�6A    5M  .   �� 5N  2   $
��Auto �6C   �� 5O  2   $
��Auto �6C   �� 5P  2   $��* �6C   $	��180��6M   .+�5S  .   �� $1 ActiveRectangle��5T  1   $��0��5U  1   $	��3.5��5V  1   $��16��5W  1   $��0q�5X  1   $
��Left=�5Y  1   $��Center=�5Z  1   $	2 0.0Q�5[  1   $3 2��5\  1   $4 2��5]  1   ��5^  2   �� 5_  6   #. �  6F   5a  /   �� 5c  .   $/ IconContentPresenter��5d  1   $��1��5e  1   $��6,0,3,0q�5f  1   $��Center=�5g  1   $��Center=�5h  1   #��z0 5i  1   $|�False��5j  1   $��18(�5k  1   #���  5l  1   y�5m  1   �� *Icon+8
  �� 5o  .   $/ ElementContentPresenter��5p  1   $��2��5q  1   $��10,0,0,0q�5r  1   $��Stretch=�5s  1   $��Center=�5t  1   #��z��5u  1   $��14(�5v  1   #��z��5w  1   5x  +   5y  '   % 5z  &   P� 5{  *   & �@ IsActive$
' True��6F   . �5|  .   �� $( ActiveRectangle��65   ) �@J�Opacity$	* 1.0Q�6e   �� 5}  .   $( 
MainBorder��65   ) � ��* 5~  2   �� 5  6   #. �  6F   5�  /   5�  +   P� 5�  *   & �@J�IsMouseOver$
' True��6I   . �5�  .   �� $( 
MainBorder��65   ) � ��* 5�  2   �� 5�  6   $	- 0.5Q�6F   #. �  6T   5�  /   5�  +   5�  #   5�     5�     5�     5�     5�     % 5�  
   P� 5�     & �@ HasMenuItems$
' True��6.   . �5�     �� $( PART_ChevronGrid��6   ) � y�$* Visible=�6M   �� 5�     $( PART_ChevronGrid��6   ) � ��$* 40��6H   5�     P� 5�     & �@ 
IsExpanded$
' True��6,   . �5�     �� $( MenuItemsPresenter��6   ) � y�$* Visible=�6O   = 5�     �� 5�     .��5�     �� .��5�     W� $  MenuItemsPresenter��5�  !   $! (ItemsControl.Opacity)�5�  !   " 0.05�  !   # 1.05�  !   $$ 00:00:00.167D�5�  !   W� 5�     $  ChevronIcon��5�  !   $7! 1(Control.RenderTransform).(RotateTransform.Angle)�5�  !   # -1805�  !   $$ 00:00:00.167D�5�  !   5�     5�     > 5�     �� 5�     .��5�     �� .��5�     W� $  MenuItemsPresenter��5�  !   $! (ItemsControl.Opacity)�5�  !   " 1.05�  !   # 0.05�  !   $$ 00:00:00.167D�5�  !   W� 5�     $  ChevronIcon��5�  !   $7! 1(Control.RenderTransform).(RotateTransform.Angle)�5�  !   # 05�  !   $$ 00:00:00.167D�5�  !   5�     5�     5�     P� 5�     & �@ IsActive$
' True��6*   . �5�     �� $( ActiveRectangle��6   ) �@J�Opacity$	* 1.0Q�6I   �� 5�     $( 
MainBorder��6   ) � ��* 5�     �� 5�     #. �  6*   5�     5�     P� 5�     ' 6&   Z� & �@ Icon. �5�     �� $( IconContentPresenter��6   ) � y�$* 	Collapsed=�6Q   5�     ]� 5�     ? 5�     �� 5�     @ �@J�IsMouseOver$
A True��67   �� 5�     @ �@ IsActive$A False��64   /�5�     �� 5�     $( IconContentPresenter��6   ) � ��* 5�     �� 5�     #. � 
 6.   5�     �� 5�     $( ElementContentPresenter��6   ) � ��* 5�     �� 5�     #. � 
 6.   5�     5�     5�     5�     