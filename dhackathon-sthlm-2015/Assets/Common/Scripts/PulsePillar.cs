using UnityEngine;
using DG.Tweening;
using System.Collections;

public class PulsePillar : MonoBehaviour
{
    [SerializeField]
    private float _distortIntensity = 0.3f;
	
    [SerializeField]
    private Vector2 _pulseInterval = new Vector2(1f, 8f);
	[SerializeField]
    private float _waitTime = 0.4f;
	
	[SerializeField]
    private float _pulseSpeed = 0.5f;
    [SerializeField]
    private AnimationCurve _pulseCurve;

    [SerializeField]
    private Tween _tween;

    [SerializeField]
    private float _retractionSpeed = 1f;
    [SerializeField]
    private AnimationCurve _retractionCurve;

    [SerializeField]
    private float _pulseForce;
    [SerializeField]
    private float _pulseRadius = 1f;

    private Shape _shape;
    private CircleCollider2D _collider;

    private float[] _offsets;
    private float _time;
    private bool _pulsing;

    private void Awake()
    {
        _shape = GetComponent<Shape>();
        _collider = GetComponent<CircleCollider2D>();
        _offsets = new float[_shape.m_resolution];
        RandomizeTime();
    }

    private void Update()
    {
        if (!_pulsing && _time <= Time.time)
        {
            StartCoroutine(Pulse());
        }
    }

    private IEnumerator Pulse()
    {		
        _pulsing = true;
        var startTime = Time.time;
        var startRadius = _collider.radius;
        var pulseRadius = startRadius + _pulseRadius;
        var currentOffset = 0f;
		
        var offsetTween = DOTween.To(() => currentOffset, x => { 
			currentOffset = x;
			SetOffset(currentOffset, _distortIntensity * Mathf.Clamp01(x));
			}, _pulseRadius, _pulseSpeed);
        var radiusTween = DOTween.To(() => _collider.radius, x =>	_collider.radius = x, pulseRadius, _pulseSpeed);
		
        offsetTween.Pause();
        radiusTween.Pause();

        offsetTween.SetEase(_pulseCurve);
        radiusTween.SetEase(_pulseCurve);

        offsetTween.Restart();
        radiusTween.Restart();

        yield return radiusTween.WaitForCompletion();
        yield return offsetTween.WaitForCompletion();

        ShapeDistorter.Instance.AddDistort(_shape, _distortIntensity, _waitTime);

        yield return new WaitForSeconds(_waitTime);

        offsetTween = DOTween.To(() => currentOffset, x => { 
			currentOffset = x;
			SetOffset(currentOffset, _distortIntensity * Mathf.Clamp01(x));
			}, 0f, _retractionSpeed);
        radiusTween = DOTween.To(() => _collider.radius, x =>	_collider.radius = x, startRadius, _retractionSpeed);
		
		offsetTween.Pause();
        radiusTween.Pause();

        offsetTween.SetEase(_retractionCurve);
        radiusTween.SetEase(_retractionCurve);

        offsetTween.Restart();
        radiusTween.Restart();
		
        yield return radiusTween.WaitForCompletion();
        yield return offsetTween.WaitForCompletion();

        _collider.radius = startRadius;
        SetOffset(0f, 0f);
        RandomizeTime();
        _pulsing = false;
    }

    private void SetOffset(float radius, float intensity)
    {
        for (int i = 0; i < _offsets.Length; ++i)
        {
            _offsets[i] = radius + Random.value * intensity;
        }

        _shape.SetOffset(_offsets);
    }

    private void RandomizeTime()
    {
        _time = Random.Range(_pulseInterval.x, _pulseInterval.y) + Time.time;
    }
}
