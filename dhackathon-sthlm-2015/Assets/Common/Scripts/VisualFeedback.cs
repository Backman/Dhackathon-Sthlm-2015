using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Shape))]
public class VisualFeedback : MonoBehaviour 
{
    MusicFeedback m_musicFeedback;

	void Start () 
    {
        m_musicFeedback = GameObject.FindGameObjectWithTag("MusicFeedback").GetComponent<MusicFeedback>();
        if(m_musicFeedback != null)
            m_musicFeedback.AddShape(GetComponent<Shape>());
	}

    void OnDestroy()
    {
        if (m_musicFeedback != null)
            m_musicFeedback.RemoveShape(GetComponent<Shape>());
    }
}
