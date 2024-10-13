using UnityEngine;

public class Enemy : MonoBehaviour
{
    private IBehaviour _moveBehaviour;
    private IBehaviour _reactionBehaviour;
    private IBehaviour _currentBehaviour;

    private void Update()
    {
        _currentBehaviour.Update();
    }

    public void Initialize(IBehaviour moveBehaviour, IBehaviour reactionBehaviour)
    {
        _moveBehaviour = moveBehaviour;
        _reactionBehaviour = reactionBehaviour;

        _currentBehaviour = _moveBehaviour;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            _currentBehaviour = _reactionBehaviour;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            _currentBehaviour = _moveBehaviour;
        }
    }
}