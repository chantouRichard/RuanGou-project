   M S B A M L   `   `   `  ����C  ?frontend, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:frontend.Controlsfrontend.Controls  5      a5clr-namespace:frontend.Controls.Navigation.Converters'frontend.Controls.Navigation.Converters  5      R NNHotkey.Wpf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a76ffb086700b11cW SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� MergedDictionaries �� Source   TopNavigationViewTemplate �� 
TargetType&     frontend.Controls.NavigationView � RowDefinitions �Name � ColumnDefinitions ��Name    IsBackEnabled  BasePaneButtonStyle - )BackButtonVisibilityToVisibilityConverter �� 	Converter"   frontend.Controls.SymbolIcon	   Symbol
    
PaneHeader ��Name-  'System.Windows.Input.KeyboardNavigation   DirectionalNavigation K� IsItemsHost �� Orientation    
PaneFooter    Header    HeaderVisibility6   0frontend.Controls.NavigationViewContentPresenter  Name    TransitionDuration   TransitionDuration    TransitionType   TransitionType    ContentOverlay ( $TopCompactNavigationViewItemTemplate*   $frontend.Controls.NavigationViewItem ��Name ��Name   Icon  TextFillColorPrimaryBrush  Name ! NavigationViewItemChevronSize    frontend.Controls.FontIcon   FontSize J� RenderTransformOrigin �� Angle#   frontend.Controls.IconElement   
Foreground  TextFillColorPrimary  �� Color! ��Name" J� Opacity# �� RadiusX$ �� RadiusY  SystemAccentColorSecondary% �Name& � AllowsTransparency' � HorizontalOffset( � 	StaysOpen) �� RelativeSource* �� Mode+ � PlacementTarget, �� ElementName- �� Padding  MenuBorderColorDefaultBrush. �� CornerRadius/ J� SnapsToDevicePixels )	 %SystemFillColorSolidNeutralBackground0 J� Effect3  -System.Windows.Media.Effects.DropShadowEffect1   
BlurRadius2   	Direction3   Opacity4   ShadowDepth5   	MenuItems6 � 	Resources "
 DefaultNavigationViewItemStyle7 �� BasedOn8 �� 
TargetType9 �� Property: �� Value; �� Triggers< P� Property= P� Value> �� 
TargetName  ControlFillColorDefault? �� Opacity@ N� EnterActionsA �� 
TargetNameB �� TargetPropertyC W� From	D W� ToE g� DurationF N� ExitActions�� 5      J 9http://schemas.microsoft.com/winfx/2006/xaml/presentation       :x,http://schemas.microsoft.com/winfx/2006/xaml     5      ,controlsclr-namespace:frontend.Controls  5      D
converters5clr-namespace:frontend.Controls.Navigation.Converters  5        5      �� 5   
   $q kpack://application:,,,/frontend;component/Controls/Navigation/Styles/NavigationViewBasePaneButtonStyle.xaml?�6   �� 5   
   $n hpack://application:,,,/frontend;component/Controls/Navigation/Styles/DefaultNavigationViewItemStyle.xaml?�6   �� 5	   
   $g apack://application:,,,/frontend;component/Controls/Navigation/Styles/NavigationViewConstants.xaml?�6   %  &	        7  7  &	 �    7  7 
 �� 5      "   68   .R�5   
   �  5      �� 5      $
��Auto �6    �� 5      $��* �6    .+�5      � $ PaneGrid��6   $��0��6%    5      �� 5      $
��Auto �6'   �� 5      $
��Auto �6'   �� 5      $��* �6'   �� 5      $
��Auto �6'   �� 5      $
��Auto �6'   �� 5      $
��Auto �6'   .+�5      �� $ PART_BackButton��5      $��0��5      #{�z 5       8��  y�5"      �� *IsBackButtonVisible+8  .��5$        $	 ArrowLeft48=�6*   5%      �� 5(      $��1��5)      $��0q�5*      #��z
 5+      �� 5-      $  PART_MenuItemsItemsControl��5.      $��2��5/      $|�False��50      $ 	Contained=�51      $��Disabled=�52      $��Disabled=�53      ��54      �� 55      .R�56      �� $��0q�57   !   $
 True��58   !   $ 
Horizontal=�59   !   5:      5<      �� 5B      $��4��5C      $��0q�5D      #��z 5E      �� 5G      $&  PART_FooterMenuItemsItemsControl��5H      $��5��5I      $��0,0,18,0q�5J      $|�False��5K      $ 	Contained=�5L      $��Disabled=�5M      $��Disabled=�5N      ��5O      �� 5P      .R�5Q      �� $��0q�5R   !   $
 True��5S   !   $ 
Horizontal=�5T   !   5U      5W      5X      � 5Z      $��1��6    5[      �� 5\      $
��Auto �6$   �� 5]      $��* �6$   .+�5a      �� $��0��5b      $��0q�5c      #��z 5d      #y�z 5e        5h      $) #PART_NavigationViewContentPresenter��5i      $��1��5j      #��z��5k      # z 5l      # z 5m      �� 5p      $��0��5q      $��2��5r      $��0q�5s      #��z 5t      5u      5v      5w      �� 5y      "  6C   .R�5z   
   �  5{      �� 5|      $
��Auto �6    �� 5}      $
��Auto �6    .+�5      �� $ 
MainBorder��6   ._�5�      � .+�5�      � $��8,8,8,8q�6    5�      �� 5�      $
��Auto �6/   �� 5�      $��* �6/   �� 5�      $
��Auto �6/   .+�5�      � $��0��6   .+�5�      �� $ IconContentPresenter��5�   !   $��0,0,6,0q�5�   !   $��Center=�5�   !   $��Center=�5�   !   #��z 5�   !   $��16(�5�   !   #���  5�   !   5�      �� 5�      $ ElementContentPresenter��5�      $��1��5�      $
��Left=�5�      $��Center=�5�      #��z��5�      $��14(�5�      #��z��5�      � 5�      $ PART_ChevronGrid��5�      $��2��5�      $��0��5�      $y�	Collapsed=�5�      .+�5�        $ ChevronIcon��5�   !   $��0q�5�   !   $��Center=�5�   !   $��Center=�5�   !   8   $ 0.5, 0.5&�5�   !   $	 ChevronDown24=�5�   !   z�5�   "   �� 5�   &   $ 0Q�66    5�   "   �� 5�   &   #  �  66   5�      5�      5�      �� 5�      $! ActiveRectangle��5�      $��0��5�      $��16��5�      $��3��5�      $��0,0,0,2q�5�      $��Center=�5�      $��Bottom=�5�      $	" 0.0Q�5�      $# 2��5�      $$ 2��5�      ��5�      �� 5�      #  �  6.   5�      5�      5�      � 5�      $% SubMenuPopup��5�      $��1��5�      $
& True��5�      $|�False��5�      $	' -18��5�      $��Bottom=�5�      $
��None=�5�      $( False��5�      ��5�      �� *
IsExpanded+) �� *TemplatedParent+$* TwoWay=�+ 5�      �� $, 
MainBorder��.��5�      �� $ SubMenuBorder��5�      $��
12,0,12,18q�5�      $- 0,3,0,3q�5�      #���  5�      $��1q�5�      $. 8��5�      $
/ True��5�      z�5�      T� 5�      ��5�      �� 5�      #  � 	 6*   0 5�        5�      $1 20Q�5�      $	2 270Q�5�      $
3 0.25Q�5�      $4 6Q�5�      ._�5�      �� $ SubMenuItemsPresenter��5�      $|�False��5�      #��z5 5�      $ 	Contained=�5�      $	" 0.0Q�5�      $��Disabled=�5�      $��Disabled=�5�      ��5�      �� 5�      .R�5�   "   �� $��0q�6-   $
 True��68   5�      6 5�      ���� 5�      87  '        "8  6^   .�5�   "   �� 9 � ��: 5�   &   �� 5�   *   "  6:   .R�5�   .   �� $ 
MainBorder��5�   1   $��25��5�   1   $��0q�5�   1   $- 0q�5�   1   $��Stretch=�5�   1   $��1q�5�   1   $. 4��5�   1   ._�5�   2   � $��0q�67   $��Stretch=�6B    5�   6   �� 5�   :   $��3 �6K   �� 5   :   $��15 �6K   �� 5  :   $��* �6K   $	��120��6U   .+�5  6   �� $! ActiveRectangle��5  9   $��0��5  9   $��3��5  9   $��16��5  9   $��0q�5  9   $
��Left=�5	  9   $��Center=�5
  9   $	" 0.0Q�5  9   $# 2��5  9   $$ 2��5  9   ��5  :   �� 5  >   #  �  6N   5  7   �� 5  6   $ IconContentPresenter��5  9   $��1��5  9   $��0,0,0,0q�5  9   $��Center=�5  9   $��Center=�5  9   #��z 5  9   $|�False��5  9   $��16(�5  9   #���  5  9   �� 5  6   $ ElementContentPresenter��5  9   $��2��5  9   $��5,0,0,0q�5  9   $��Stretch=�5   9   $��Center=�5!  9   #��z��5"  9   $��14(�5#  9   #��z��5$  9   5%  3   5&  /   ; 5'  .   P� 5(  2   < �@ IsActive$
= True��6N   . �5)  6   �� $> ActiveRectangle��6=   9 �@J�Opacity$	: 1.0Q�6m   �� 5*  6   $> 
MainBorder��6=   9 � ��: 5+  :   �� 5,  >   #  �  6N   5.  7   5/  3   P� 50  2   < �@J�IsMouseOver$
= True��6Q   . �51  6   �� $> 
MainBorder��6=   9 � ��: 52  :   �� 53  >   $	? 0.5Q�6N   #  �  6\   55  7   56  3   58  +   5:  #   5;     5<     5=     5>     5?     5@     ; 5A  
   P� 5B     < �@ HasMenuItems$
= True��6.   . �5C     �� $> PART_ChevronGrid��6   9 � y�$: Visible=�6M   �� 5D     $> PART_ChevronGrid��6   9 � ��$: 20��6H   5E     P� 5F     < �@ 
IsExpanded$
= True��6,   @ 5H     �� 5I     .��5J     �� .��5K     W� $A SubMenuItemsPresenter��5L  !   $B (UIElement.Opacity)�5M  !   C 0.05N  !   D 1.05O  !   $E 	0:0:0.167D�5P  !   W� 5Q     $A ChevronIcon��5R  !   $7B 1(Control.RenderTransform).(RotateTransform.Angle)�5S  !   D -1805T  !   $E 00:00:00.167D�5U  !   5V     5W     F 5Y     �� 5Z     .��5[     �� .��5\     W� $A SubMenuItemsPresenter��5]  !   $B (UIElement.Opacity)�5^  !   C 1.05_  !   D 0.05`  !   $E 	0:0:0.167D�5a  !   W� 5c     $A ChevronIcon��5d  !   $7B 1(Control.RenderTransform).(RotateTransform.Angle)�5e  !   D 05f  !   $E 00:00:00.167D�5g  !   5h     5i     5k     P� 5l     < �@ IsActive$
= True��6*   . �5m     �� $> ActiveRectangle��6   9 �@J�Opacity$	: 1.0Q�6I   5n     P� 5o     = 6&   Z� < �@ Icon. �5p     �� $> IconContentPresenter��6   9 � y�$: 	Collapsed=�6Q   5q     P� 5r     = 6)   Z� < � ��. �5s     �� $> IconContentPresenter��6   9 � ��$: 0q�6M   �� 5t     $> ElementContentPresenter��6   9 � y�$: 	Collapsed=�6T   5u     P� 5v     < � ��$=  ��6)   . �5w     �� $> IconContentPresenter��6   9 � ��$: 0q�6M   �� 5x     $> ElementContentPresenter��6   9 � y�$: 	Collapsed=�6T   5y     5{     5}     