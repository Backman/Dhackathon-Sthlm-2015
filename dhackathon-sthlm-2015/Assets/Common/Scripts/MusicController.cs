using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MusicController : MonoBehaviour
{
    public static MusicController Instance;

    [SerializeField]
    private float _minValue;
    [SerializeField]
    private float _maxValue;
    [SerializeField]
    private float _fadeSpeed;
    [SerializeField]
    private AudioLowPassFilter _filter;

    private AudioSource _audio;
    private bool _fadingIn;

    public float Transition { get; set; }
	
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Transition = 1f;
        Instance = this;
        _audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //  Transition += _fadeSpeed * Time.deltaTime;
        //  Transition = Mathf.Clamp01(Transition);
        //  _filter.cutoffFrequency = Mathf.Lerp(_minValue, _maxValue, Transition);
    }

    public void StartFadeIn(float duration)
    {
        StartCoroutine(FadeIn(duration));
        _fadingIn = true;
    }

    private IEnumerator FadeIn(float duration)
    {
        if (_fadingIn)
        {
            yield break;
        }
		
        //  var tween = DOTween.To(() => _filter.cutoffFrequency, x =>
        //  {
        //      _filter.cutoffFrequency = x;
        //  }, _minValue, duration);
        //  tween.timeScale = 1f;
        //  tween.Play();

        //  yield return tween.WaitForCompletion();

        var startTime = Time.unscaledTime;
        while (startTime + duration >= Time.unscaledTime)
        {
            var t = (Time.unscaledTime - startTime) / duration;
            Transition = Mathf.Lerp(1f, 0f, t);
            var lowPassValue = Mathf.Lerp(_minValue, _maxValue, Transition);
        	_filter.cutoffFrequency = lowPassValue;
            yield return null;
        }

        startTime = Time.unscaledTime;
        while (startTime + _fadeSpeed >= Time.unscaledTime)
        {
    		var t = (Time.unscaledTime - startTime) / duration;
            Transition = Mathf.Lerp(0f, 1f, t);
            var lowPassValue = Mathf.Lerp(_minValue, _maxValue, Transition);
        	_filter.cutoffFrequency = lowPassValue;
            yield return null;
        }

        _filter.cutoffFrequency = _maxValue;

        _fadingIn = false;
    }
}
