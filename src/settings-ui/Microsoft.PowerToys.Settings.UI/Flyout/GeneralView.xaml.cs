// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading;
using Microsoft.PowerToys.Settings.UI.Views;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Microsoft.PowerToys.Settings.UI.Flyout
{
    public sealed partial class GeneralView : Page
    {
        private Func<string, int> SendConfigMSG { get; }

        private Frame selectedFrame;

        public GeneralView()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            selectedFrame = this.Parent as Frame;
        }

        private void ColorPicker_Click(object sender, RoutedEventArgs e)
        {
            if (ShellPage.ColorPickerSharedEventCallback != null)
            {
                using (var eventHandle = new EventWaitHandle(false, EventResetMode.AutoReset, ShellPage.ColorPickerSharedEventCallback()))
                {
                    eventHandle.Set();
                }
            }
        }

        private void Run_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (ShellPage.RunSharedEventCallback != null)
            {
                using (var eventHandle = new EventWaitHandle(false, EventResetMode.AutoReset, ShellPage.RunSharedEventCallback()))
                {
                    eventHandle.Set();
                }
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            if (ShellPage.OpenMainWindowCallback != null)
            {
                ShellPage.OpenMainWindowCallback(typeof(GeneralPage));
            }
        }

        private void FancyZonesButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void AwakeButton_Click(object sender, RoutedEventArgs e)
        {
            selectedFrame.Navigate(typeof(AwakeView), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void ShortcutGuideButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void VCMButton_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
