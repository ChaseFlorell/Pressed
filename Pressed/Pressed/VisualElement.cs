using System.Linq;
using Pressed.Pressed.Extensions;
using Xamarin.Forms;

namespace Pressed.Pressed
{
    // note: I don't think any of this needs to be an attached property. Instead this would all end up on `Xamarin.Forms.VisualElement`
    public class VisualElement
    {
// ReSharper disable InconsistentNaming
#pragma warning disable 649
        private static string ObservesTouches;
        private static string TrackBoundaryChanges;
#pragma warning restore 649
// ReSharper restore InconsistentNaming

        public static readonly BindableProperty ObservesTouchesProperty = BindableProperty.Create(nameof(ObservesTouches),
            typeof(bool),
            typeof(Xamarin.Forms.VisualElement),
            default(bool), propertyChanged: OnObservesTouchesPropertyChanged);

        public static readonly BindableProperty TrackBoundaryChangesProperty = BindableProperty.Create(nameof(TrackBoundaryChanges),
            typeof(bool),
            typeof(Xamarin.Forms.VisualElement),
            default(bool));
        
        public static bool GetTrackBoundaryChanges(Xamarin.Forms.VisualElement view) => (bool) view.GetValue(TrackBoundaryChangesProperty);
        public static void SetTrackBoundaryChanges(Xamarin.Forms.VisualElement view, bool value) => view.SetValue(TrackBoundaryChangesProperty, value);
        public static bool GetObservesTouches(Xamarin.Forms.VisualElement view) => (bool) view.GetValue(ObservesTouchesProperty);
        public static void SetObservesTouches(Xamarin.Forms.VisualElement view, bool value) => view.SetValue(ObservesTouchesProperty, value);

        private static void OnObservesTouchesPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue == newValue) return;
            // in the future, the effect won't matter...
            var self = (Xamarin.Forms.VisualElement) bindable;

            if ((bool) newValue)
            {
                if(!self.Effects.Any(x => x.Is<TouchEffect>())) self.Effects.Add(new TouchEffect());
            }
            else
            {
                var effect = self.Effects.FirstOrDefault(x => x.Is<TouchEffect>());
                if (effect != null) self.Effects.Remove(effect);
            }
        }
    }
}