﻿#pragma checksum "..\..\..\Client.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "94538F804792FC24589735D4E5FFE2987FCAC400"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Session2._1;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
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


namespace Session2._1 {
    
    
    /// <summary>
    /// Client
    /// </summary>
    public partial class Client : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\..\Client.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Discr;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Client.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SizeX;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\Client.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Examples;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Client.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid OrdersView;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\Client.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Save;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Client.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameOrd;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\Client.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameProduct;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Client.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Del;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\Client.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SizeY;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Client.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SizeZ;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\Client.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox SizeUnit;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\Client.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Exit;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.8.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Session2;component/client.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Client.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.8.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\..\Client.xaml"
            ((Session2._1.Client)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Discr = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.SizeX = ((System.Windows.Controls.TextBox)(target));
            
            #line 24 "..\..\..\Client.xaml"
            this.SizeX.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.SizeX_TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Examples = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.OrdersView = ((System.Windows.Controls.DataGrid)(target));
            
            #line 29 "..\..\..\Client.xaml"
            this.OrdersView.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.OrdersView_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Save = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\Client.xaml"
            this.Save.Click += new System.Windows.RoutedEventHandler(this.Save_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.NameOrd = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.NameProduct = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.Del = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\Client.xaml"
            this.Del.Click += new System.Windows.RoutedEventHandler(this.Del_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.SizeY = ((System.Windows.Controls.TextBox)(target));
            
            #line 37 "..\..\..\Client.xaml"
            this.SizeY.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.SizeY_TextChanged);
            
            #line default
            #line hidden
            return;
            case 11:
            this.SizeZ = ((System.Windows.Controls.TextBox)(target));
            
            #line 38 "..\..\..\Client.xaml"
            this.SizeZ.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.SizeZ_TextChanged);
            
            #line default
            #line hidden
            return;
            case 12:
            this.SizeUnit = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 13:
            this.Exit = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\..\Client.xaml"
            this.Exit.Click += new System.Windows.RoutedEventHandler(this.Exit_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

