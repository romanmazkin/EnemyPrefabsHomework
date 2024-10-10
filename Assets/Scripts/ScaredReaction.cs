using UnityEngine;

public class ScaredReaction : IBehaviour
{
    private EnemyController _enemy;
    private ParticleSystem _particleSystem;

    public ScaredReaction(EnemyController enemy, ParticleSystem particleSystem)
    {
        _enemy = enemy;
        _particleSystem = particleSystem;
    }

    public void Awake()
    {
        return;
    }

    public void Update()
    {
        _particleSystem.transform.position = _enemy.transform.position;

        _particleSystem.Play();
        _enemy.gameObject.SetActive(false);
    }
}
