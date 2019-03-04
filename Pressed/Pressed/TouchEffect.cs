using System;
using System.ComponentModel;
using System.Linq;
using Pressed.Pressed.Extensions;
using Xamarin.Forms;

namespace Pressed.Pressed
{
    public class TouchEffect : RoutingEffect
    {
        // note: the usefulness of the Effect is simply because we don't have direct access to VisualElement
        // and therefore we use an effect. If this were baked into Xamarin.Forms, we can simply add the API to VisualElement directly
        public TouchEffect() : base($"Xfx.{nameof(TouchEffect)}") { }

        public event EventHandler<TouchEventArgs> Touched;
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SendTouched(Xamarin.Forms.VisualElement visualElement, TouchEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine($"Tracked as {args.TouchState} at point {args.Point.X}:{args.Point.Y}.");
            VisualStateManager.GoToState(visualElement, args.TouchState.ToString());
            Touched?.Invoke(visualElement, args);
            
            if (!(visualElement is View view)) return;

            foreach (var gesture in view.GestureRecognizers.Where(x => x.Is<TouchGestureRecognizer>()))
            {
                var touchGesture = gesture as TouchGestureRecognizer;
                touchGesture?.SendTouched(view, args);
            }            
        }
    }
}