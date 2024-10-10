using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private const float MinDistanceToTarget = 0.05f;

    [SerializeField] private List<Transform> _targets;

    private Mover _mover;

    private IBehaviour _currentBehaviour;

    public List<Transform> GetTargets() => _targets;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _currentBehaviour.Awake();
    }

    private void Update()
    {
        _currentBehaviour.Update();
    }

    public void SetBehaviour(IBehaviour currentBehaviour)
    {
        _currentBehaviour = currentBehaviour;
    }
}