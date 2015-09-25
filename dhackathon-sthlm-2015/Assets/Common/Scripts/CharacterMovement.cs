using UnityEngine;


public class CharacterMovement : MonoBehaviour
{
    public Player PlayerValue;
	[SerializeField]
    private float _movementSpeed;

    private Vector3 _movement;
    private float _angle;
    private Vector3 _rotation;

    private void Update()
    {
        DoInput();
		
		transform.position += _movement.normalized * _movementSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, _angle));
    }

    private void DoInput()
    {
        switch (PlayerValue)
        {
            case Player.PlayerOne:
                PlayerOneInput();
                break;
            case Player.PlayerTwo:
                PlayerTwoInput();
                break;
        }
    }

    private void PlayerOneInput()
    {
        _movement.x = Input.GetAxisRaw(InputManager.PlayerOneHoriz);
        _movement.y = Input.GetAxisRaw(InputManager.PlayerOneVert);

        Vector2 rot;
        rot.x = Input.GetAxisRaw(InputManager.PlayerOneXRot);
        rot.y = Input.GetAxisRaw(InputManager.PlayerOneYRot);

        if (rot.sqrMagnitude > 0f)
        {
            _angle = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;
            _rotation = rot;
        }
	}

    private void PlayerTwoInput()
    {
        _movement.x = Input.GetAxisRaw(InputManager.PlayerTwoHoriz);
        _movement.y = Input.GetAxisRaw(InputManager.PlayerTwoVert);

        Vector2 rot;
        rot.x = Input.GetAxisRaw(InputManager.PlayerTwoXRot);
        rot.y = Input.GetAxisRaw(InputManager.PlayerTwoYRot);

        if (rot.sqrMagnitude > 0f)
        {
            _angle = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;
            _rotation = rot;
        }
    }
}