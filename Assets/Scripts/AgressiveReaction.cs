using UnityEngine;

public class AgressiveReaction : IBehaviour
{
    private EnemyController _enemy;
    private PlayerController _player;
    private Mover _mover;
    private Animator _animator;

    public AgressiveReaction(EnemyController enemy, PlayerController player)
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
        Vector3 direction =  _player.transform.position - _enemy.transform.position;
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.z);

        _mover.MoveTo(moveDirection.normalized);
        _mover.RotationTo(moveDirection.normalized);
        _animator.SetBool("IsRunning", true);
    }

}
