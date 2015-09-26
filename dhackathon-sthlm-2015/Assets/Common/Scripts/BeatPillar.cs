using UnityEngine;
using System.Collections;

public class BeatPillar : MonoBehaviour
{
	[SerializeField]
    private AudioSource _audioSource;
	
    [SerializeField]
    private Vector2 _radius = new Vector2(1f, 3f);
    [SerializeField]
    private float _beatSpeed = 1f;
    [SerializeField]
    private float _retractionSpeed = 1f;

    private CircleCollider2D _collider;
    private Shape _shape;
    private float[] _offsets;
	
    private float _freqMax;
    private float[] _freqData;

    private int _samples = 1024;


    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
        _shape = GetComponent<Shape>();
        _offsets = new float[_shape.m_resolution];
        _freqData = new float[_samples];
        _freqMax = AudioSettings.outputSampleRate / 2.0f;
    }

    private void Update()
    {
        _collider.radius -= Time.deltaTime * _retractionSpeed;
		
        _audioSource.GetSpectrumData(_freqData, 0, FFTWindow.BlackmanHarris);
        float lowIntensity = IntensityOfRange(0f, _freqMax * 0.1f) * 0.5f;

        var totalFreq = 0f;
        for (int i = 0; i < _freqData.Length; ++i)
        {
            totalFreq += _freqData[i];
        }

        var freq = _freqData[Random.Range(0, _freqData.Length)];

        var radius = (((totalFreq / _freqData.Length) + lowIntensity) * 30f);

        _collider.radius = Mathf.Lerp(_collider.radius, radius, _beatSpeed * Time.deltaTime);

        _collider.radius = Mathf.Clamp(_collider.radius, _radius.x, _radius.y);
    }
	
	private float IntensityOfRange(float freqLow, float freqHigh)
    {
        freqLow = Mathf.Clamp(freqLow, 20, _freqMax);		// Limit low...
        freqHigh = Mathf.Clamp(freqHigh, freqLow, _freqMax);	// and high frequencies

        //m_audioSource.GetSpectrumData(m_freqData, 0, FFTWindow.BlackmanHarris);
        
        int n1 = (int)Mathf.Floor((freqLow * _samples) / _freqMax);
        int n2 = (int)Mathf.Floor((freqHigh * _samples) / _freqMax);

        float sum = 0;
        // Average the volumes of frequencies f_low to f_high
        for (int i = n1; i < n2; ++i)
        {
            sum += _freqData[i];
        }

        return sum;
    }
	
    private float GetFreqData(int index)
    {
        if (index < 0)
            return _freqData[_freqData.Length + index];
        else if (index >= _freqData.Length)
            return _freqData[index - _freqData.Length];
        else
            return _freqData[index];
    }
	
	
}
