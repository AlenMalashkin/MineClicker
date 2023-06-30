using System;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class EnemyDeath : MonoBehaviour
{
    public event Action EnemyDestroyed;

    public int Reward { get; set; }

    [SerializeField] private AudioClip deathSfx; 
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private Transform enemyTransform;

    private Sound _sound;
    private Sequence _dieSeq;

    [Inject]
    private void Construct(Sound sound)
    {
        _sound = sound;
    }
    
    private void OnEnable()
    {
        enemyHealth.HealthChanged += OnEnemyHealthChanged;
    }
    
    private void OnEnemyHealthChanged(int health)
    {
        if (health <= 0)
            Die();
    }

    private void Die()
    {
        enemyHealth.HealthChanged -= OnEnemyHealthChanged;
        _sound.PlaySfx(deathSfx);
        deathParticles.Play();
        StartDieSeq(enemyTransform);
    }
    
    private void StartDieSeq(Transform enemeyTransform)
    {
        _dieSeq = DOTween.Sequence()
            .Append(enemeyTransform.DORotate(new Vector3(-90, 0, 0), 1f))
            .OnComplete(DestroyObject);
    }

    private void DestroyObject()
    {
        EnemyDestroyed?.Invoke();
        Destroy(gameObject);
    }
}
