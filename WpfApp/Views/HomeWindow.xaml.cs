using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Controls;
//using Windows.UI.Xaml.Controls;

namespace WpfApp.Views
{
    public partial class HomeWindow : Window
    {

        /// <summary>单例模式 The instance</summary>
        /// 创建人: gy
        /// 创建时间：2024/11/17  11:45
        private static HomeWindow _instance = new HomeWindow();




        /// <summary>
        ///   <para>根据导航栏选择切换内容,同样使用单例模式</para>
        /// </summary>
        /// 创建人: gy
        /// 创建时间：2024/11/18  16:57
        private static Dictionary<string, Frame>? pageDict;



        internal static HomeWindow Instance
        {
            get
            {
                return _instance;
            }
        }



        public HomeWindow()
        {
            InitializeComponent();

            if (pageDict == null)
            {
                pageDict = new Dictionary<string, Frame>();
            }

            //首页
            //Frame frame = new Frame();
            //frame.Source = new Uri(K1.Tag.ToString(), UriKind.Relative);
            //frame.BorderThickness = new Thickness(0);
            //frame.Margin = new Thickness(10);
            //frame.NavigationUIVisibility = NavigationUIVisibility.Hidden;

            //pageDict.TryAdd(K1.Tag.ToString(), frame);
            //grid1.Children.Add(frame);
            //lastClickLabel = K1;
        }




        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var sideMenuItem = sender as HandyControl.Controls.SideMenuItem;

            if (sideMenuItem == null) return;

            string pageKey = sideMenuItem.Tag.ToString();

            // 如果页面还没加载过，则创建并加入字典和界面
            if (!pageDict.ContainsKey(pageKey))
            {
                Frame frame = new Frame();
                frame.Source = new Uri(pageKey, UriKind.Relative);
                frame.BorderThickness = new Thickness(0);
                frame.Margin = new Thickness(10);
                frame.NavigationUIVisibility = NavigationUIVisibility.Hidden;

                pageDict.Add(pageKey, frame);
                grid1.Children.Add(frame);
            }

            // 隐藏所有已加载的页面
            foreach (var frame in pageDict.Values)
            {
                frame.Visibility = Visibility.Collapsed;
            }

            // 显示当前选中的页面
            pageDict[pageKey].Visibility = Visibility.Visible;
        }
    }
}