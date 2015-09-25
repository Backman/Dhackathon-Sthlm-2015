using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float _baseSpeed;
	
    private Rigidbody2D _rb;
    private TrailRenderer _trail;
    private float _currentSpeed;
    
	public Vector2 Velocity { get { return _rb.velocity; } }
	
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _trail = GetComponent<TrailRenderer>();
        _trail.sortingOrder = -1;

        Random.seed = (int)System.DateTime.Now.Ticks;
        float x = Random.value;
        float y = Random.value;

        _currentSpeed = _baseSpeed;
        _rb.AddForce(new Vector2(x, y) * _currentSpeed, ForceMode2D.Impulse);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddSpeed(1f);
        }

        Debug.Log(Velocity.magnitude);
    }

    private void FixedUpdate()
    {
    }

    public void AddSpeed(float percent)
    {
        Vector2 dir = _rb.velocity.normalized;
        _rb.velocity = Vector2.zero;
        _currentSpeed = _currentSpeed + _baseSpeed * percent;
        _rb.AddForce(dir * _currentSpeed, ForceMode2D.Impulse);
    }
}