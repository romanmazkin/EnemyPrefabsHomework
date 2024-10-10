using UnityEngine;

public class RandomMove : IBehaviour
{
    private EnemyController _enemy;
    private Mover _mover;
    private Animator _animator;

    private Vector3 _randomTarget;

    private float _randomValue = 10f;

    public RandomMove(EnemyController enemy)
    {
        _enemy = enemy;
    }

    public void Awake()
    {
        _mover = _enemy.gameObject.GetComponent<Mover>();
        _animator = _enemy.gameObject.GetComponent<Animator>();
    }

    public void Update()
    {
        Vector3 distance = _randomTarget - _enemy.transform.position;
        Vector3 moveDirection = new Vector3(distance.x, 0, distance.z);

        if (moveDirection.magnitude < 0.1f)
        {
            GenerateDirection();
        }

        _mover.MoveTo(moveDirection.normalized);
        _mover.RotationTo(moveDirection.normalized);
        _animator.SetBool("IsRunning", true);
    }

    private void GenerateDirection()
    {
        float randomXCoordinateValue = Random.Range(-_randomValue, _randomValue);
        float randomZCoordinateValue = Random.Range(-_randomValue, _randomValue);

        _randomTarget = new Vector3(randomXCoordinateValue, 0, randomZCoordinateValue);
    }
}