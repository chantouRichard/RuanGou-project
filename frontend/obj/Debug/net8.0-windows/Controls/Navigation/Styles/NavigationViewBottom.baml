   M S B A M L   `   `   `  ����C  ?frontend, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:frontend.Controlsfrontend.Controls  5      R NNHotkey.Wpf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a76ffb086700b11cW SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� MergedDictionaries �� Source    BottomNavigationViewTemplate �� 
TargetType&     frontend.Controls.NavigationView � RowDefinitions    Header    HeaderVisibility6   0frontend.Controls.NavigationViewContentPresenter  Name    TransitionDuration   TransitionDuration	    TransitionType
   TransitionType    ContentOverlay �Name � ColumnDefinitions ��Name    IsBackEnabled  BasePaneButtonStyle - )BackButtonVisibilityToVisibilityConverter �� 	Converter"   frontend.Controls.SymbolIcon   Symbol    
PaneHeader ��Name-  'System.Windows.Input.KeyboardNavigation   DirectionalNavigation K� IsItemsHost �� Orientation    
PaneFooter + 'BottomCompactNavigationViewItemTemplate*   $frontend.Controls.NavigationViewItem ��Name ��Name   Icon  TextFillColorPrimaryBrush  Name ! NavigationViewItemChevronSize    frontend.Controls.FontIcon   FontSize J� RenderTransformOrigin �� Angle#   frontend.Controls.IconElement   
Foreground  TextFillColorPrimary  �� Color! ��Name" J� Opacity# �� RadiusX$ �� RadiusY  SystemAccentColorSecondary% �Name& � AllowsTransparency' � HorizontalOffset( � 	StaysOpen) �� RelativeSource* �� Mode+ � PlacementTarget, �� ElementName- �� Padding  MenuBorderColorDefaultBrush. �� CornerRadius/ J� SnapsToDevicePixels )	 %SystemFillColorSolidNeutralBackground0 J� Effect3  -System.Windows.Media.Effects.DropShadowEffect1   
BlurRadius2   	Direction3   Opacity4   ShadowDepth5   	MenuItems6 � 	Resources "
 DefaultNavigationViewItemStyle7 �� BasedOn8 �� 
TargetType9 �� Property: �� Value; �� Triggers< P� Property= P� Value> �� 
TargetName  ControlFillColorDefault? �� Opacity@ N� EnterActionsA �� 
TargetNameB �� TargetPropertyC W� From	D W� ToE g� DurationF N� ExitActions�� 5      J 9http://schemas.microsoft.com/winfx/2006/xaml/presentation       :x,http://schemas.microsoft.com/winfx/2006/xaml     5      ,controlsclr-namespace:frontend.Controls  5        5      �� 5   
   $q kpack://application:,,,/frontend;component/Controls/Navigation/Styles/NavigationViewBasePaneButtonStyle.xaml?�6   �� 5   
   $n hpack://application:,,,/frontend;component/Controls/Navigation/Styles/DefaultNavigationViewItemStyle.xaml?�6   �� 5   
   $g apack://application:,,,/frontend;component/Controls/Navigation/Styles/NavigationViewConstants.xaml?�6   %  &	        7  7  &	 �    7  7 
 �� 5      "   6;   .R�5   
   �  5      �� 5      $��* �6    �� 5      $
��Auto �6    .+�5      � $��0��6    5      �� 5      $
��Auto �6$   �� 5      $��* �6$   .+�5      �� $��0��5      $��0q�5      #��z 5      #y�z 5        5       $) #PART_NavigationViewContentPresenter��5!      $��1��5"      #��z��5#      # z 5$      #
 z	 5%      �� 5(      $��0��5)      $��2��5*      $��0q�5+      #��z 5,      5-      � 5.      $ PaneGrid��6   $��1��6%    5/      �� 50      $
��Auto �6'   �� 51      $
��Auto �6'   �� 52      $��* �6'   �� 53      $
��Auto �6'   �� 54      $
��Auto �6'   �� 55      $
��Auto �6'   .+�58      �� $ PART_BackButton��59      $��0��5:      #{�z 5;      8��  y�5=      �� *IsBackButtonVisible+8  .��5?        $ ArrowLeft48=�6*   5@      �� 5C      $��1��5D      $��0q�5E      #��z 5F      �� 5H      $  PART_MenuItemsItemsControl��5I      $��2��5J      $|�False��5K      $ 	Contained=�5L      $��Disabled=�5M      $��Disabled=�5N      ��5O      �� 5P      .R�5Q      �� $��0q�5R   !   $
 True��5S   !   $ 
Horizontal=�5T   !   5U      5W      �� 5]      $��4��5^      $��0q�5_      #��z 5`      �� 5b      $&  PART_FooterMenuItemsItemsControl��5c      $��5��5d      $��0,0,18,0q�5e      $|�False��5f      $ 	Contained=�5g      $��Disabled=�5h      $��Disabled=�5i      ��5j      �� 5k      .R�5l      �� $��0q�5m   !   $
 True��5n   !   $ 
Horizontal=�5o   !   5p      5r      5s      5t      5u      �� 5w      "  6F   .R�5x   
   �  5y      �� 5z      $
��Auto �6    �� 5{      $
��Auto �6    .+�5}      �� $ 
MainBorder��6   ._�5~      � .+�5      � $��8,8,8,8q�6    5�      �� 5�      $
��Auto �6+   �� 5�      $��* �6+   �� 5�      $
��Auto �6+   .+�5�      � $��0��6   .+�5�      �� $ IconContentPresenter��5�      $��0,0,6,0q�5�      $��Center=�5�      $��Center=�5�      #��z 5�      $��16(�5�      #���  5�      5�      �� 5�      $ ElementContentPresenter��5�      $��1��5�      $
��Left=�5�      $��Center=�5�      #��z��5�      $��14(�5�      #��z��5�      � 5�      $ PART_ChevronGrid��5�      $��2��5�      $��0��5�      $y�	Collapsed=�5�      .+�5�        $ ChevronIcon��5�      $��0q�5�      $��Center=�5�      $��Center=�5�      8   $ 0.5, 0.5&�5�      $ ChevronUp24=�5�      z�5�   "   �� 5�   &   $ 0Q�66    5�   "   �� 5�   &   #  �  66   5�      5�      5�      �� 5�      $! ActiveRectangle��5�      $��0��5�      $��16��5�      $��3��5�      $��0,0,0,2q�5�      $��Center=�5�      $��Bottom=�5�      $	" 0.0Q�5�      $# 2��5�      $$ 2��5�      ��5�      �� 5�      #  �  6*   5�      5�      5�      � 5�   
   $% SubMenuPopup��5�   	   $��1��5�   	   $
& True��5�   	   $|�False��5�   	   $	' -18��5�   	   $	��Top=�5�   	   $
��None=�5�   	   $( False��5�   	   ��5�   	   �� *
IsExpanded+) �� *TemplatedParent+$* TwoWay=�+ 5�   	   �� $, 
MainBorder��.��5�   
   �� $ SubMenuBorder��5�      $��
12,0,12,18q�5�      $- 0,3,0,3q�5�      #���  5�      $��1q�5�      $. 8��5�      $
/ True��5�      z�5�      T� 5�      ��5�      �� 5�      #  � 	 6*   0 5�        5�      $1 20Q�5�      $	2 270Q�5�      $
3 0.25Q�5�      $4 6Q�5�      ._�5�      �� $ SubMenuItemsPresenter��5�      $|�False��5�      #��z5 5�      $ 	Contained=�5�      $	" 0.0Q�5�      $��Disabled=�5�      $��Disabled=�5�      ��5�      �� 5�      .R�5�   "   �� $��0q�6-   $
 True��68   5�      6 5�      ���� 5�      87  '        "8  6^   .�5�   "   �� 9 � ��: 5�   &   �� 5�   *   "  6:   .R�5�   .   �� $ 
MainBorder��5�   )   $��25��5�   )   $��0q�5�   )   $- 0q�5�   )   $��Stretch=�5�   )   $��1q�5�   )   $. 4��5�   )   ._�5�   2   � $��0q�67   $��Stretch=�6B    5�   6   �� 5�   :   $��3 �6K   �� 5�   :   $��15 �6K   �� 5�   :   $��* �6K   $	��120��6U   .+�5  6   �� $! ActiveRectangle��5  1   $��0��5  1   $��3��5  1   $��16��5  1   $��0q�5  1   $
��Left=�5  1   $��Center=�5  1   $	" 0.0Q�5	  1   $# 2��5
  1   $$ 2��5  1   ��5  :   �� 5  >   #  �  6N   5  7   �� 5  6   $ IconContentPresenter��5  1   $��1��5  1   $��0,0,0,0q�5  1   $��Center=�5  1   $��Center=�5  1   #��z 5  1   $|�False��5  1   $��16(�5  1   #���  5  1   �� 5  6   $ ElementContentPresenter��5  1   $��2��5  1   $��5,0,0,0q�5  1   $��Stretch=�5  1   $��Center=�5  1   #��z��5   1   $��14(�5!  1   #��z��5"  1   5#  3   5$  /   ; 5%  .   P� 5&  2   < �@ IsActive$
= True��6N   . �5'  6   �� $> ActiveRectangle��6=   9 �@J�Opacity$	: 1.0Q�6m   �� 5(  6   $> 
MainBorder��6=   9 � ��: 5)  :   �� 5*  >   #  �  6N   5,  7   5-  3   P� 5.  2   < �@J�IsMouseOver$
= True��6Q   . �5/  6   �� $> 
MainBorder��6=   9 � ��: 50  :   �� 51  >   $	? 0.5Q�6N   #  �  6\   53  7   54  3   56  +   58  #   59     5:     5;     5<     5=     5>     ; 5?  
   P� 5@     < �@ HasMenuItems$
= True��6.   . �5A     �� $> PART_ChevronGrid��6   9 � y�$: Visible=�6M   �� 5B     $> PART_ChevronGrid��6   9 � ��$: 20��6H   5C     P� 5D     < �@ 
IsExpanded$
= True��6,   @ 5F     �� 5G     .��5H     �� .��5I     W� $A SubMenuItemsPresenter��5J     $B (UIElement.Opacity)�5K     C 0.05L     D 1.05M     $E 	0:0:0.167D�5N     W� 5O     $A ChevronIcon��5P     $7B 1(Control.RenderTransform).(RotateTransform.Angle)�5Q     D -1805R     $E 00:00:00.167D�5S     5T     5U     F 5W     �� 5X     .��5Y     �� .��5Z     W� $A SubMenuItemsPresenter��5[     $B (UIElement.Opacity)�5\     C 1.05]     D 0.05^     $E 	0:0:0.167D�5_     W� 5a     $A ChevronIcon��5b     $7B 1(Control.RenderTransform).(RotateTransform.Angle)�5c     D 05d     $E 00:00:00.167D�5e     5f     5g     5i     P� 5j     < �@ IsActive$
= True��6*   . �5k     �� $> ActiveRectangle��6   9 �@J�Opacity$	: 1.0Q�6I   5l     P� 5m     = 6&   Z� < �@ Icon. �5n     �� $> IconContentPresenter��6   9 � y�$: 	Collapsed=�6Q   5o     P� 5p     = 6)   Z� < � ��. �5q     �� $> IconContentPresenter��6   9 � ��$: 0q�6M   �� 5r     $> ElementContentPresenter��6   9 � y�$: 	Collapsed=�6T   5s     P� 5t     < � ��$=  ��6)   . �5u     �� $> IconContentPresenter��6   9 � ��$: 0q�6M   �� 5v     $> ElementContentPresenter��6   9 � y�$: 	Collapsed=�6T   5w     5y     5z     