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
    private CharacterConfig _config;

    [SerializeField]
    private AudioClip _death;

    private AudioSource _audioSource;

    private int _health;

    private Rigidbody2D _rb;
    private Vector2 _movement;
    private float _angle;

    private KnockbackState _knockbackState;
    private KnockbackConfig _knockbackConfig;

    private Shape _shape;
    private bool _isAlive = true;
	
	public bool IsAlive { get { return _isAlive; } }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _shape = GetComponent<Shape>();
        _audioSource = GetComponent<AudioSource>();
        _health = _config.Health;
        _knockbackConfig = _config.KnockbackConfig;
    }

    private void Update()
    {
        if (!_isAlive)
        {
            return;
        }
		
        DoInput();
    }

    private void FixedUpdate()
    {
        if (!_isAlive)
        {
            return;
        }
		
        if(!_knockbackState.Valid)
        {
            _rb.velocity = _movement * _config.MovementSpeed;
        }

        _rb.rotation = _angle;

        Vector3 pos = _rb.position;
        _rb.position = new Vector3(Mathf.Clamp(pos.x, -82, 82), Mathf.Clamp(pos.y, -42, 42), 0);
    }

    public void TakeDamage(int amount)
    {
        _health -= amount;
        if (_isAlive && _health <= 0)
        {
            //  Kill();
        }
    }

    public void Kill()
    {
        if (!_isAlive) return;

        var duration = 1f;
        var intensity = 0.3f;
        ShapeDistorter.Instance.AddDistort(_shape, intensity, duration);
		StartCoroutine(FadeColor(duration));
		_isAlive = false;
        _rb.velocity = Vector2.zero;

        if (_config.DeathParticle)
        {
            Instantiate(_config.DeathParticle, transform.position, transform.rotation);
        }

        //Destroy(gameObject, duration);
        if(PlayerValue == Player.PlayerTwo)
            GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().AddScoreOne();
        else if (PlayerValue == Player.PlayerOne)
            GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().AddScoreTwo();
        
        StartCoroutine(RestartGame(duration));

        if (_audioSource != null && _death != null)
        {
            _audioSource.clip = _death;
            _audioSource.Play();
        }
    }

    private IEnumerator RestartGame(float duration)
    {
        yield return new WaitForSeconds(duration);
        Application.LoadLevel(0);
    }

    private IEnumerator FadeColor(float duration)
    {
        var startTime = Time.unscaledTime;
        var a = _shape.m_color.a;

        while (startTime + duration >= Time.unscaledTime)
        {
            var percent = 1f - ((Time.unscaledTime - startTime) / duration);
            _shape.m_color.a = a * percent;
            yield return null;
        }

        _shape.m_color.a = 0f;
    }

    private void DoInput()
    {
        string horiz = InputManager.GetInputName(PlayerValue, InputManager.InputType.Horizontal);
        string vert = InputManager.GetInputName(PlayerValue, InputManager.InputType.Vertical);
        string xRot = InputManager.GetInputName(PlayerValue, InputManager.InputType.XRotation);
        string yRot = InputManager.GetInputName(PlayerValue, InputManager.InputType.YRotation);

        _movement.x = Input.GetAxisRaw(horiz);
        _movement.y = Input.GetAxisRaw(vert);

        _movement.Normalize();

        Vector2 rot;
        rot.x = Input.GetAxisRaw(xRot);
        rot.y = Input.GetAxisRaw(yRot);
        if (rot.sqrMagnitude > 0.2f)
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
        ShapeDistorter.Instance.AddDistort(_shape, 0.15f, 0.3f);

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