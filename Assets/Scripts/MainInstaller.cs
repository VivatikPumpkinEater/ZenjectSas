using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private GameObject _test;
    
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        
        Container.BindInterfacesTo<ControllerError>().AsSingle().NonLazy();
        // Container.BindInterfacesTo<ControllerWarning>().AsSingle().NonLazy();
        // Container.BindInterfacesTo<ControllerDebug>().AsSingle().NonLazy();
        Container.Bind<TestView>().FromComponentInNewPrefab(_test).AsTransient().Lazy();

        Container.BindInterfacesTo<Bootstrap>().AsSingle().NonLazy();

        Container.DeclareSignal<Signal>();
    }
}

public class Bootstrap : IInitializable, ITickable
{
    private readonly DiContainer _container;
    private readonly SignalBus _signalBus;

    private int _counter = 1500;
    
    public Bootstrap(DiContainer container, SignalBus signalBus)
    {
        _container = container;
        _signalBus = signalBus;
    }

    public void Initialize()
    {
        _container.Instantiate<TestController>();
        _container.Instantiate<TestController2>();
    }

    public void Tick()
    {
        _counter--;
        
        if (_counter == 0)
        {
            _counter = 1500;
            _signalBus.Fire(new Signal(Random.ColorHSV()));
        }
    }
}

public interface IController1
{
    public void Print(string text);
}

public class ControllerError : IController1
{
    public void Print(string text)
    {
        Debug.LogError(text);
    }
}

public class ControllerWarning : IController1
{
    public void Print(string text)
    {
        Debug.LogWarning(text);
    }
}

public class ControllerDebug : IController1
{
    public void Print(string text)
    {
        Debug.Log(text);
    }
}

public class Controller2 
{
    private readonly List<IController1> _printer;

    public Controller2(List<IController1> printer)
    {
        _printer = printer;
        foreach (var controller1 in _printer)
        {
            controller1.Print("Init");
        }
    }
}
