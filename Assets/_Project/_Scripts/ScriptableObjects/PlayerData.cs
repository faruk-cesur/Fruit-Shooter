using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/PlayerData", order = 0)]
public class PlayerData : ScriptableObject
{
    [field: SerializeField] public float BonusForwardMovementSpeed { get; set; }
    [field: SerializeField] public float BonusSideMovementSpeed { get; set; }
    [field: SerializeField] public float FruitBulletDamage { get; set; }
}