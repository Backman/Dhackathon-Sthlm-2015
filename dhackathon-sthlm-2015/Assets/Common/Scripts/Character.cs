using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    private struct KnockbackState
    {
        public Vector3 Direction;
        public float Length;
        public bool Valid;
    }

    public Player PlayerValue;
    [SerializeField]
    private float _movementSpeed = 3f;
    [SerializeField]
    private KnockbackConfig _knockbackConfig;

    private Rigidbody2D _rb;
    private Vector2 _movement;
    private float _angle;

    private KnockbackState _knockbackState;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        DoInput();
    }

    private void FixedUpdate()
    {
        if(!_knockbackState.Valid)
        {
            _rb.velocity = _movement * _movementSpeed;
        }

        _rb.rotation = _angle;
    }

    private void DoInput()
    {
        string horiz = InputManager.GetInputName(PlayerValue, InputManager.InputType.Horizontal);
        string vert = InputManager.GetInputName(PlayerValue, InputManager.InputType.Vertical);
        string xRot = InputManager.GetInputName(PlayerValue, InputManager.InputType.XRotation);
        string yRot = InputManager.GetInputName(PlayerValue, InputManager.InputType.YRotation);

        _movement.x = Input.GetAxisRaw(horiz);
        _movement.y = Input.GetAxisRaw(vert);

        Vector2 rot;
        rot.x = Input.GetAxis(xRot);
        rot.y = Input.GetAxisRaw(yRot);
        if (rot.sqrMagnitude > 0f)
        {
            _angle = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;
            _angle -= 90f;
        }
    }

    public void Knockback(Vector2 dir, float length)
    {
        if (_knockbackState.Valid)
        {
            return;
        }

        _knockbackState.Direction = dir;
        _knockbackState.Length = length;
        _knockbackState.Valid = true;
		
		StartCoroutine(Knockback());
    }

    private IEnumerator Knockback()
    {
        AnimationCurve curve = _knockbackConfig.Curve;
        float length = Mathf.Min(_knockbackState.Length, _knockbackConfig.MaxLength);
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position + _knockbackState.Direction * length;

        float totalDistance = Vector3.Distance(startPos, endPos);
        float currentDistance = totalDistance * 0.98f;

        while (currentDistance > 0.001f)
        {
            float t = 1.0f - (currentDistance / totalDistance);
            transform.position = Vector3.Lerp(startPos, endPos, curve.Evaluate(t));
            currentDistance = Vector3.Distance(transform.position, endPos);
            yield return null;
        }

        _knockbackState.Valid = false;
    }
}