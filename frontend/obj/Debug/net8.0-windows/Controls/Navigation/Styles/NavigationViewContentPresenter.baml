   M S B A M L   `   `   `  ����C  ?frontend, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:frontend.Controlsfrontend.Controls  5      R NNHotkey.Wpf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a76ffb086700b11cW SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 8  4DefaultNavigationViewContentPresenterControlTemplate  �� 
TargetType6    0frontend.Controls.NavigationViewContentPresenter ��Name O KDefaultNavigationViewContentPresenterWithDynamicScrollViewerControlTemplate . *DefaultNavigationViewContentPresenterStyle �� 
TargetType �� Property �� Value �� Triggers P� Property P� Value �� BasedOn�� 5      J 9http://schemas.microsoft.com/winfx/2006/xaml/presentation       :x,http://schemas.microsoft.com/winfx/2006/xaml     5      ,controlsclr-namespace:frontend.Controls  5      %z  &	        &	 V     &	 	    7  7   '   )    7  �� 5      "    6S   .R�5   
   �� $ PART_FrameCP��6   #��z��61   5      �� 5
      "    6j   .R�5   
   �� $��Stretch=�5      $��Stretch=�5      $|�False��5      .��5      �� $ PART_FrameCP��6   #��z��65   5      5      �� 5      "   6?   .�5   
   ��  �@�FocusVisualStyle 6-   Z� �� 5   
    � ��$ False��6&   �� 5   
    �@J�SnapsToDevicePixels$
 True��60   �� 5   
    �@�OverridesDefaultStyle$
 True��62   �� 5   
    �@ �JournalOwnership$ OwnsJournal=�6-    5   
   P� 5      $ �@  IsDynamicScrollViewerEnabled$
 True��6>   . �5      ��  � ��8   6-   5      P� 5      $ �@  IsDynamicScrollViewerEnabled$ False��6>   . �5      ��  � ��8  6-   5       5"      �� 5$      8   "   6R   5&      