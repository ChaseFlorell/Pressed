using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Pressed.Pressed
{
    public class TouchEventArgs : EventArgs
    {
        public TouchEventArgs(TouchState touchState, IList<TouchPoint> touchPoints, long id, bool isInContact)
        {
            TouchState = touchState;
            TouchPoints = new ReadOnlyCollection<TouchPoint>(touchPoints);
            Id = id;
            IsInContact = isInContact;
        }
        
        public TouchState TouchState { get; }
        
        // to line up with the comment on "TouchState", we could make this enumerable and the count would indicate the number of fingers touching
        public IReadOnlyCollection<TouchPoint> TouchPoints { get; }
        public long Id { get; }
        public bool IsInContact { get; }
    }
}