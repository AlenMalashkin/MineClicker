using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private PlayerSpawner playerSpawner;
    [SerializeField] private HudSpawner hudSpawner;

    public override void InstallBindings()
    {
        BindEnemySpawner();
        BindPlayerSpawner();
        BindHud();
    }

    private void BindEnemySpawner()
    {
        Bind(enemySpawner);
    }

    private void BindPlayerSpawner()
    {
        Bind(playerSpawner);
    }

    private void BindHud()
    {
        Bind(hudSpawner);
    }

    private void Bind<T>(T instance)
    {
        Container
            .Bind<T>()
            .FromInstance(instance)
            .AsSingle();
    }
}