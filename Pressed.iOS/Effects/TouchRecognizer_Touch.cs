using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Foundation;
using Pressed.Pressed;
using Pressed.Pressed.Extensions;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Pressed.iOS.Effects
{
    // ReSharper disable InconsistentNaming
    class TouchRecognizer_Touch : UIGestureRecognizer
    // ReSharper restore InconsistentNaming
    {
        private readonly View _formsView; // Forms element for firing events
        private readonly UIView _uiView; // iOS UIView 
        private static readonly Dictionary<UIView, TouchRecognizer_Touch> _viewDictionary = new Dictionary<UIView, TouchRecognizer_Touch>();

        private static readonly Dictionary<long, TouchRecognizer_Touch> _idToTouchDictionary = new Dictionary<long, TouchRecognizer_Touch>();
        private bool _constrainToView;

        public TouchRecognizer_Touch(View formsView, UIView uiView)
        {
            _formsView = formsView;
            _uiView = uiView;

            _viewDictionary.Add(uiView, this);
        }


        public void Detach()
        {
            _viewDictionary.Remove(_uiView);
        }
        
        
        // touches = touches of interest; evt = all touches of type UITouch
        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            var id = touches.Handle.ToInt64();
            RaiseEvent(this, id, TouchState.Pressed, touches, true);

            if (!_idToTouchDictionary.ContainsKey(id))
            {
                _idToTouchDictionary.Add(id, this);
            }
            
            // Save the setting of the Capture property
            // note: this will eventually be _formsView.TrackBoundaryChanges
            _constrainToView = !Pressed.VisualElement.GetTrackBoundaryChanges(_formsView);
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);

            var id = touches.Handle.ToInt64();

            if (_constrainToView)
            {
                RaiseEvent(this, id, TouchState.Changed, touches, true);
            }
            else
            {
                CheckForBoundaryHop(touches);

                if (_idToTouchDictionary[id] != null)
                {
                    RaiseEvent(_idToTouchDictionary[id], id, TouchState.Changed, touches, true);
                }
            }
            
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

                long id = touches.Handle.ToInt64();

                if (_constrainToView)
                {
                    RaiseEvent(this, id, TouchState.Released, touches, false);
                }
                else
                {
                    CheckForBoundaryHop(touches);

                    if (_idToTouchDictionary[id] != null)
                    {
                        RaiseEvent(_idToTouchDictionary[id], id, TouchState.Released, touches, false);
                    }
                }

                _idToTouchDictionary.Remove(id);
            
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);

           
                long id = touches.Handle.ToInt64();

                if (_constrainToView)
                {
                    RaiseEvent(this, id, TouchState.Cancelled, touches, false);
                }
                else if (_idToTouchDictionary[id] != null)
                {
                    RaiseEvent(_idToTouchDictionary[id], id, TouchState.Cancelled, touches, false);
                }

                _idToTouchDictionary.Remove(id);
            
        }

        void CheckForBoundaryHop(NSSet touches)
        {
            long id = touches.Handle.ToInt64();

            // TODO: Might require converting to a List for multiple hits
            TouchRecognizer_Touch recognizerHit = null;

            foreach (UIView view in _viewDictionary.Keys)
            {
                var locations = touches.Cast<UITouch>().Select(touch => touch.LocationInView(view));
                if (locations.Any(location => IsInView(view, location)))
                {
                    recognizerHit = _viewDictionary[view];
                }
            }

            if (!ReferenceEquals(recognizerHit, _idToTouchDictionary[id]))
            {
                if (_idToTouchDictionary[id] != null)
                {
                    RaiseEvent(_idToTouchDictionary[id], id, TouchState.Exited, touches, true);
                }

                if (recognizerHit != null)
                {
                    RaiseEvent(recognizerHit, id, TouchState.Entered, touches, true);
                }

                _idToTouchDictionary[id] = recognizerHit;
            }
        }

        private static bool IsInView(UIView view, CGPoint location) => new CGRect(new CGPoint(), view.Frame.Size).Contains(location);

        private static void RaiseEvent(TouchRecognizer_Touch recognizer, long id, TouchState actionType, NSSet touches, bool isInContact)
        {
            var formsPoints = touches.Cast<UITouch>()
                                     .Select(touch => touch.LocationInView(recognizer.View))
                                     .Select( cgPoint => new TouchPoint(cgPoint.ToPoint(), IsInView(recognizer.View, cgPoint)))
                                     .ToList();
            
            // Get the method to call for firing events
            // note: this would live on the VisualElement and not in an effect :)
            var effect = recognizer._formsView.Effects.FirstOrDefault(x => x.Is<TouchEffect>()) as TouchEffect;
            effect?.SendTouched(recognizer._formsView, new TouchEventArgs(actionType, formsPoints, id, isInContact));
        }
    }
}