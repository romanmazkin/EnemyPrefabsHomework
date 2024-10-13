using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform _player;
    [SerializeField] private ParticleSystem _particleSystem;

    [SerializeField] private List<SpawnPoint> _spawnPoints;
    [SerializeField] private List<Transform> _targets;

    private Queue<Vector3> _targetPositions;

    private IBehaviour _moveBehaviour;
    private IBehaviour _reactionBehaviour;

    private void Awake()
    {
        _targetPositions = new Queue<Vector3>();

        foreach (Transform target in _targets)
        {
            _targetPositions.Enqueue(target.position);
        }

        foreach (SpawnPoint spawnPoint in _spawnPoints)
        {
            SpawnEnemy(spawnPoint);
        }
    }

    public void SpawnEnemy(SpawnPoint spawnPoint)
    {
        Enemy enemy = Instantiate(_enemyPrefab, spawnPoint.transform.position, Quaternion.identity, null);

        DetermineMoveBehaviour(enemy, spawnPoint.moveBehaviours);
        DetermineReactionBehaviour(enemy, spawnPoint.reactionBehaviours);

        enemy.Initialize(_moveBehaviour, _reactionBehaviour);
    }

    private void DetermineReactionBehaviour(Enemy enemy, EnemyReactionBehaviours reactionBehaviours)
    {
        switch (reactionBehaviours)
        {
            case EnemyReactionBehaviours.Avoid:
                enemy.SetBehaviour(new AvoidReaction(enemy, _player));
                break;
            case EnemyReactionBehaviours.Agressive:
                enemy.SetBehaviour(new AgressiveReaction(enemy, _player));
                break;
            case EnemyReactionBehaviours.Scared:
                enemy.SetBehaviour(new ScaredReaction(enemy, _particleSystem));
                break;
        }
    }

    private void DetermineMoveBehaviour(Enemy enemy, EnemyMoveBehaviours moveBehaviours)
    {
        switch (moveBehaviours)
        {
            case EnemyMoveBehaviours.Idle:
                enemy.SetBehaviour(new IdleMove());
                break;
            case EnemyMoveBehaviours.Patrol:
                enemy.SetBehaviour(new PatrolMove(enemy, _targetPositions));
                break;
            case EnemyMoveBehaviours.Random:
                enemy.SetBehaviour(new RandomMove(enemy));
                break;
        }
    }
}
