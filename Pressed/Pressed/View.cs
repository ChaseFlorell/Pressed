using System.Linq;
using System.Windows.Input;
using Pressed.Pressed.Extensions;
using Xamarin.Forms;

namespace Pressed.Pressed
{
    public class View
    {
// ReSharper disable InconsistentNaming
#pragma warning disable 649
        private static string ObservesTouches;
        private static string TouchEventCommand;
        private static string TrackBoundaryChanges;
#pragma warning restore 649
// ReSharper restore InconsistentNaming

        public static readonly BindableProperty ObservesTouchesProperty = BindableProperty.Create(nameof(ObservesTouches),
            typeof(bool),
            typeof(Xamarin.Forms.View),
            default(bool), propertyChanged: OnObservesTouchesPropertyChanged);
        
        public static readonly BindableProperty TouchEventCommandProperty = BindableProperty.Create(nameof(TouchEventCommand),
            typeof(ICommand),
            typeof(Xamarin.Forms.View),
            default(ICommand));

        public static readonly BindableProperty TrackBoundaryChangesProperty = BindableProperty.Create(nameof(TrackBoundaryChanges),
            typeof(bool),
            typeof(Xamarin.Forms.View),
            default(bool), propertyChanged: OnTrackBoundaryChangesPropertyChanged);
        
        public static bool GetTrackBoundaryChanges(Xamarin.Forms.View view) => (bool) view.GetValue(TrackBoundaryChangesProperty);
        public static void SetTrackBoundaryChanges(Xamarin.Forms.View view, bool value) => view.SetValue(TrackBoundaryChangesProperty, value);
        public static ICommand GetTouchEventCommand(Xamarin.Forms.View view) => (ICommand) view.GetValue(TouchEventCommandProperty);
        public static void SetTouchEventCommand(Xamarin.Forms.View view, ICommand value) => view.SetValue(TouchEventCommandProperty, value);
        public static bool GetObservesTouches(Xamarin.Forms.View view) => (bool) view.GetValue(ObservesTouchesProperty);
        public static void SetObservesTouches(Xamarin.Forms.View view, bool value) => view.SetValue(ObservesTouchesProperty, value);

        private static void OnObservesTouchesPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue == newValue) return;
            var self = (Xamarin.Forms.View) bindable;

            if ((bool) newValue)
            {
                var touchGestureRecognizer = new TouchGestureRecognizer()
                {
                    Capture = !GetTrackBoundaryChanges(self)
                };
                if (!self.GestureRecognizers.Any(x => x.Is<TouchGestureRecognizer>())) self.GestureRecognizers.Add(touchGestureRecognizer);
                
                if(!self.Effects.Any(x => x.Is<TouchEffect>())) self.Effects.Add(new TouchEffect());
            }
            else
            {
                var gestureRecognizer = self.GestureRecognizers.FirstOrDefault(x => x.Is<TouchGestureRecognizer>());
                var effect = self.Effects.FirstOrDefault(x => x.Is<TouchEffect>());
                if (gestureRecognizer != null) self.GestureRecognizers.Remove(gestureRecognizer);
                if (effect != null) self.Effects.Remove(effect);
            }
        }
        
        private static void OnTrackBoundaryChangesPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue == newValue) return;
            var self = (Xamarin.Forms.View) bindable;

            if (!(self.GestureRecognizers.FirstOrDefault(x => x.Is<TouchGestureRecognizer>()) is TouchGestureRecognizer touchGestureRecognizer)) return;
            touchGestureRecognizer.Capture = !(bool) newValue;
        }
    }
}