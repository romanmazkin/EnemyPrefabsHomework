using UnityEngine;

public class AgressiveReaction : IBehaviour
{
    private Enemy _enemy;
    private Transform _player;
    private Mover _mover;

    public AgressiveReaction(Enemy enemy, Transform player)
    {
        _enemy = enemy;
        _player = player;
        _mover = _enemy.gameObject.GetComponent<Mover>();
    }

    public void Update()
    {
        Vector3 direction =  _player.transform.position - _enemy.transform.position;
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.z);

        _mover.MoveTo(moveDirection.normalized);
        _mover.RotationTo(moveDirection.normalized);
    }

}
