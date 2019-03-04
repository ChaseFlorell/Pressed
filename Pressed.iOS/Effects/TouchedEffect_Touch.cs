using System;
using System.Linq;
using Pressed.iOS.Effects;
using Pressed.Pressed;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Pressed.Pressed.Extensions;
using VisualElement = Xamarin.Forms.VisualElement;

[assembly: ExportEffect(typeof(TouchEffect_Touch), nameof(TouchEffect))]
namespace Pressed.iOS.Effects
{
    // ReSharper disable InconsistentNaming
    public class TouchEffect_Touch : PlatformEffect
    // ReSharper restore InconsistentNaming
    {
        private TouchRecognizer_Touch _uiGestureRecognizer;
        private Xamarin.Forms.View _formsElement;
        private UIView UiView => Control ?? Container;
        
        protected override void OnAttached()
        {
            if(!Element.Is(out _formsElement)) throw new InvalidOperationException("Can only attach Touch Effects to Views");
            
            var formsGestureRecognizer = (TouchGestureRecognizer)_formsElement.GestureRecognizers.First(x => x.Is<TouchGestureRecognizer>());
            
            _uiGestureRecognizer = new TouchRecognizer_Touch(_formsElement, UiView, formsGestureRecognizer);
            
            UiView.UserInteractionEnabled = true;
            
            if(!(UiView.GestureRecognizers is null) && UiView.GestureRecognizers.Contains(_uiGestureRecognizer))
                UiView.RemoveGestureRecognizer(_uiGestureRecognizer);
            
            UiView.AddGestureRecognizer (_uiGestureRecognizer);
        }

        protected override void OnDetached()
        {
            UiView.RemoveGestureRecognizer(_uiGestureRecognizer);
            _uiGestureRecognizer.Detach();
        }
    }
}