﻿#pragma checksum "..\..\..\..\..\Views\Pages\HotkeyBindingPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "91B7A72115F76401712662631545B06C54CFBAE0"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using NHotkey.Wpf;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace frontend.Views {
    
    
    /// <summary>
    /// HotkeyBindingWindow
    /// </summary>
    public partial class HotkeyBindingWindow : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 19 "..\..\..\..\..\Views\Pages\HotkeyBindingPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ActionNameBox;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\..\Views\Pages\HotkeyBindingPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ActionComboBox;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\..\Views\Pages\HotkeyBindingPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CustomPathBox;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\..\Views\Pages\HotkeyBindingPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CtrlCheck;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\..\Views\Pages\HotkeyBindingPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox AltCheck;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\..\Views\Pages\HotkeyBindingPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox ShiftCheck;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\..\Views\Pages\HotkeyBindingPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox WinCheck;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\..\Views\Pages\HotkeyBindingPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox KeyComboBox;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\..\Views\Pages\HotkeyBindingPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BindButton;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\..\..\Views\Pages\HotkeyBindingPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView HotkeyListView;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.2.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/frontend;component/views/pages/hotkeybindingpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\Pages\HotkeyBindingPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.2.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ActionNameBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.ActionComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 24 "..\..\..\..\..\Views\Pages\HotkeyBindingPage.xaml"
            this.ActionComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ActionComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.CustomPathBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            
            #line 35 "..\..\..\..\..\Views\Pages\HotkeyBindingPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BrowseButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.CtrlCheck = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 6:
            this.AltCheck = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 7:
            this.ShiftCheck = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 8:
            this.WinCheck = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 9:
            this.KeyComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 10:
            this.BindButton = ((System.Windows.Controls.Button)(target));
            
            #line 61 "..\..\..\..\..\Views\Pages\HotkeyBindingPage.xaml"
            this.BindButton.Click += new System.Windows.RoutedEventHandler(this.BindButton_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.HotkeyListView = ((System.Windows.Controls.ListView)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.2.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 12:
            
            #line 89 "..\..\..\..\..\Views\Pages\HotkeyBindingPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteHotkey_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

