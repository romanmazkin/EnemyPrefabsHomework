using System.Collections.Generic;
using UnityEngine;

public class PatrolMove : IBehaviour
{
    private const float MinDistanceToTarget = 0.05f;

    private List<Transform> _targets;

    private EnemyController _enemy;
    private Mover _mover;
    private Animator _animator;

    private Queue<Vector3> _targetPositions;
    private Vector3 _currentTarget;

    public PatrolMove(EnemyController enemy)
    {
        _enemy = enemy;
    }

    public void Awake()
    {
        _mover = _enemy.gameObject.GetComponent<Mover>();
        _animator = _enemy.gameObject.GetComponent<Animator>();
        _targets = _enemy.GetTargets();
        _targetPositions = new Queue<Vector3>();

        foreach (Transform target in _targets)
        {
            _targetPositions.Enqueue(target.position);
        }
        _currentTarget = _targetPositions.Dequeue();
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
        _animator.SetBool("IsRunning", true);
    }

    private void SwitchTarget()
    {
        _targetPositions.Enqueue(_currentTarget);
        _currentTarget = _targetPositions.Dequeue();
    }
}
