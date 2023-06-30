using System;
using UnityEngine;
using Zenject;

public class PlayerSpawner : MonoBehaviour
{
    public Player Player { get; private set; }

    private IGameFactory _gameFactory;
    private ISceneObjectsProvider _sceneObjectsProvider;
    
    [Inject]
    private void Construct(IGameFactory gameFactory, ISceneObjectsProvider sceneObjectsProvider)
    {
        _gameFactory = gameFactory;
        _sceneObjectsProvider = sceneObjectsProvider;
    }

    private void Awake()
    {
        SpawnPlayer();
    }

    private void Start()
    {
        if (!_sceneObjectsProvider.SceneObjectsMap.ContainsKey(typeof(Player)) && Player.gameObject != null)
            _sceneObjectsProvider.SceneObjectsMap.Add(typeof(Player), Player.gameObject);
    }

    private void SpawnPlayer()
    {
        Player = _gameFactory.CreatePlayer(transform).GetComponent<Player>();
    }
}
