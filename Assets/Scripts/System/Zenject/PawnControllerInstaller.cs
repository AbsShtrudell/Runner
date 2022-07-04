using UnityEngine;
using Zenject;

public class PawnControllerInstaller : MonoInstaller
{
    [SerializeField]
    private PawnsController controller;

    public override void InstallBindings()
    {
        Container.Bind<PawnsController>().FromInstance(controller).AsSingle();
    }
}