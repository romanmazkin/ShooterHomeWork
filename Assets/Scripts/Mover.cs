using UnityEngine;

public class Mover
{
    private CharacterController _characterController;

    private float _speed;

    public Mover(CharacterController characterController, float speed)
    {
        _characterController = characterController;
        _speed = speed;
    }

    public void MoveTo(Vector3 direction)
    {
        _characterController.Move(direction.normalized * _speed * Time.deltaTime);
    }
}
