using System;

namespace Pressed.Pressed
{
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
        Hover = 1<<8,
    }
}