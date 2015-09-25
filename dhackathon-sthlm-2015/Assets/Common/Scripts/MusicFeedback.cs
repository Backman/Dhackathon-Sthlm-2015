using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class MusicFeedback : MonoBehaviour 
{
    [SerializeField]
    private Transform test0;
    [SerializeField]
    private Transform test1;
    [SerializeField]
    private Transform test2;
    [SerializeField]
    private Transform test3;

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

    private float IntensityOfRange(float freqLow, float freqHigh)
    {
        freqLow = Mathf.Clamp(freqLow, 20, m_freqMax);		// Limit low...
        freqHigh = Mathf.Clamp(freqHigh, freqLow, m_freqMax);	// and high frequencies

        m_audioSource.GetSpectrumData(m_freqData, 0, FFTWindow.BlackmanHarris);
        
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
	
	// Update is called once per frame
	void Update ()
    {
        test0.localScale = new Vector3(IntensityOfRange(0f, (m_freqMax / 4f) * 1) * 50, 5, 5);
        test1.localScale = new Vector3(IntensityOfRange((m_freqMax / 4f) * 1, (m_freqMax / 4f) * 2) * 50, 5, 5);
        test2.localScale = new Vector3(IntensityOfRange((m_freqMax / 4f) * 2, (m_freqMax / 4f) * 3) * 50, 5, 5);
        test3.localScale = new Vector3(IntensityOfRange((m_freqMax / 4f) * 3, m_freqMax) * 50, 5, 5);
	}
}
