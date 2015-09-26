using UnityEngine;
using System.Collections;

public class LevelCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var character = other.gameObject.GetComponent<Character>();
        if (!character)
        {
            return;
        }

        character.Kill();
    }
}
