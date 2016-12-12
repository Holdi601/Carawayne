using System;
using UnityEngine;
using System.Collections;

public class IntDim
{
    private int value;
    public int max;
    public int min;

    public IntDim(int _value, int _maxVal, int _minVal)
    {
        max = _maxVal;
        min = _minVal;
        Value = _value;
    }

    public IntDim(int _maxVal, int _minVal)
    {
        max = _maxVal;
        min = _minVal;
    }

    public IntDim()
    {
    }

    //Todo: new IntDim resets max/min. Rework...
    public static implicit operator IntDim(int _value)
    {
        return new IntDim {Value = _value};
    }

    public static implicit operator int(IntDim _value)
    {
        return _value.value;
    }

    private int Value
    {
        get { return value; }
        set
        {
            this.value = Math.Max(value, min);
            this.value = Math.Min(value, max);
        }
    }
}
