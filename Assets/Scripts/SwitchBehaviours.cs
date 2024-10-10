using UnityEngine;

public class SwitchBehaviours : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    private EnemyController _enemy;

    [SerializeField] private ParticleSystem _particleSystem;

    private EnemyMoveBehaviours _moveBehaviours;
    private EnemyReactionBehaviours _reactionBehaviours;

    public void Initialize(EnemyMoveBehaviours moveBehaviours, EnemyReactionBehaviours reactionBehaviours)
    {
        _moveBehaviours = moveBehaviours;
        _reactionBehaviours = reactionBehaviours;

        _enemy = GetComponent<EnemyController>();

        DetermineMoveBehaviour();
    }

    private void OnTriggerEnter(Collider other)
    {
        DetermineReactionBehaviour();
    }

    private void OnTriggerExit(Collider other)
    {
        DetermineMoveBehaviour();
    }

    private void DetermineMoveBehaviour()
    {
        switch (_moveBehaviours)
        {
            case EnemyMoveBehaviours.Idle:
                _enemy.SetBehaviour(new IdleMove());
                break;
            case EnemyMoveBehaviours.Patrol:
                _enemy.SetBehaviour(new PatrolMove(_enemy));
                break;
            case EnemyMoveBehaviours.Random:
                _enemy.SetBehaviour(new RandomMove(_enemy));
                break;
        }
    }

    private void DetermineReactionBehaviour()
    {
        switch (_reactionBehaviours)
        {
            case EnemyReactionBehaviours.Avoid:
                _enemy.SetBehaviour(new AvoidReaction(_enemy, _player));
                break;
            case EnemyReactionBehaviours.Agressive:
                _enemy.SetBehaviour(new AgressiveReaction(_enemy, _player));
                break;
            case EnemyReactionBehaviours.Scared:
                _enemy.SetBehaviour(new ScaredReaction(_enemy, _particleSystem));
                break;
        }
    }
}
