using UnityEngine;

[CreateAssetMenu(menuName = "Boosters", fileName = "Boost", order = 5)]
public class BoosterData : ScriptableObject
{
    [SerializeField] private BoostType type;
    [SerializeField] private Sprite sprite;
    [SerializeField] private float multiplier;
    [SerializeField] private float duration;

    public Sprite Sprite => sprite;
    public BoostType Type => type;
    public float Multiplier => multiplier;
    public float Duration => duration;
}
