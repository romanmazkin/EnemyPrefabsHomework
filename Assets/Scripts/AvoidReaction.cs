using UnityEngine;

public class AvoidReaction : IBehaviour
{
    private EnemyController _enemy;
    private PlayerController _player;
    private Mover _mover;
    private Animator _animator;

    public AvoidReaction(EnemyController enemy, PlayerController player)
    {
        _enemy = enemy;
        _player = player;
    }

    public void Awake()
    {
        _mover = _enemy.gameObject.GetComponent<Mover>();
        _animator = _enemy.gameObject.GetComponent<Animator>();
    }

    public void Update()
    {
        Vector3 direction = _enemy.transform.position - _player.transform.position;
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.z);

        _mover.MoveTo(moveDirection.normalized);
        _mover.RotationTo(moveDirection.normalized);
        _animator.SetBool("IsRunning", true);
    }
}
