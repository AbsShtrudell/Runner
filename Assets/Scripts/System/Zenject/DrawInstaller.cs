using UnityEngine;
using Zenject;

public class DrawInstaller : MonoInstaller
{
    [SerializeField]
    private DrawController controller;

    public override void InstallBindings()
    {
        Container.Bind<DrawController>().FromInstance(controller).AsSingle();
    }
}