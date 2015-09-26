using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour
{
    public static MusicController Instance;

    [SerializeField]
    private float _minValue;
    [SerializeField]
    private float _maxValue;
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

        Instance = this;
        _audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //  Transition = Mathf.Clamp01(Transition);
    }

    public void StartFadeIn(float duration)
    {
        DG.Tweening.DOTween.To(() => _filter.cutoffFrequency, x =>
        {
            _filter.cutoffFrequency = x;
        }, _minValue, duration);
        //  StartCoroutine(FadeIn(duration));
        //  _fadingIn = true;
    }

    private IEnumerator FadeIn(float duration)
    {
        //  if (_fadingIn)
        //  {
        //      yield break;
        //  }

        var startTime = Time.unscaledTime;
        var startTransition = Transition;

        if (startTime + duration > Time.unscaledTime)
        {
            var t = (Time.unscaledTime - startTime) / duration;
            Transition = Mathf.Lerp(0f, 1f, t);
            var lowPassValue = Mathf.Lerp(_maxValue, _minValue, Transition);
			_filter.cutoffFrequency = lowPassValue;
            yield return null;
        }

        _fadingIn = false;
    }
}
