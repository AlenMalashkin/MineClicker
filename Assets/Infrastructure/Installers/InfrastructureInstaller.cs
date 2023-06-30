using UnityEngine;
using YG;
using Zenject;

public class InfrastructureInstaller : MonoInstaller
{
    [SerializeField] private Bootstrapper bootstrapperPrefab;
    [SerializeField] private Sound soundPrefab;
    [SerializeField] private YandexGame yandexGamePrefab;
    
    public override void InstallBindings()
    {
        BindSound();
        BindBootstrapper();
        BindYandexSdk();
    }

    private void BindBootstrapper()
    {
        Bootstrapper bootstrapper = Container.InstantiatePrefabForComponent<Bootstrapper>(bootstrapperPrefab);
        
        Container
            .Bind<Bootstrapper>()
            .FromInstance(bootstrapper)
            .AsSingle();
    }

    private void BindSound()
    {
        Sound sound = Container.InstantiatePrefabForComponent<Sound>(soundPrefab);
        
        Container
            .Bind<Sound>()
            .FromInstance(sound)
            .AsSingle();
    }

    private void BindYandexSdk()
    {
        YandexGame yandexGame = Container.InstantiatePrefabForComponent<YandexGame>(yandexGamePrefab);
        
        Container
            .Bind<YandexGame>()
            .FromInstance(yandexGame)
            .AsSingle();
    }
}