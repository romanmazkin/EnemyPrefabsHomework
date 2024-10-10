using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyController _enemy;
    private EnemyMoveBehaviours _enemyMove;
    private EnemyReactionBehaviours _enemyReaction;
    private SwitchBehaviours _switchBehaviour;

    private void Awake()
    {
        _switchBehaviour = _enemy.gameObject.GetComponent<SwitchBehaviours>();
    }

    private void Start()
    {
        SpawnTo(_enemy);
        _switchBehaviour.Initialize(_enemyMove, _enemyReaction);
    }

    public void SpawnTo(EnemyController enemy)
    {
        Instantiate(enemy, transform.position, Quaternion.identity, null);
    }
}
