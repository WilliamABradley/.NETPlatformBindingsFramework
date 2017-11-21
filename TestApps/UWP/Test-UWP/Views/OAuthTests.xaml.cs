﻿using Test_UWP.Services;
using Tests.Tests;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Test_UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OAuthTests : Page
    {
        public OAuthTestPage Viewmodel { get; }

        public OAuthTests()
        {
            this.InitializeComponent();
            Viewmodel = new OAuthTestPage(new UWPTestPageGenerator(this));
            Viewmodel.DisplayTests();
        }
    }
}