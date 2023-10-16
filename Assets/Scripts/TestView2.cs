using System;
using UnityEngine;

public class TestView2 : TestView
{
    protected override void OnSignal(Signal obj)
    {
        _sprite.color = obj.Color;
    }
}