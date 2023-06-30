using UnityEngine;

[RequireComponent(typeof(IHealth))]
public class ActorUI : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;

    private IHealth _health;

    public void Construct(IHealth health)
    {
        _health = health;
        _health.HealthChanged += OnHealthChanged;
        healthBar.UpdateHealthBar(health.CurrentHealth, health.MaxHealth);
    }

    private void OnDisable()
    {
        _health.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int health)
    {
        healthBar.UpdateHealthBar(health, _health.MaxHealth);
    }
}
