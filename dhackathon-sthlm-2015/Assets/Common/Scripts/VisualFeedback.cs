using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Shape))]
public class VisualFeedback : MonoBehaviour 
{
    [SerializeField]
    private float _intensity = 1f;
    [SerializeField]
    private bool _randomness;
    MusicFeedback m_musicFeedback;
    private string _id;

    void Start () 
    {
        m_musicFeedback = GameObject.FindGameObjectWithTag("MusicFeedback").GetComponent<MusicFeedback>();
        _id = System.Guid.NewGuid().ToString("N");
        if(m_musicFeedback != null)
            m_musicFeedback.AddShape(GetComponent<Shape>(), _id, _intensity, _randomness);
	}

    void OnDestroy()
    {
        if (m_musicFeedback != null)
            m_musicFeedback.RemoveShape(_id);
    }
}
