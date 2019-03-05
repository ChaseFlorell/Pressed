SPEC

_Currently only iOS is functioning_

Extend View
```csharp
public class View
{
    ///<summary>
    /// When enabled, the View will begin listening for Touches
    ///</sumary>
    ///<remarks>
    /// When a touch is detected, it will raise <see cref="Touched" /> and also toggle the VisualState to the TouchState
    ///</remarks>
    public bool ObservesTouches { get; set; } // should this be bindable?

    ///<summary>
    /// When enabled, the tracked touches will include Views outside the bounds of the current View
    ///</sumary>
    ///<remarks>
    /// Adding this will extend the TouchState to include <see cref="TouchState.Entered" /> and <see cref="TouchState.Exited" />
    ///</remarks>
    public bool TrackBoundaryChanges { get; set; }

    ///<summary>
    /// Raised when <see cref="ObservesTouches"/> is enabled and a touch is detected
    ///<summary>
    event EventArgs<TouchEventArgs> Touched;
}
```

New Gesture Recognizer
```csharp
public class TouchGestureRecognizer : BindableObject, IGestureRecognizer
{
    ///<summary>
    /// Raised when <see cref="View.ObservesTouches"/> is enabled and a touch is detected
    ///<summary>
    public event EventHandler<TouchEventArgs> Touched; 

    ///<summary>
    /// Raised when <see cref="View.ObservesTouches"/> is enabled and a touch is detected
    ///<summary>
    public ICommand Command { get; set; }
}
```

New TouchState
```csharp
[Flags]
public enum TouchState
{
    Entered = 1<<0,
    Exited = 1<<2,
    Cancelled = 1<<3,
    Failed = 1<<4,
    Changed = 1<<5,
    Pressed = 1<<6, 
    Released = 1<<7,
    Hover = 1<<8
}
```

New TouchEventArgs
```csharp
public class TouchEventArgs : EventArgs
{   
    public TouchState TouchState { get; }
    public IReadOnlyList<TouchPoint> TouchPoints { get; }
    public long Id { get; }
    public bool IsInContact { get; }
}
```

New TouchPoints
```csharp
public Xamarin.Forms.Point Point { get; }
public bool IsInOriginalView { get; }
```

Additional States
```xml
<VisualStateGroup x:Name="PressedStates">
    <VisualState x:Name="Entered" />
    <VisualState x:Name="Exited" />
    <VisualState x:Name="Cancelled" />
    <VisualState x:Name="Failed" />
    <VisualState x:Name="Changed" />
    <VisualState x:Name="Pressed" />
    <VisualState x:Name="Released" />
    <VisualState x:Name="Hover" />
</VisualStateGroup>
```


![https://media.giphy.com/media/9JtdVObFQ6myYGuO0q/giphy.gif](https://media.giphy.com/media/9JtdVObFQ6myYGuO0q/giphy.gif)
