using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class EnemyHealth : MonoBehaviour, IHealth
{
    [SerializeField] private AudioClip hitSfx;
    [SerializeField] private ParticleSystem punchParticles;
    [SerializeField] private Transform enemyTransform;
    [SerializeField] private List<Vector3> directionsToRotate;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    private Sound _sound;
    private Sequence _takeDamageSeq;
    
    public event Action<int> HealthChanged;

    public int MaxHealth
    {
        get => maxHealth; 
        set => maxHealth = value;
    }

    public int CurrentHealth
    {
        get => currentHealth = currentHealth < 0 ? 0 : currentHealth;
        set => currentHealth = value;
    }

    [Inject]
    private void Construct(Sound sound)
    {
        _sound = sound;
    }
    
    private void StartTakeDamageSeq(Transform enemyTransform)
    {
        _takeDamageSeq?.Kill();

        _takeDamageSeq = DOTween.Sequence()
            .Append(enemyTransform.DORotate(directionsToRotate[Random.Range(0, directionsToRotate.Count)], 0.1f))
            .Append(enemyTransform.DORotate(new Vector3(0, 0, 0), 0.1f));
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0)
            return;
        
        currentHealth -= damage;
        _sound.PlaySfx(hitSfx);
        punchParticles.Play();
        StartTakeDamageSeq(enemyTransform);
        HealthChanged?.Invoke(CurrentHealth);
    }
}
