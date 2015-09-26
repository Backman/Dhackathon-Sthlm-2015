using UnityEngine;

public class BackgroundShaders : MonoBehaviour 
{
    public Material m_material;
    private Vector2 m_resolution;
    private MusicFeedback m_feedback;

    void Start()
    {
        m_resolution = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
        m_feedback = GameObject.FindWithTag("MusicFeedback").GetComponent<MusicFeedback>();
    }

    void Update ()
    {
        GetComponent<Renderer>().sharedMaterial.SetFloat("_width", m_resolution.x);
        GetComponent<Renderer>().sharedMaterial.SetFloat("_height", m_resolution.y);
        GetComponent<Renderer>().sharedMaterial.SetFloat("_rythm", m_feedback.m_lowIntensity);
    }
}
