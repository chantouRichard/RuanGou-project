   M S B A M L   `   `   `  ����C  ?frontend, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:frontend.Controlsfrontend.Controls  5      R NNHotkey.Wpf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a76ffb086700b11cW SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� MergedDictionaries �� Source   DefaultNavigationViewStyle �� 
TargetType&     frontend.Controls.NavigationView �� Property �� Value  TextFillColorSecondary �� Color �� Triggers P� Property P� Value  LeftNavigationViewTemplate ) %LeftCompactNavigationViewItemTemplate 0 ,LeftMinimalCompactNavigationViewItemTemplate ( $LeftFluentNavigationViewItemTemplate  TopNavigationViewTemplate ( $TopCompactNavigationViewItemTemplate   BottomNavigationViewTemplate +	 'BottomCompactNavigationViewItemTemplate "
 DefaultNavigationViewItemStyle	 �� BasedOn*   $frontend.Controls.NavigationViewItem�� 5	      J 9http://schemas.microsoft.com/winfx/2006/xaml/presentation       :x,http://schemas.microsoft.com/winfx/2006/xaml     5
      ,controlsclr-namespace:frontend.Controls  5        5      �� 5   
   $e _pack://application:,,,/frontend;component/Controls/Navigation/Styles/NavigationViewCompact.xaml?�6   �� 5   
   $p jpack://application:,,,/frontend;component/Controls/Navigation/Styles/NavigationViewLeftMinimalCompact.xaml?�6   �� 5   
   $d ^pack://application:,,,/frontend;component/Controls/Navigation/Styles/NavigationLeftFluent.xaml?�6   �� 5   
   $a [pack://application:,,,/frontend;component/Controls/Navigation/Styles/NavigationViewTop.xaml?�6   �� 5   
   $d ^pack://application:,,,/frontend;component/Controls/Navigation/Styles/NavigationViewBottom.xaml?�6   %q  &	        7  7  7  7  7  7  7  7  7  7 	 '  �    7 
 '       7   �� 5      "   6/   .�5   
   ��  � �� 5      �� 5      # �  6"   5      �� 5   
    � ��
 ���� 6'   �� 5   
    � ��$ 0q�6#   �� 5   
    � ��$ 0q�6$   �� 5   
    �@  OpenPaneLength$	 230Q�6+   �� 5   
    �@  CompactPaneLength$ 40Q�6.   �� 5   
    � ��$ False��6:   �� 5    
    � ��$ Disabled=�6G   �� 5!   
    � ��$ Disabled=�6E   �� 5"   
   " �@��IsDeferredScrollingEnabled$ False��6D   �� 5#   
    �@�FocusVisualStyle 6-   Z� �� 5$   
    � ��$ False��6&   �� 5%   
    �@J�SnapsToDevicePixels$
 True��60   �� 5&   
    �@�OverridesDefaultStyle$
 True��62    5'   
   P� 5(       �@  PaneDisplayMode$
 Left=�61   . �5)      ��  � ��8   6-   �� 5*       �@  ItemTemplate8  61   5+      P� 5,       �@  PaneDisplayMode$ LeftMinimal=�61   . �5-      ��  � ��8  6-   �� 5.       �@  ItemTemplate8  61   5/      P� 50       �@  PaneDisplayMode$ 
LeftFluent=�61   . �51      ��  � ��8  6-   �� 52       �@  ItemTemplate8  61   �� 53       �@  OpenPaneLength$	 NaNQ�63   54      P� 55       �@  PaneDisplayMode$	 Top=�61   . �56      ��  � ��8  6-   �� 57       �@  ItemTemplate8  61   58      P� 59       �@  PaneDisplayMode$ Bottom=�61   . �5:      ��  � ��8  6-   �� 5;       �@  ItemTemplate8 	 61   5<      5>      �� 5@      8	   "  6F   �� 5A      8	   "   6B   5C      