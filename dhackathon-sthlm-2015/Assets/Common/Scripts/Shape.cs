using UnityEngine;

public class Shape : MonoBehaviour
{
    private const float Tau = Mathf.PI * 2;

    private Vector3[] m_vertices;
    private float[] m_offsets;
    private LinePrinter.DrawObject m_drawObject;
    private LinePrinter m_printer;

    public GeometryType m_geometryType = GeometryType.Circle;
    public int m_resolution = 1024;
    public Color m_color = Color.white;

    private void Start ()
    {
        m_printer = Camera.main.GetComponent<LinePrinter>();
        m_vertices = new Vector3[m_resolution];
        m_offsets = new float[m_resolution];
        m_drawObject = new LinePrinter.DrawObject(new Vector3[m_resolution], m_color);
        GenerateShape();
    }

    public void SetOffset(float[] offsets)
    {
        if (offsets.Length != m_resolution) return;
        m_offsets = offsets;
    }

    private void Update ()
    {
        var scale = Matrix4x4.Scale(transform.localScale);
        var rotation = transform.rotation;
        var position = transform.position;

        // TODO: Try make this parallell. :)
        for (int i = 0; i < m_vertices.Length; ++i)
        {
            m_drawObject.List[i] = scale * m_vertices[i] * (1.0f + m_offsets[i]);
            m_drawObject.List[i] = rotation * m_drawObject.List[i];
            m_drawObject.List[i] += position;
        }

        // Enqueue this drawObject to camera's PostRenderer
        m_printer.Objects.Enqueue(m_drawObject);
    }

    private void GenerateShape() 
    {
        switch(m_geometryType) 
        {
            case GeometryType.Circle:
                Circle();
                break;
            case GeometryType.Square:
                Square();
                break;
            case GeometryType.Triangle:
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
        var pointsInCircle = m_vertices.Length;
        for (int i = 0; i < pointsInCircle; ++i)
        {
            float theta = Tau * i / pointsInCircle;
            m_vertices[i] = new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), transform.position.z);
        }
        m_vertices[m_vertices.Length - 1] = m_vertices[0];
    }

    private void Square()
    {
    
    }
}
