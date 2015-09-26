using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]

public class MusicFeedback : MonoBehaviour 
{
    [SerializeField]
    Shape m_shape;

    private List<Shape> m_shapes = new List<Shape>();

    private float m_freqMax;
    private float[] m_freqData;

    private int m_samples = 1024;

    private AudioSource m_audioSource;

    private float[] m_offsets;

    void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

	// Use this for initialization
	void Start ()
    {
        m_freqData = new float[m_samples];
        m_offsets = new float[256];
        m_freqMax = AudioSettings.outputSampleRate / 2.0f;
	}

    public void AddShape(Shape shape)
    {
        m_shapes.Add(shape);
    }

    public void RemoveShape(Shape shape)
    {
        m_shapes.Remove(shape);
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
        
        for (int i = 0; i < 256; ++i)
            m_offsets[i] = -0.2f + ((m_freqData[i + 150] + lowIntensity) * 30f);
        m_offsets[m_offsets.Length - 1] = m_offsets[0];
        
        
        m_shape.SetOffset(m_offsets);

        for(int i = 0; i < m_shapes.Count; ++i)
        {
            m_shapes[i].SetOffset(m_offsets);
        }
	}
}
