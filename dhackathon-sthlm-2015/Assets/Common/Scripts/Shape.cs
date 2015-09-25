using UnityEngine;

public class Shape : MonoBehaviour
{
    public enum Type 
    {
        InvalidType,
        Circle,
        Square,
        Triangle,
    };

    private Vector3[] m_originalVertices;
    private Vector3[] m_customVertices;
    public Type m_geometryType = Type.InvalidType;
    public int m_resolution;

    private void Start ()
    {
        m_originalVertices = new Vector3[m_resolution];
        m_customVertices = new Vector3[m_resolution];
        GenerateShape();
    }

    private void Update ()
    {
        var scale = Matrix4x4.Scale(transform.localScale);
        var rotation = transform.rotation;
        var position = transform.position;

        // TODO: Try make this parallell. :)
        for (int i = 0; i < m_originalVertices.Length; ++i)
        {
            m_customVertices[i] = scale * m_originalVertices[i];
            m_customVertices[i] = rotation * m_customVertices[i];
            m_customVertices[i] += position;
        }
    }

    private void GenerateShape() 
    {
        switch(m_geometryType) 
        {
            case Type.Circle:
                Circle();
                break;
            case Type.Square:
                Square();
                break;
            case Type.Triangle:
                Triangle();
                break;
            default:
                Debug.LogError("Error in Shape.cs - Geometry shape unknown.");
                break;
        }
    }

    private void Triangle()
    {

    }

    private void Circle()
    {
        
    }

    private void Square()
    {
    
    }
}
