using UnityEngine;
using System.Collections;

public class LinePreview : MonoBehaviour
{
    Vector3[] unitLine = new Vector3[2];
    Vector3[] custom = new Vector3[2];

    private void Start() 
    {
        unitLine[0] = new Vector3(-0.5f, 0, 0);
        unitLine[1] = new Vector3(0.5f, 0, 0);
    }

    private void Update()
    {
        var scale = Matrix4x4.Scale(transform.localScale);
        var rotation = transform.rotation;
        var position = transform.position;

        for (int i = 0; i < unitLine.Length; ++i)
        {
            custom[i] = scale * unitLine[i];
            custom[i] = rotation * custom[i];
            custom[i] += position;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine (custom[0], custom[1]);
    }
}
