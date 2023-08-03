using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private EnemyHolder _enemyHolder;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.TryGetComponent<Collectable>(out Collectable collectable))
        {
            collectable.Collect(_inventory.CollectableTargetPosition);
        }

        if (other.TryGetComponent<FruitGate>(out FruitGate fruitGate))
        {
            if (IsGateTriggered(fruitGate))
                return;

            TriggerGates(fruitGate);
            fruitGate.TriggerFruitGate(fruitGate.GateFruitAmount);
            Inventory.OnChangeFruitAmount?.Invoke();
            Destroy(other.gameObject);
        }
        
        if (other.transform.parent.TryGetComponent<EnemyBase>(out EnemyBase enemyBase))
        {
            if (enemyBase.IsEnemyExplode)
                return;
            
            Inventory.OnRemoveFruit?.Invoke(enemyBase.EnemyDamageAmount);
            Inventory.OnChangeFruitAmount?.Invoke();
            _enemyHolder.RemoveEnemyFromList(enemyBase);
            enemyBase.ExplodeOnTrigger();
        }

        if (other.CompareTag("ShootingStance"))
        {
            _cameraController.EnableShootingVirtualCamera();
            _enemyHolder.EnableAllEnemies();
            _playerController.PlayerState = PlayerController.PlayerStates.DriveAndShootEnemies;
            _playerController.SetSideMoveLimits(0,0,0.5f);
            _playerController.ResetSideMovementRootPosition();
            _inventory.SetFruitBackgroundShootingStance();
        }
    }

    private bool IsGateTriggered(FruitGate fruitGate)
    {
        return fruitGate.IsGateTriggered || fruitGate.OtherFruitGate.IsGateTriggered;
    }

    private void TriggerGates(FruitGate fruitGate)
    {
        fruitGate.IsGateTriggered = true;
        fruitGate.OtherFruitGate.IsGateTriggered = true;
    }
}