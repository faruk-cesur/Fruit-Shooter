using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/PlayerData", order = 0)]
public class PlayerData : ScriptableObject
{
    [field: SerializeField] public float ForwardMovementSpeedBonus { get; set; }
    [field: SerializeField] public float SideMovementSpeedBonus { get; set; }
    [field: SerializeField] public float BulletDamage { get; set; }
    [field: SerializeField] public float BulletReloadDuration { get; set; }
    [field: SerializeField] public int CollectedFruitBonus { get; set; }
}