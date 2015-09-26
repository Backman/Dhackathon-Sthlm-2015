using UnityEngine;

public class LoadScene : MonoBehaviour
{
    private bool m_playerPushedButton = false;
    void Update ()
    {
        if(m_playerPushedButton == false)
        {
            m_playerPushedButton = Input.anyKeyDown;
        }
        else
        {
            Application.LoadLevel(1);
        }
    }
}
