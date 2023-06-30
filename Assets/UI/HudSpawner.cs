using UnityEngine;
using Zenject;

public class HudSpawner : MonoBehaviour
{
    private IGameFactory _gameFactory;
    private ISceneObjectsProvider _sceneObjectsProvider;
    
    [Inject]
    private void Construct(IGameFactory gameFactory, ISceneObjectsProvider sceneObjectsProvider)
    {
        _gameFactory = gameFactory;
        _sceneObjectsProvider = sceneObjectsProvider;
    }

    private void Start()
    {
        GameObject hud = _gameFactory.CreateHud(transform);
        _sceneObjectsProvider.SceneObjectsMap.Add(typeof(Hud), hud);
    }
}
