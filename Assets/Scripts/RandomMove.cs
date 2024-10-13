using UnityEngine;

public class RandomMove : IBehaviour
{
    private const float MinDistanceToTarget = 0.1f;

    private Enemy _enemy;
    private Mover _mover;

    private Vector3 _randomTarget;

    private float _randomValue = 10f;

    public RandomMove(Enemy enemy)
    {
        _enemy = enemy;
        _mover = enemy.gameObject.GetComponent<Mover>();
    }

    public void Update()
    {
        Vector3 distance = _randomTarget - _enemy.transform.position;
        Vector3 moveDirection = new Vector3(distance.x, 0, distance.z);

        if (moveDirection.magnitude < MinDistanceToTarget)
        {
            GenerateDirection();
        }

        _mover.MoveTo(moveDirection.normalized);
        _mover.RotationTo(moveDirection.normalized);
    }

    private void GenerateDirection()
    {
        float randomXCoordinateValue = Random.Range(-_randomValue, _randomValue);
        float randomZCoordinateValue = Random.Range(-_randomValue, _randomValue);

        _randomTarget = new Vector3(randomXCoordinateValue, 0, randomZCoordinateValue);
    }
}