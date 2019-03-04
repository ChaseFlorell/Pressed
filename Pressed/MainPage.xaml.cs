using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Pressed
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            Debug.WriteLine("Tapped!!!");
        }
    }
}
