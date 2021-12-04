// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Windows;
using System.Windows.Interop;
using interop;
using Microsoft.PowerToys.Settings.UI.Flyout;
using Microsoft.PowerToys.Settings.UI.Helpers;
using Microsoft.Toolkit.Wpf.UI.XamlHost;
using Windows.ApplicationModel.Resources;

namespace PowerToys.Settings
{
    public partial class FlyoutPage : Window
    {
        private static Window inst;
        private ShellPage shellPage;

        public FlyoutPage()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            /*
            if (shellPage != null)
            {
                shellPage.OnClosing();
            }

            inst = null;
            MainWindow.CloseHiddenWindow(); */
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (inst != null)
            {
                inst.Close();
            }

            inst = this;

            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width - 16;
            this.Top = desktopWorkingArea.Bottom - this.Height - 16;
        }

        private void WindowsXamlHost_ChildChanged(object sender, EventArgs e)
        {
            if (sender == null)
            {
                return;
            }

            WindowsXamlHost windowsXamlHost = sender as WindowsXamlHost;
            shellPage = windowsXamlHost.GetUwpInternalObject() as ShellPage;

            ShellPage.SetRunSharedEventCallback(() =>
            {
                return Constants.PowerLauncherSharedEvent();
            });

            ShellPage.SetColorPickerSharedEventCallback(() =>
            {
                return Constants.ShowColorPickerSharedEvent();
            });

            ShellPage.SetOpenMainWindowCallback((Type type) =>
            {
                ((App)Application.Current).OpenSettingsWindow(type);
            });
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var hwnd = new WindowInteropHelper(this).Handle;
            NativeMethods.SetPopupStyle(hwnd);
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            // OnDeactivated(e);
            // Close();
        }
    }
}
