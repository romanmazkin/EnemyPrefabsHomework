using UnityEngine;

public class Mover : MonoBehaviour
{
    private float _speed = 5;
    private float _rotationSpeed = 500;
    private float _deadZone = 0.1f;

    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponentInParent<CharacterController>();
    }

    public void MoveTo(Vector3 direction)
    {
        if (direction.magnitude > _deadZone)
        {
            _characterController.Move(direction.normalized * _speed * Time.deltaTime);
        }
    }

    public void RotationTo(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction.normalized);

        float step = _rotationSpeed * Time.deltaTime;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, step);
    }
}
