using UnityEngine;

public class TestView1 : TestView
{
    protected override void OnSignal(Signal obj)
    {
        _sprite.color = Color.green;
    }
}