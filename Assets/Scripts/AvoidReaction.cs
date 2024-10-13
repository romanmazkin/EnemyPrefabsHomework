using UnityEngine;

public class AvoidReaction : IBehaviour
{
    private Enemy _enemy;
    private Transform _player;
    private Mover _mover;

    public AvoidReaction(Enemy enemy, Transform player)
    {
        _enemy = enemy;
        _player = player;
        _mover = enemy.gameObject.GetComponent<Mover>();
    }

    public void Update()
    {
        Vector3 direction = _enemy.transform.position - _player.transform.position;
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.z);

        _mover.MoveTo(moveDirection.normalized);
        _mover.RotationTo(moveDirection.normalized);
    }
}
