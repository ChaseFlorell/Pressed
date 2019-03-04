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
        
        // to line up with the comment on "TouchState", we could make this enumerable and the count would indicate the number of fingers touching
        public Point Point { get; }
        public long Id { get; }
        public bool IsInContact { get; }
    }
}