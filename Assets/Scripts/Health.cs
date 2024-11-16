using UnityEngine;

public class Health : MonoBehaviour
{
    public float Value { get; set; }

    public void Damage(float damage)
    {
        Value -= damage;

        if (Value < 0)
        {
            Value = 0;
        }
    }
}
