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
        Enemy enemy = Instantiate(_enemyPrefab, spawnPoint.transform.position, Quaternion.identity, null).GetComponent<Enemy>();

        _moveBehaviour = DetermineMoveBehaviour(enemy, spawnPoint.moveBehaviours);
        _reactionBehaviour = DetermineReactionBehaviour(enemy, spawnPoint.reactionBehaviours);

        enemy.Initialize(_moveBehaviour, _reactionBehaviour);
    }

    private IBehaviour DetermineReactionBehaviour(Enemy enemy, EnemyReactionBehaviours reactionBehaviours)
    {
        switch (reactionBehaviours)
        {
            case EnemyReactionBehaviours.Avoid:
                return new AvoidReaction(enemy, _player);
                
            case EnemyReactionBehaviours.Agressive:
                return new AgressiveReaction(enemy, _player);
                
            case EnemyReactionBehaviours.Scared:
                return new ScaredReaction(enemy, _particleSystem);
        }
        return new IdleMove();
    }

    private IBehaviour DetermineMoveBehaviour(Enemy enemy, EnemyMoveBehaviours moveBehaviours)
    {
        switch (moveBehaviours)
        {
            case EnemyMoveBehaviours.Idle:
                return(new IdleMove());

            case EnemyMoveBehaviours.Patrol:
                return(new PatrolMove(enemy, _targetPositions));

            case EnemyMoveBehaviours.Random:
                return(new RandomMove(enemy));
        }
        return new IdleMove();
    }
}
