// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Microsoft.PowerToys.Settings.UI.Flyout
{
    public sealed partial class GeneralView : Page
    {
        public GeneralView()
        {
            this.InitializeComponent();
        }

        private void Options_Click(object sender, RoutedEventArgs e)
        {
            Button selectedButton = sender as Button;

            Frame selectedFrame = this.Parent as Frame;

            switch ((string)selectedButton.Tag)
            {
                case "Awake": selectedFrame.Navigate(typeof(AwakeView), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight }); break;

                // case "FancyZones": selectedFrame.Navigate(typeof(FancyZonesView), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight }); break;
            }
        }
    }
}
