using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 _startPosition = new Vector3(0, 0, 0);

    private const string HorizontalAxisName = "Horizontal";
    private const string VerticalAxisName = "Vertical";

    private Mover _mover;

    private void Awake()
    {
        _mover = gameObject.GetComponent<Mover>();
    }

    private void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw(HorizontalAxisName), 0, Input.GetAxisRaw(VerticalAxisName));

        if (input.x != _startPosition.x || input.z != _startPosition.z)
        {
            _mover.MoveTo(input);
            _mover.RotationTo(input);
        }
    }
}
