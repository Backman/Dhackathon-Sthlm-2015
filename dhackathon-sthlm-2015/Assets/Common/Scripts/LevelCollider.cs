using UnityEngine;
using System.Collections;

public class LevelCollider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        var character = other.gameObject.GetComponent<Character>();
        if (!character)
        {
            return;
        }

        character.Kill();
    }
}
