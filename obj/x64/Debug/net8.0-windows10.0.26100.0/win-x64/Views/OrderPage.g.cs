﻿#pragma checksum "D:\LEARN_DOCUMENTS\LTWindow\New folder\Books_Store_Management_App\Views\OrderPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "856245D1BE5175C990843808927E267EE45353F1A4D4F267E05A46D847069CCF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Books_Store_Management_App.Views
{
    partial class OrderPage : 
        global::Microsoft.UI.Xaml.Controls.Page, 
        global::Microsoft.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2409")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private static class XamlBindingSetters
        {
            public static void Set_Microsoft_UI_Xaml_Controls_ItemsControl_ItemsSource(global::Microsoft.UI.Xaml.Controls.ItemsControl obj, global::System.Object value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::System.Object) global::Microsoft.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::System.Object), targetNullValue);
                }
                obj.ItemsSource = value;
            }
            public static void Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(global::Microsoft.UI.Xaml.Controls.TextBlock obj, global::System.String value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = targetNullValue;
                }
                obj.Text = value ?? global::System.String.Empty;
            }
        };

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2409")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private partial class OrderPage_obj7_Bindings :
            global::Microsoft.UI.Xaml.IDataTemplateExtension,
            global::Microsoft.UI.Xaml.Markup.IDataTemplateComponent,
            global::Microsoft.UI.Xaml.Markup.IXamlBindScopeDiagnostics,
            global::Microsoft.UI.Xaml.Markup.IComponentConnector,
            IOrderPage_Bindings
        {
            private global::Books_Store_Management_App.Models.Order dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);
            private bool removedDataContextHandler = false;

            // Fields for each control that has bindings.
            private global::System.WeakReference obj7;
            private global::Microsoft.UI.Xaml.Controls.TextBlock obj8;
            private global::Microsoft.UI.Xaml.Controls.TextBlock obj9;
            private global::Microsoft.UI.Xaml.Controls.TextBlock obj10;
            private global::Microsoft.UI.Xaml.Controls.TextBlock obj11;
            private global::Microsoft.UI.Xaml.Controls.TextBlock obj12;
            private global::Microsoft.UI.Xaml.Controls.TextBlock obj13;

            // Static fields for each binding's enabled/disabled state
            private static bool isobj8TextDisabled = false;
            private static bool isobj9TextDisabled = false;
            private static bool isobj10TextDisabled = false;
            private static bool isobj11TextDisabled = false;
            private static bool isobj12TextDisabled = false;
            private static bool isobj13TextDisabled = false;

            public OrderPage_obj7_Bindings()
            {
            }

            public void Disable(int lineNumber, int columnNumber)
            {
                if (lineNumber == 123 && columnNumber == 116)
                {
                    isobj8TextDisabled = true;
                }
                else if (lineNumber == 124 && columnNumber == 97)
                {
                    isobj9TextDisabled = true;
                }
                else if (lineNumber == 125 && columnNumber == 97)
                {
                    isobj10TextDisabled = true;
                }
                else if (lineNumber == 126 && columnNumber == 116)
                {
                    isobj11TextDisabled = true;
                }
                else if (lineNumber == 127 && columnNumber == 116)
                {
                    isobj12TextDisabled = true;
                }
                else if (lineNumber == 128 && columnNumber == 116)
                {
                    isobj13TextDisabled = true;
                }
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 7: // Views\OrderPage.xaml line 113
                        this.obj7 = new global::System.WeakReference(global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Grid>(target));
                        break;
                    case 8: // Views\OrderPage.xaml line 123
                        this.obj8 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                        break;
                    case 9: // Views\OrderPage.xaml line 124
                        this.obj9 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                        break;
                    case 10: // Views\OrderPage.xaml line 125
                        this.obj10 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                        break;
                    case 11: // Views\OrderPage.xaml line 126
                        this.obj11 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                        break;
                    case 12: // Views\OrderPage.xaml line 127
                        this.obj12 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                        break;
                    case 13: // Views\OrderPage.xaml line 128
                        this.obj13 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                        break;
                    default:
                        break;
                }
            }
                        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2409")]
                        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                        public global::Microsoft.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target) 
                        {
                            return null;
                        }

            public void DataContextChangedHandler(global::Microsoft.UI.Xaml.FrameworkElement sender, global::Microsoft.UI.Xaml.DataContextChangedEventArgs args)
            {
                 if (this.SetDataRoot(args.NewValue))
                 {
                    this.Update();
                 }
            }

            // IDataTemplateExtension

            public bool ProcessBinding(uint phase)
            {
                throw new global::System.NotImplementedException();
            }

            public int ProcessBindings(global::Microsoft.UI.Xaml.Controls.ContainerContentChangingEventArgs args)
            {
                int nextPhase = -1;
                ProcessBindings(args.Item, args.ItemIndex, (int)args.Phase, out nextPhase);
                return nextPhase;
            }

            public void ResetTemplate()
            {
                Recycle();
            }

            // IDataTemplateComponent

            public void ProcessBindings(global::System.Object item, int itemIndex, int phase, out int nextPhase)
            {
                nextPhase = -1;
                switch(phase)
                {
                    case 0:
                        nextPhase = -1;
                        this.SetDataRoot(item);
                        if (!removedDataContextHandler)
                        {
                            removedDataContextHandler = true;
                            var rootElement = (this.obj7.Target as global::Microsoft.UI.Xaml.Controls.Grid);
                            if (rootElement != null)
                            {
                                rootElement.DataContextChanged -= this.DataContextChangedHandler;
                            }
                        }
                        this.initialized = true;
                        break;
                }
                this.Update_(global::WinRT.CastExtensions.As<global::Books_Store_Management_App.Models.Order>(item), 1 << phase);
            }

            public void Recycle()
            {
            }

            // IOrderPage_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
            }

            public void DisconnectUnloadedObject(int connectionId)
            {
                throw new global::System.ArgumentException("No unloadable elements to disconnect.");
            }

            public bool SetDataRoot(global::System.Object newDataRoot)
            {
                if (newDataRoot != null)
                {
                    this.dataRoot = global::WinRT.CastExtensions.As<global::Books_Store_Management_App.Models.Order>(newDataRoot);
                    return true;
                }
                return false;
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::Books_Store_Management_App.Models.Order obj, int phase)
            {
                OrderPage_obj7_Bindings bindings = this;
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_ID(obj.ID, phase);
                        this.Update_Customer(obj.Customer, phase);
                        this.Update_Date(obj.Date, phase);
                        this.Update_Discount(obj.Discount, phase);
                        this.Update_Amount(obj.Amount, phase);
                        this.Update_Price(obj.Price, phase);
                    }
                }
            }
            private void Update_ID(global::System.Int32 obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Views\OrderPage.xaml line 123
                    if (!isobj8TextDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(this.obj8, obj.ToString(), null);
                    }
                }
            }
            private void Update_Customer(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Views\OrderPage.xaml line 124
                    if (!isobj9TextDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(this.obj9, obj, null);
                    }
                }
            }
            private void Update_Date(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Views\OrderPage.xaml line 125
                    if (!isobj10TextDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(this.obj10, obj, null);
                    }
                }
            }
            private void Update_Discount(global::System.Double obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Views\OrderPage.xaml line 126
                    if (!isobj11TextDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(this.obj11, obj.ToString(), null);
                    }
                }
            }
            private void Update_Amount(global::System.Int32 obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Views\OrderPage.xaml line 127
                    if (!isobj12TextDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(this.obj12, obj.ToString(), null);
                    }
                }
            }
            private void Update_Price(global::System.Double obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Views\OrderPage.xaml line 128
                    if (!isobj13TextDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(this.obj13, obj.ToString(), null);
                    }
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2409")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private partial class OrderPage_obj1_Bindings :
            global::Microsoft.UI.Xaml.Markup.IDataTemplateComponent,
            global::Microsoft.UI.Xaml.Markup.IXamlBindScopeDiagnostics,
            global::Microsoft.UI.Xaml.Markup.IComponentConnector,
            IOrderPage_Bindings
        {
            private global::Books_Store_Management_App.Views.OrderPage dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Microsoft.UI.Xaml.Controls.ListView obj2;
            private global::Microsoft.UI.Xaml.Controls.ComboBox obj34;
            private global::Microsoft.UI.Xaml.Controls.ComboBox obj36;
            private global::Microsoft.UI.Xaml.Controls.ComboBox obj37;

            // Static fields for each binding's enabled/disabled state
            private static bool isobj2ItemsSourceDisabled = false;
            private static bool isobj34ItemsSourceDisabled = false;
            private static bool isobj36ItemsSourceDisabled = false;
            private static bool isobj37ItemsSourceDisabled = false;

            public OrderPage_obj1_Bindings()
            {
            }

            public void Disable(int lineNumber, int columnNumber)
            {
                if (lineNumber == 108 && columnNumber == 75)
                {
                    isobj2ItemsSourceDisabled = true;
                }
                else if (lineNumber == 33 && columnNumber == 75)
                {
                    isobj34ItemsSourceDisabled = true;
                }
                else if (lineNumber == 35 && columnNumber == 31)
                {
                    isobj36ItemsSourceDisabled = true;
                }
                else if (lineNumber == 36 && columnNumber == 31)
                {
                    isobj37ItemsSourceDisabled = true;
                }
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 2: // Views\OrderPage.xaml line 108
                        this.obj2 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ListView>(target);
                        break;
                    case 34: // Views\OrderPage.xaml line 33
                        this.obj34 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ComboBox>(target);
                        break;
                    case 36: // Views\OrderPage.xaml line 35
                        this.obj36 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ComboBox>(target);
                        break;
                    case 37: // Views\OrderPage.xaml line 36
                        this.obj37 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ComboBox>(target);
                        break;
                    default:
                        break;
                }
            }
                        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2409")]
                        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                        public global::Microsoft.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target) 
                        {
                            return null;
                        }

            // IDataTemplateComponent

            public void ProcessBindings(global::System.Object item, int itemIndex, int phase, out int nextPhase)
            {
                nextPhase = -1;
            }

            public void Recycle()
            {
                return;
            }

            // IOrderPage_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
            }

            public void DisconnectUnloadedObject(int connectionId)
            {
                throw new global::System.ArgumentException("No unloadable elements to disconnect.");
            }

            public bool SetDataRoot(global::System.Object newDataRoot)
            {
                if (newDataRoot != null)
                {
                    this.dataRoot = global::WinRT.CastExtensions.As<global::Books_Store_Management_App.Views.OrderPage>(newDataRoot);
                    return true;
                }
                return false;
            }

            public void Activated(object obj, global::Microsoft.UI.Xaml.WindowActivatedEventArgs data)
            {
                this.Initialize();
            }

            public void Loading(global::Microsoft.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::Books_Store_Management_App.Views.OrderPage obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_DisplayedOrders(obj.DisplayedOrders, phase);
                        this.Update_ShowEntities(obj.ShowEntities, phase);
                        this.Update_monthSearch(obj.monthSearch, phase);
                        this.Update_priceSearch(obj.priceSearch, phase);
                    }
                }
            }
            private void Update_DisplayedOrders(global::System.Collections.ObjectModel.ObservableCollection<global::Books_Store_Management_App.Models.Order> obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Views\OrderPage.xaml line 108
                    if (!isobj2ItemsSourceDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_ItemsControl_ItemsSource(this.obj2, obj, null);
                    }
                }
            }
            private void Update_ShowEntities(global::System.Int32[] obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Views\OrderPage.xaml line 33
                    if (!isobj34ItemsSourceDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_ItemsControl_ItemsSource(this.obj34, obj, null);
                    }
                }
            }
            private void Update_monthSearch(global::System.Int32[] obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Views\OrderPage.xaml line 35
                    if (!isobj36ItemsSourceDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_ItemsControl_ItemsSource(this.obj36, obj, null);
                    }
                }
            }
            private void Update_priceSearch(global::System.String[] obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Views\OrderPage.xaml line 36
                    if (!isobj37ItemsSourceDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_ItemsControl_ItemsSource(this.obj37, obj, null);
                    }
                }
            }
        }

        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2409")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Views\OrderPage.xaml line 108
                {
                    this.OrderListView = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ListView>(target);
                }
                break;
            case 3: // Views\OrderPage.xaml line 143
                {
                    this.PreviousButton = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.PreviousButton).Click += this.PreviousPage_Click;
                }
                break;
            case 4: // Views\OrderPage.xaml line 144
                {
                    this.PageInfo = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 5: // Views\OrderPage.xaml line 145
                {
                    this.NextButton = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.NextButton).Click += this.NextPage_Click;
                }
                break;
            case 14: // Views\OrderPage.xaml line 129
                {
                    global::Microsoft.UI.Xaml.Controls.Button element14 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)element14).Click += this.DeleteOrder_Click;
                }
                break;
            case 15: // Views\OrderPage.xaml line 133
                {
                    global::Microsoft.UI.Xaml.Controls.Button element15 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)element15).Click += this.EditOrder_Click;
                }
                break;
            case 16: // Views\OrderPage.xaml line 50
                {
                    this.ID = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.DropDownButton>(target);
                }
                break;
            case 17: // Views\OrderPage.xaml line 59
                {
                    this.Customer = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.DropDownButton>(target);
                }
                break;
            case 18: // Views\OrderPage.xaml line 68
                {
                    this.Date = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.DropDownButton>(target);
                }
                break;
            case 19: // Views\OrderPage.xaml line 77
                {
                    this.Discount = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.DropDownButton>(target);
                }
                break;
            case 20: // Views\OrderPage.xaml line 86
                {
                    this.Amount = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.DropDownButton>(target);
                }
                break;
            case 21: // Views\OrderPage.xaml line 95
                {
                    this.Price = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.DropDownButton>(target);
                }
                break;
            case 22: // Views\OrderPage.xaml line 98
                {
                    global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem element22 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem>(target);
                    ((global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem)element22).Click += this.PublisherMenuItem_Click;
                }
                break;
            case 23: // Views\OrderPage.xaml line 99
                {
                    global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem element23 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem>(target);
                    ((global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem)element23).Click += this.PublisherMenuItem_Click;
                }
                break;
            case 24: // Views\OrderPage.xaml line 89
                {
                    global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem element24 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem>(target);
                    ((global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem)element24).Click += this.PublisherMenuItem_Click;
                }
                break;
            case 25: // Views\OrderPage.xaml line 90
                {
                    global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem element25 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem>(target);
                    ((global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem)element25).Click += this.PublisherMenuItem_Click;
                }
                break;
            case 26: // Views\OrderPage.xaml line 80
                {
                    global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem element26 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem>(target);
                    ((global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem)element26).Click += this.PublisherMenuItem_Click;
                }
                break;
            case 27: // Views\OrderPage.xaml line 81
                {
                    global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem element27 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem>(target);
                    ((global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem)element27).Click += this.PublisherMenuItem_Click;
                }
                break;
            case 28: // Views\OrderPage.xaml line 71
                {
                    global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem element28 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem>(target);
                    ((global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem)element28).Click += this.PublisherMenuItem_Click;
                }
                break;
            case 29: // Views\OrderPage.xaml line 72
                {
                    global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem element29 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem>(target);
                    ((global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem)element29).Click += this.PublisherMenuItem_Click;
                }
                break;
            case 30: // Views\OrderPage.xaml line 62
                {
                    global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem element30 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem>(target);
                    ((global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem)element30).Click += this.PublisherMenuItem_Click;
                }
                break;
            case 31: // Views\OrderPage.xaml line 63
                {
                    global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem element31 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem>(target);
                    ((global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem)element31).Click += this.PublisherMenuItem_Click;
                }
                break;
            case 32: // Views\OrderPage.xaml line 53
                {
                    global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem element32 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem>(target);
                    ((global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem)element32).Click += this.PublisherMenuItem_Click;
                }
                break;
            case 33: // Views\OrderPage.xaml line 54
                {
                    global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem element33 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem>(target);
                    ((global::Microsoft.UI.Xaml.Controls.MenuFlyoutItem)element33).Click += this.PublisherMenuItem_Click;
                }
                break;
            case 34: // Views\OrderPage.xaml line 33
                {
                    global::Microsoft.UI.Xaml.Controls.ComboBox element34 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ComboBox>(target);
                    ((global::Microsoft.UI.Xaml.Controls.ComboBox)element34).TextSubmitted += this.Combo3_TextSubmitted;
                }
                break;
            case 35: // Views\OrderPage.xaml line 34
                {
                    global::Microsoft.UI.Xaml.Controls.TextBox element35 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBox>(target);
                    ((global::Microsoft.UI.Xaml.Controls.TextBox)element35).TextChanged += this.SearchTextBox_TextChanged;
                }
                break;
            case 38: // Views\OrderPage.xaml line 37
                {
                    global::Microsoft.UI.Xaml.Controls.Button element38 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)element38).Click += this.AddOrder_Click;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }


        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2409")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Microsoft.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Microsoft.UI.Xaml.Markup.IComponentConnector returnValue = null;
            switch(connectionId)
            {
            case 1: // Views\OrderPage.xaml line 1
                {                    
                    global::Microsoft.UI.Xaml.Controls.Page element1 = (global::Microsoft.UI.Xaml.Controls.Page)target;
                    OrderPage_obj1_Bindings bindings = new OrderPage_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                    global::Microsoft.UI.Xaml.Markup.XamlBindingHelper.SetDataTemplateComponent(element1, bindings);
                }
                break;
            case 7: // Views\OrderPage.xaml line 113
                {                    
                    global::Microsoft.UI.Xaml.Controls.Grid element7 = (global::Microsoft.UI.Xaml.Controls.Grid)target;
                    OrderPage_obj7_Bindings bindings = new OrderPage_obj7_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(element7.DataContext);
                    element7.DataContextChanged += bindings.DataContextChangedHandler;
                    global::Microsoft.UI.Xaml.DataTemplate.SetExtensionInstance(element7, bindings);
                    global::Microsoft.UI.Xaml.Markup.XamlBindingHelper.SetDataTemplateComponent(element7, bindings);
                }
                break;
            }
            return returnValue;
        }
    }
}

