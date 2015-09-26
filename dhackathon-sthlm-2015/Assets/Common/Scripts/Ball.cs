using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [SerializeField]
    private float _baseSpeed;
    [SerializeField]
    private float _maxSpeed = 300f;

    public float PushbackForce = 10f;

    private Rigidbody2D _rb;
    private TrailRenderer _trail;
    private float _currentSpeed;

    private Vector2 _velocity;

    public Vector2 Velocity { get { return _rb.velocity; } }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        //  _trail = GetComponent<TrailRenderer>();
        //  _trail.sortingOrder = -1;

        Random.seed = (int)System.DateTime.Now.Ticks;
        float x = Random.value;
        float y = Random.value;

        _currentSpeed = _baseSpeed;
        _rb.AddForce(new Vector2(x, y) * _currentSpeed, ForceMode2D.Impulse);
        _velocity = _rb.velocity;
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(_rb.velocity.sqrMagnitude - _velocity.sqrMagnitude) > 0.001f)
        {
			_velocity = Vector2.ClampMagnitude(_velocity, _maxSpeed);
            _rb.velocity = _velocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var character = collision.gameObject.GetComponent<Character>();
        if (!character)
        {
            return;
        }

        character.TakeDamage(10);
    }

    public void MultiplySpeed(Vector3 dir, float multiplier)
    {
        Vector3 newVel = dir * _velocity.magnitude * multiplier;
        _velocity = newVel;
    }
}