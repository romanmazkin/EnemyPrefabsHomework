using System.Collections.Generic;
using UnityEngine;

public class PatrolMove : IBehaviour
{
    private const float MinDistanceToTarget = 0.1f;

    private Enemy _enemy;
    private Mover _mover;

    private Queue<Vector3> _targetPositions;
    private Vector3 _currentTarget;

    public PatrolMove(Enemy enemy, Queue<Vector3> targetPositions)
    {
        _enemy = enemy;
        _targetPositions = targetPositions;
        _mover = _enemy.gameObject.GetComponent<Mover>();
    }

    public void Update()
    {
        Vector3 direction = _currentTarget - _enemy.transform.position;
        Vector3 normalizedDirection = direction.normalized;

        if (direction.magnitude <= MinDistanceToTarget)
        {
            SwitchTarget();
        }
        
        _mover.MoveTo(normalizedDirection);
        _mover.RotationTo(normalizedDirection);
    }

    private void SwitchTarget()
    {
        _targetPositions.Enqueue(_currentTarget);
        _currentTarget = _targetPositions.Dequeue();
    }
}
