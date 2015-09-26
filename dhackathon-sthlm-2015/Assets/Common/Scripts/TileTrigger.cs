using UnityEngine;
using System.Collections;

public class TileTrigger : MonoBehaviour
{
    private Tile _tile;

    private void Awake()
    {
        _tile = GetComponentInChildren<Tile>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        var character = other.gameObject.GetComponent<Character>();
        if (!character || _tile.IsAlive)
        {
            return;
        }

        character.Kill();
    }
}
