using UnityEngine;
using System.Collections;

public class LevelCollider : MonoBehaviour
{
    [SerializeField]
    private AudioClip m_bounce;

    private AudioSource m_audioSource;

    void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ball"))
        {
            if (m_audioSource != null && m_bounce != null)
            {
                m_audioSource.clip = m_bounce;
                m_audioSource.Play();
            }
        }

        var character = other.gameObject.GetComponent<Character>();
        if (!character)
        {
            return;
        }

        character.Kill();
    }
}
