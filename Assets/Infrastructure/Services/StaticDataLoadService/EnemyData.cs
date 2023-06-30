using UnityEngine;

[CreateAssetMenu(menuName = "EnemyData", fileName = "Data", order = 1)]
public class EnemyData : ScriptableObject
{
    [SerializeField] private EnemyType _type;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _health;
    [SerializeField] private int _reward;

    public GameObject Prefab => _prefab;
    public EnemyType Type => _type;
    public int Health => _health;
    public int Reward => _reward;
}
