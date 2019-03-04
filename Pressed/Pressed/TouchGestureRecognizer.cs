using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Pressed.Pressed
{
    public class TouchGestureRecognizer : BindableObject, IGestureRecognizer
    {
        public event EventHandler<TouchEventArgs> Touched; 

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command),
            typeof(ICommand),
            typeof(TouchGestureRecognizer),
            default(ICommand));

        /// <summary>
        /// TouchEventCommand summary. This is a bindable property.
        /// </summary>
        public ICommand Command
        {
            get => (ICommand) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
        
        [EditorBrowsable(EditorBrowsableState.Never)]

        public void SendTouched(View view, TouchEventArgs args)
        {
            if (Command?.CanExecute(args) == true)
                Command.Execute(args);
            
            Touched?.Invoke(view, args);
        }
    }
}