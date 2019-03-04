using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Pressed.Pressed.Extensions;
using Xamarin.Forms;

namespace Pressed.Pressed
{
    public class TouchGestureRecognizer : BindableObject, IGestureRecognizer
    {
        public bool Capture { get; set; } = true;

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command),
            typeof(ICommand),
            typeof(TouchGestureRecognizer),
            default(ICommand));

        public static readonly BindableProperty TrackBoundaryChangesProperty = BindableProperty.Create(nameof(TrackBoundaryChanges),
            typeof(bool),
            typeof(TouchGestureRecognizer),
            default(bool), propertyChanged: OnTrackBoundaryChangesPropertyChanged);

        /// <summary>
        /// TouchEventCommand summary. This is a bindable property.
        /// </summary>
        public ICommand Command
        {
            get => (ICommand) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// TrackBoundaryChanges summary. This is a bindable property.
        /// </summary>
        public bool TrackBoundaryChanges
        {
            get => (bool) GetValue(TrackBoundaryChangesProperty);
            set => SetValue(TrackBoundaryChangesProperty, value);
        }
        
        public void RaiseTouchAction(Xamarin.Forms.View view, TouchEventArgs args)
        {
            Debug.WriteLine($"Changed VisualState to {args.TouchState}");
            VisualStateManager.GoToState(view, args.TouchState.ToString());

            if (Command?.CanExecute(args) == true)
                Command.Execute(args);
        }

        private static void OnTrackBoundaryChangesPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue == newValue) return;
            var self = (Xamarin.Forms.View) bindable;

            if (!(self.GestureRecognizers.FirstOrDefault(x => x.Is<TouchGestureRecognizer>()) is TouchGestureRecognizer effect)) return;
            effect.Capture = !(bool) newValue;
        }
    }
}