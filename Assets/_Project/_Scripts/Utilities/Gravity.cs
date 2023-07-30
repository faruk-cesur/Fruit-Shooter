using UnityEngine;

public class Gravity : MonoBehaviour
{
    public bool ActivateGravity = true;
    public Transform Target;
    public float OffsetPositionY = 0.1f;
    public float GravityForce = 2f;

    private void FixedUpdate()
    {
        SetGravity();
    }

    private void SetGravity()
    {
        if (!ActivateGravity)
            return;

        var position = Target.position;
        position = Vector3.MoveTowards(position, new Vector3(position.x, OffsetPositionY, position.z), GravityForce * Time.fixedDeltaTime);
        Target.position = position;
    }
}