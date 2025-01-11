﻿#pragma checksum "..\..\..\..\views\ChatSection.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6CEF907E6662C49CEF85CCED5AA8855BF1962BCB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace chat_app.views {
    
    
    /// <summary>
    /// ChatWindow
    /// </summary>
    public partial class ChatWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 37 "..\..\..\..\views\ChatSection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer ScrollViewer;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\views\ChatSection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel MessagesPanel;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\views\ChatSection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel ConnectedUsersPanel;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\views\ChatSection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ClientList;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\..\views\ChatSection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox MessageInput;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/chatapp;component/views/chatsection.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\views\ChatSection.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ScrollViewer = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 2:
            this.MessagesPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 3:
            this.ConnectedUsersPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 4:
            this.ClientList = ((System.Windows.Controls.ListBox)(target));
            return;
            case 5:
            this.MessageInput = ((System.Windows.Controls.TextBox)(target));
            
            #line 67 "..\..\..\..\views\ChatSection.xaml"
            this.MessageInput.GotFocus += new System.Windows.RoutedEventHandler(this.MessageInput_GotFocus);
            
            #line default
            #line hidden
            
            #line 67 "..\..\..\..\views\ChatSection.xaml"
            this.MessageInput.LostFocus += new System.Windows.RoutedEventHandler(this.MessageInput_LostFocus);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 71 "..\..\..\..\views\ChatSection.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SendButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

