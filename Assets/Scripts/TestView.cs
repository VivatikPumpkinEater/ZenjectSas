using System;
using UnityEngine;
using Zenject;

public class TestView : MonoBehaviour
{
    private IController1 _controllerError;
    private SignalBus _signalBus;

    protected SpriteRenderer _sprite;
    
    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }
    
    [Inject]
    public void Construct(IController1 controllerError, SignalBus signalBus)
    {
        _controllerError = controllerError;
        _signalBus = signalBus;
        
        _signalBus.Subscribe<Signal>(OnSignal);
    }

    protected virtual void OnSignal(Signal obj)
    {
        
    }

    private void OnDestroy()
    {
        _signalBus.TryUnsubscribe<Signal>(OnSignal);
    }
}

public class TestController
{
    private readonly TestView1 _view;

    public TestController(TestView1 view)
    {
        _view = view;
    }
}

public class TestController2
{
    private readonly TestView2 _view;

    public TestController2(TestView2 view)
    {
        _view = view;
    }
}