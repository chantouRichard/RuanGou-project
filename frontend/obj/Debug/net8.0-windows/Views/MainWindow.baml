   M S B A M L   `   `   `  ����C  ?frontend, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:frontend.Controlsfrontend.Controls  5      #clr-namespace:frontendfrontend  5          frontend.MainWindow   5      R NNHotkey.Wpf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a76ffb086700b11cW SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35J 9http://schemas.microsoft.com/winfx/2006/xaml/presentation       5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      S OHandyControl, Version=3.5.1.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5      (lcclr-namespace:frontend.Controls  5      "localclr-namespace:frontend  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5	      IoAhttp://schemas.microsoft.com/winfx/2006/xaml/presentation/options  5
        � Title$  
MainWindow��5      $
��1100��5      $	��800��5      !  HandyControl.Controls.Empty   	ShowEmpty$
 True��5       � Icon$) #/frontend;component/Assets/icon.ico��5       � 
ResizeMode$ CanMinimize=�5      "  HandyControl.Controls.Window   	ShowTitle$ False��5         WindowWin10#��[  5       � WindowStartupLocation$ CenterScreen=�5       � 	Resources 5       
 Freeze  CloudDrawingBrush( �� !CacheInvalidationThresholdMaximum( �� !CacheInvalidationThresholdMinimum	 �� CachingHint
 j� TileMode j� Viewport j� ViewportUnits �� Opacity   frontend.SkinManager   Current �� Source �� Path j� Stretch j� 
AlignmentX j� 
AlignmentY L� Drawing �� Rect �� ImageSource  CountTextBlock �� 
TargetType �� Property �� Value�� 5   
   %f  &	       &	 �    L� 5      4True 5#      $	 2.0Q�5      $	 0.5Q�5      $	 Cache=�5      $

 None=�5!      $ 0,0,1100,800�5"      $ Absolute=�5#       5      �� # Z $ 	InOpacity� 5      �� # Z $ BackgroundStretch� 5      �� # Z $ BackgroundAlignmentX� 5       �� # Z $ BackgroundAlignmentY� 5$      �� 5%      4True 5(      $ 0,0,1100,800� 5'      �� # Z $ BackgroundPic�5(      5*      �� 5,      " ��6+   .�5-      ��  � ��$ 16(�6-   �� 5.       � ��$
 Bold"�6/   5/      50         NonClientAreaContent 52      �� 53   
   $��Stretch=�6    �� Orientation$ 
Horizontal=�61   .+�54      �� $��10,0,0,0q�55       �� Padding$
 10,4q�56      $��Center=�57       * &SolidBackgroundFillColorSecondaryBrush#���  58       �� CornerRadius$ 2��59      ._�5:      �� $��Center=�5;      $
��Bold"�5<        TextFillColorPrimaryBrush#���  5=      ��5>      �� *ViewModel.Title+5?      o� 5@      $
��Left=�6   .@�5A      m� $��29��6   $��菜单��6'   .@�5B      m� $��子菜单-1��6   m� 5C      $��子菜单-2��6   m� 5D      $��子菜单-2��6   5E      5F      5H      .��5J      � .+�5L   
   �� #��[ 6   &    frontend.Controls.NavigationView  5O   
   -   5          Name$ RootNavigation��5P   	   
������    PaneDisplayMode$
 Left=�5U   	     J� Opacity  5R   	   �� # Z $ 
OutOpacity�!   FooterMenuItemsSource! 5S   	   �� *ViewModel.NavigationFooter+" �� Mode$" OneWay=�#   MenuItemsSource# 5T   	   �� *ViewModel.NavigationItems+$" OneWay=�5U   	   5V      5X      