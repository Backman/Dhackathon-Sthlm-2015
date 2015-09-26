using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]

public class MusicFeedback : MonoBehaviour 
{
    private struct ActiveShape
    {
        public Shape Shape;
        public float[] Offsets;
        public float Intensity;
        public bool Randomness;
        public string ID;
    }

    [SerializeField]
    Shape m_shape;

    public float m_lowIntensity { get; private set; }

    private Dictionary<string, ActiveShape> _shapes = new Dictionary<string, ActiveShape>();

    private float m_freqMax;
    private float[] m_freqData;

    private int m_samples = 1024;

    private AudioSource m_audioSource;

    void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

	// Use this for initialization
	void Start ()
    {
        m_freqData = new float[m_samples];
        m_freqMax = AudioSettings.outputSampleRate / 2.0f;
	}

    public void AddShape(Shape shape, string id, float intensity, bool randomness)
    {
        var activeShape = new ActiveShape()
        {
            Shape = shape,
            Offsets = new float[shape.m_resolution],
            Intensity = intensity,
			Randomness = randomness,
        	ID = id
    	};
        _shapes.Add(id, activeShape);
    }

    public void RemoveShape(string id)
    {
        _shapes.Remove(id);
    }

    private float IntensityOfRange(float freqLow, float freqHigh)
    {
        freqLow = Mathf.Clamp(freqLow, 20, m_freqMax);		// Limit low...
        freqHigh = Mathf.Clamp(freqHigh, freqLow, m_freqMax);	// and high frequencies

        //m_audioSource.GetSpectrumData(m_freqData, 0, FFTWindow.BlackmanHarris);
        
        int n1 = (int)Mathf.Floor((freqLow * m_samples) / m_freqMax);
        int n2 = (int)Mathf.Floor((freqHigh * m_samples) / m_freqMax);

        float sum = 0;
        // Average the volumes of frequencies f_low to f_high
        for (int i = n1; i < n2; ++i)
        {
            sum += m_freqData[i];
        }

        return sum;
    }
	
    private float GetFreqData(int index)
    {
        if (index < 0)
            return m_freqData[m_freqData.Length + index];
        else if (index >= m_freqData.Length)
            return m_freqData[index - m_freqData.Length];
        else
            return m_freqData[index];
    }

	// Update is called once per frame
	void Update ()
    {
        m_audioSource.GetSpectrumData(m_freqData, 0, FFTWindow.BlackmanHarris);
        float lowIntensity = IntensityOfRange(0f, m_freqMax * 0.1f) * 0.005f;

        // Storing the low intensity for use outside this script.
        m_lowIntensity = lowIntensity;

        foreach (var shape in _shapes.Values)
        {
            var offsets = shape.Offsets;
            for (int i = 0; i < offsets.Length; ++i)
			{
                float rand = shape.Randomness ? Random.Range(0.3f, 1.7f) : 1f;
                offsets[i] = -0.2f + ((m_freqData[i + 150] * rand + lowIntensity * shape.Intensity) * 30f);
			}
       		offsets[offsets.Length - 1] = offsets[0];
            shape.Shape.SetOffset(offsets);
        }
	}
}
