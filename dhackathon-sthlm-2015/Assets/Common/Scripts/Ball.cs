using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [SerializeField]
    private float _baseSpeed;
    [SerializeField]
    private float _maxSpeed = 300f;

    [SerializeField]
    public float _characterKnockbackMultiplier = 0.5f;
    [SerializeField]
    private float _shieldKnockbackMultiplier = 0.1f;

    private Rigidbody2D _rb;
    private TrailRenderer _trail;
    private float _currentSpeed;

    private Vector2 _velocity;

    public Vector2 Velocity { get { return _rb.velocity; } }
	public float ShieldKnockbackLength { get { return _rb.velocity.magnitude * _shieldKnockbackMultiplier; } }
	public float CharacterKnockbackLength { get { return _rb.velocity.magnitude * _characterKnockbackMultiplier; } }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        //  _trail = GetComponent<TrailRenderer>();
        //  _trail.sortingOrder = -1;

        //Random.seed = (int)System.DateTime.Now.Ticks;
        //float x = Random.value;
        //float y = Random.value;

        _currentSpeed = _baseSpeed;
		_velocity = Vector3.up * _currentSpeed;
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(_rb.velocity.sqrMagnitude - _velocity.sqrMagnitude) > 0.001f)
        {
			_velocity = Vector2.ClampMagnitude(_velocity, _maxSpeed);
            _rb.velocity = _velocity;
        }
    }

    public void MultiplySpeed(Vector3 dir, float multiplier)
    {
        Vector3 newVel = dir * _velocity.magnitude * multiplier;
        _velocity = newVel;
    }
}