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

    public void ScreenShake()
    {
        StartCoroutine(Shake());
        _isShaking = true;
    }

    private IEnumerator Shake()
    {
        AnimationCurve x = _config.X;
        AnimationCurve y = _config.Y;
        float duration = _config.Duration;
        float strength = _config.Strength;

        float startTime = Time.unscaledTime;
        Vector3 startPos = transform.position;

        while (startTime + duration > Time.unscaledTime)
        {
            float t = (Time.unscaledTime - startTime) / duration;

            float xShake = x.Evaluate(t) * strength;
            float yShake = y.Evaluate(t) * strength;

            Vector3 pos = new Vector3(xShake, yShake, 0f);
            transform.position = startPos + pos;

            yield return null;
        }

        transform.position = startPos;

        _isShaking = false;
    }
}