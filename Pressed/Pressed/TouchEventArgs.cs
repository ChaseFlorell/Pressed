using System;
using Xamarin.Forms;

namespace Pressed.Pressed
{
    public class TouchEventArgs : EventArgs
    {
        public TouchEventArgs(TouchState touchState, Point point, long id, bool isInContact)
        {
            TouchState = touchState;
            Point = point;
            Id = id;
            IsInContact = isInContact;
        }
        
        public TouchState TouchState { get; }
        public Point Point { get; }
        public long Id { get; }
        public bool IsInContact { get; }
    }
}