﻿#pragma checksum "..\..\..\..\..\Views\Pages\MemoPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E2A7B0019D5F931952C8CE7FAF7B9CAA4537D041"
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
using frontend.Models;


namespace frontend.Views.Pages {
    
    
    /// <summary>
    /// MemoPage
    /// </summary>
    public partial class MemoPage : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 134 "..\..\..\..\..\Views\Pages\MemoPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TaskTitleTextBox;
        
        #line default
        #line hidden
        
        
        #line 140 "..\..\..\..\..\Views\Pages\MemoPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TaskDesciptionTextBox;
        
        #line default
        #line hidden
        
        
        #line 146 "..\..\..\..\..\Views\Pages\MemoPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker DueDatePicker;
        
        #line default
        #line hidden
        
        
        #line 170 "..\..\..\..\..\Views\Pages\MemoPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView TodoListView;
        
        #line default
        #line hidden
        
        
        #line 238 "..\..\..\..\..\Views\Pages\MemoPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup EditTaskPopup;
        
        #line default
        #line hidden
        
        
        #line 253 "..\..\..\..\..\Views\Pages\MemoPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox EditTitleTextBox;
        
        #line default
        #line hidden
        
        
        #line 256 "..\..\..\..\..\Views\Pages\MemoPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker EditDueDatePicker;
        
        #line default
        #line hidden
        
        
        #line 259 "..\..\..\..\..\Views\Pages\MemoPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox EditDescriptionTextBox;
        
        #line default
        #line hidden
        
        
        #line 262 "..\..\..\..\..\Views\Pages\MemoPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox EditIsCompletedCheckBox;
        
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
            System.Uri resourceLocater = new System.Uri("/frontend;component/views/pages/memopage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\Pages\MemoPage.xaml"
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
            this.TaskTitleTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.TaskDesciptionTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.DueDatePicker = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 4:
            
            #line 151 "..\..\..\..\..\Views\Pages\MemoPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddTask_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 159 "..\..\..\..\..\Views\Pages\MemoPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.RefreshList_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 163 "..\..\..\..\..\Views\Pages\MemoPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ClearCompleted_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.TodoListView = ((System.Windows.Controls.ListView)(target));
            return;
            case 11:
            this.EditTaskPopup = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 12:
            this.EditTitleTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 13:
            this.EditDueDatePicker = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 14:
            this.EditDescriptionTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 15:
            this.EditIsCompletedCheckBox = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 16:
            
            #line 266 "..\..\..\..\..\Views\Pages\MemoPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CancelEdit_Click);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 270 "..\..\..\..\..\Views\Pages\MemoPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveTask_Click);
            
            #line default
            #line hidden
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
            case 8:
            
            #line 187 "..\..\..\..\..\Views\Pages\MemoPage.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.TaskCompleted_Changed);
            
            #line default
            #line hidden
            
            #line 188 "..\..\..\..\..\Views\Pages\MemoPage.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Unchecked += new System.Windows.RoutedEventHandler(this.TaskCompleted_Changed);
            
            #line default
            #line hidden
            break;
            case 9:
            
            #line 223 "..\..\..\..\..\Views\Pages\MemoPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.EditTask_Click);
            
            #line default
            #line hidden
            break;
            case 10:
            
            #line 226 "..\..\..\..\..\Views\Pages\MemoPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteTask_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

