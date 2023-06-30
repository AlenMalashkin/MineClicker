using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private AudioClip musicLoop;
    
    private DiContainer _diContainer;
    private Sound _sound;
    private INumFormatterService _numFormatterService;
    private IAssets _assets;
    private IStaticDataLoadService _staticDataLoadService;
    private IPersistentProgressService _persistentProgressService;
    private ISceneManagementService _sceneManagementService;
    private ISaveService _saveService;
    private ILoadProgressService _loadProgressService;
    private IGameFactory _gameFactory;
    private IPlayerClickHandlerService _playerClickHandlerService;
    private IPlayerBoostService _playerBoostService;
    private IBank _bank;
    private ISceneObjectsProvider _sceneObjectsProvider;

    private List<IPlayerBooster> _boosters;

    [Inject]
    private void Construct(DiContainer diContainer, Sound sound)
    {
        _diContainer = diContainer;
        _sound = sound;
    }
    
    private void Awake()
    {
        InitializeAll();
        RegisterAll();
        _sound.PlayMusic(musicLoop);
    }

    private void InitializeAll()
    {
        _numFormatterService = new NumFormatterService();
        _staticDataLoadService = new StaticDataLoadService();
        _assets = new Assets();
        _persistentProgressService = new PersistentProgressService();
        _gameFactory = new GameFactory(_staticDataLoadService, _persistentProgressService, _assets, _diContainer);
        _saveService = new SaveService(_persistentProgressService, _gameFactory);
        _loadProgressService = new LoadProgressService(_saveService, _persistentProgressService);
        _sceneManagementService = new SceneManagementService(_staticDataLoadService);
        _playerClickHandlerService = new PlayerClickHandlerService();
        _bank = new Bank(_persistentProgressService, _saveService);
        _sceneObjectsProvider = new SceneObjectsProvider();
        _playerBoostService = new PlayerBoostService(_staticDataLoadService, _sceneObjectsProvider);

        _staticDataLoadService.LoadData();
        _playerBoostService.InitBoostersMap();
        _loadProgressService.LoadOrInitNewProgress();
        _sceneManagementService.LoadScene(Scene.Main);
    }

    private void RegisterAll()
    {
        Register(_numFormatterService);
        Register(_staticDataLoadService);
        Register(_assets);
        Register(_persistentProgressService);
        Register(_gameFactory);
        Register(_saveService);
        Register(_loadProgressService);
        Register(_sceneManagementService);
        Register(_playerClickHandlerService);
        Register(_bank);
        Register(_playerBoostService);
        Register(_sceneObjectsProvider);
    }

    private void Register<TService>(TService instance)
    {
        _diContainer
            .Bind<TService>()
            .FromInstance(instance)
            .AsSingle();
    }
}
