using System;
using System.Diagnostics;
using Pressed.Pressed;
using Xamarin.Forms;
using VisualElement = Xamarin.Forms.VisualElement;

namespace Pressed
{
    public partial class MainPage
    {
        private bool _isPressed;

        public MainPage()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            Debug.WriteLine("Tapped!!!");
        }

        private void TouchGestureRecognizer_OnTouched(object sender, TouchEventArgs e)
        {
            var view = (View) sender;
            RotateTheView(view.FindByName<Label>("InnerLabel"), e.TouchState);
        }

        private async void RotateTheView(VisualElement element, TouchState touchState)
        {
            switch (touchState)
            {
                case TouchState.Pressed:
                    _isPressed = true;
                    break;
                case TouchState.Released:
                    _isPressed = false;
                    break;
                default: return;
            }

            while (_isPressed)
            {
                await element.RotateTo(360, 500, Easing.Linear);
                await element.RotateTo(0, 0); // reset to initial position
            }
        }
    }
}
