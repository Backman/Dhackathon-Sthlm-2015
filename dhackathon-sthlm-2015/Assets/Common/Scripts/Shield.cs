using UnityEngine;

public class Shield : MonoBehaviour
{
    private struct BashState
    {
        public float StartTime;
        public float SpeedMultiplier;
        public bool Valid;
    }

    [SerializeField]
    private Character _character;

    private BashState _bashState;

    private float _bashTime;

    private void Awake()
    {
        _character = GetComponentInParent<Character>();
    }

    private void Update()
    {
        if (_bashState.Valid && _bashState.StartTime < Time.unscaledTime)
        {
            _bashState.Valid = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Ball ball = other.gameObject.GetComponent<Ball>();
        if (ball == null)
        {
            return;
        }

        if (!_bashState.Valid)
        {
            Vector2 dir = _character.transform.position - ball.transform.position;
            _character.Knockback(dir.normalized, ball.Velocity.magnitude * 0.1f);
        }
        else
        {
			ball.MultiplySpeed(transform.up, _bashState.SpeedMultiplier);
            FreezeManager.Instance.Freeze(true);
            _bashState.Valid = false;
        }
    }

    public void Bash(float bashTime, float speedMultiplier)
    {
        if (_bashState.Valid)
        {
            return;
		}

        _bashState.StartTime = bashTime + Time.unscaledTime;
        _bashState.SpeedMultiplier = speedMultiplier;
        _bashState.Valid = true;
	}
}