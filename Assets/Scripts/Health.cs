public class Health
{
    public float Value { get; private set; }
    private float _valueWhenWounded;

    public Health(float value, float valueWhenWounded)
    {
        Value = value;
        _valueWhenWounded = valueWhenWounded;
    }

    public void Damage(float damage)
    {
        Value -= damage;

        if (Value < 0)
        {
            Value = 0;
        }
    }

    public bool IsNotZero() => Value != 0;
}
