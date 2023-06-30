using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    private IGameFactory _gameFactory;
    private IBank _bank;
    private IPersistentProgressService _persistentProgressService;
    private ISaveService _saveService;
    private PlayerSpawner _playerSpawner;
    private EnemyDeath _enemyDeath;
    private IHealth _enemyHealth;
    
    [Inject]
    private void Construct(IGameFactory gameFactory, IBank bank, 
        IPersistentProgressService persistentProgressService, 
        ISaveService saveService, PlayerSpawner playerSpawner)
    {
        _gameFactory = gameFactory;
        _bank = bank;
        _persistentProgressService = persistentProgressService;
        _saveService = saveService;
        _playerSpawner = playerSpawner;
    }

    private void Start()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        EnemyType[] enemyTypes = (EnemyType[]) Enum.GetValues(typeof(EnemyType));
        EnemyType randomEnemyType = enemyTypes[Random.Range(0, enemyTypes.Length)];
        
        GameObject enemy = _gameFactory.CreateEnemy(randomEnemyType, transform);

        _enemyHealth = enemy.GetComponent<IHealth>();
        _enemyDeath = enemy.GetComponent<EnemyDeath>();
        _enemyDeath.EnemyDestroyed += OnEnemyDestroyed;

        _playerSpawner.Player.PlayerAttack.SetAttackTarget(_enemyHealth);
    }

    private void OnEnemyDestroyed()
    {
        _enemyDeath.EnemyDestroyed -= OnEnemyDestroyed;
        _persistentProgressService.Progress.KillCount += 1;
        _bank.AddMoney(_enemyDeath.Reward * _persistentProgressService.Progress.KillCount);
        _saveService.SaveProgress();
        SpawnEnemy();
    }
}
