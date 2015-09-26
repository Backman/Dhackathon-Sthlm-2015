using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class ScreenShaker : MonoBehaviour
{
    public static ScreenShaker Instance;

    [SerializeField]
    private ShakeConfig _config;

    private bool _isShaking;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void ScreenShake(float duration)
    {
        StartCoroutine(Shake(duration));
        _isShaking = true;
    }

    private IEnumerator Shake(float duration)
    {
        AnimationCurve x = _config.X;
        AnimationCurve y = _config.Y;
        AnimationCurve strength = _config.Strength;
        //  float duration = _config.Duration;
        float updateRate = _config.UpdateRate;

        float startTime = Time.unscaledTime;
        Vector3 startPos = transform.position;

        float time = 0f;

        while (startTime + duration > Time.unscaledTime)
        {
            time += Time.unscaledDeltaTime;
            if (time > updateRate)
            {
                time -= updateRate;
                float t = (Time.unscaledTime - startTime) / duration;
	
				Vector2 random = Random.insideUnitCircle;
				float xShake = random.x * strength.Evaluate(t);
				float yShake = random.y * strength.Evaluate(t);
	
				Vector3 pos = new Vector3(xShake, yShake, 0f);
				transform.position = startPos + pos;
            }
            yield return null;
        }

        transform.position = startPos;

        _isShaking = false;
    }
}