using UnityEngine;

public class Rotator
{
    private Transform _transform;

    public Rotator(Transform transform)
    {
        _transform = transform;
    }

    public void ForceRotateTo(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction.normalized);
        _transform.rotation = lookRotation;
    }
}
