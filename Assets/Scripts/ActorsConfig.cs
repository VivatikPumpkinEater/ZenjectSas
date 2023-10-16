using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// Хранит View всех игровых обьектов.
/// Когфиг биндит обекты в Zenject ситиеме для быстрого доступа.
/// ключем доступа выступает тип.
/// </summary>
[CreateAssetMenu(fileName = "ActorsConfig", menuName = "ScriptableObjects/ActorsConfig", order = 1)]
public class ActorsConfig : ScriptableObjectInstaller
{
    [SerializeField] private List<GameObject> _views;

    public override void InstallBindings()
    {
        foreach (var viewInfo in _views)
        {
            Container.Bind(viewInfo.GetComponent<TestView>().GetType())
                .FromComponentInNewPrefab(viewInfo).AsTransient();
        }
    }
}