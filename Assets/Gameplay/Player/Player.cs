using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerAttack playerAttack;
    public PlayerAttack PlayerAttack => playerAttack;
}
