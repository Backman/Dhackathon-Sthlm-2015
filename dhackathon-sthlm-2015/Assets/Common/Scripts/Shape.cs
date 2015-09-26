using UnityEngine;
using System.Threading;

public class Shape : MonoBehaviour
{
    private const float Tau = Mathf.PI * 2;
    private Vector3[] m_vertices;
	[HideInInspector]
    public float[] m_offsets;

    private LinePrinter.DrawObject m_drawObject;
    private LinePrinter m_printer;

    public GeometryType m_geometryType = GeometryType.Circle;
    private GeometryType m_prevType = GeometryType.Circle;
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

    public void AddOffset(float[] offsets)
    {
		if(offsets.Length != m_resolution) return;
        for (int i = 0; i < offsets.Length; ++i)
        {
            m_offsets[i] += offsets[i];
        }
    }

    public void SetOffset(float[] offsets)
    {
        if (offsets.Length != m_resolution) return;
        m_offsets = offsets;
    }

    private void Update ()
    {
        if(m_vertices.Length <= 0)
            return;
        // If someone changed the enum type, generate a new shape.
        if(m_geometryType != m_prevType)
        {
            m_prevType = m_geometryType;
            GenerateShape();
        }
        
        var scale = Matrix4x4.Scale(transform.lossyScale);
        var rotation = transform.rotation;
        var position = transform.position;

        for (int i = 0; i < m_vertices.Length; ++i)
        {
            if (m_geometryType == GeometryType.Line)
            {
                m_drawObject.List[i] = scale * m_vertices[i];
                m_drawObject.List[i].y += m_offsets[i];
            }
            else
            {
                m_drawObject.List[i] = scale * m_vertices[i] * (1.0f + m_offsets[i]);
            }
            m_drawObject.List[i] = rotation * m_drawObject.List[i];

            m_drawObject.List[i] += position;
        }
        m_drawObject.Color = m_color;

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
            case GeometryType.Line:
                Line();
                break;
            default:
                Debug.LogError("Error in Shape.cs - Geometry shape unknown.");
                break;
        }
    }

    private void Line()
    {
        var space = 1f / (m_vertices.Length - 1);

        for(int i = 0; i < m_vertices.Length; ++i)
        {
            m_vertices[i] = new Vector3(-0.5f + i * space, 0, transform.position.z);
        }
    }

    private void Triangle()
    {
        if(m_vertices.Length <= 0)
            return;

        // 0.75 = 1^2 - (1 / 2)^2
        var height = Mathf.Sqrt(0.75f);
        var verticesPerSide = m_vertices.Length / 3;
        var space = 1f / verticesPerSide;
        var offset = new Vector3(0, -height / 2, 0);
        
        var dir1 = new Vector3(0, height, 0) - new Vector3(-0.5f,0,0);
        var dir2 = new Vector3(0.5f, 0, 0) - new Vector3(0,height,0);

        for(int i = 0; i < verticesPerSide; ++i)
        {
            m_vertices[i] = new Vector3(0.5f - i * space, 0, transform.position.z) + offset;
            m_vertices[i + verticesPerSide] = dir1 * i * space + new Vector3(-0.5f, 0, transform.position.z) + offset;
            m_vertices[i + verticesPerSide * 2] = dir2 * i * space + new Vector3(0, height, transform.position.z) + offset;
        }

        m_vertices[m_vertices.Length-1] = m_vertices[0];
    }

    private void Circle()
    {
        if(m_vertices.Length <= 0)
            return;

        var pointsInCircle = m_vertices.Length;
        for (int i = 0; i < pointsInCircle; ++i)
        {
            float theta = Tau * i / pointsInCircle;
            m_vertices[i] = new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), transform.position.z) * 0.5f;
        }
        m_vertices[m_vertices.Length - 1] = m_vertices[0];
    }

    private void Square()
    {
        if(m_vertices.Length <= 0)
            return;

        var verticesPerSide = m_vertices.Length / 4;
        var space = 1f / verticesPerSide;

        for(int i = 0; i < verticesPerSide; ++i)
        {
            m_vertices[i] = new Vector3(-0.5f + i * space, 0.5f, transform.position.z);
            m_vertices[i + verticesPerSide] = new Vector3(0.5f, 0.5f - i*space, transform.position.z);
            m_vertices[i + verticesPerSide * 2] = new Vector3(0.5f - i * space,-0.5f, transform.position.z);
            m_vertices[i + verticesPerSide * 3] = new Vector3(-0.5f, -0.5f + i * space, transform.position.z);
        }
        m_vertices[m_vertices.Length - 1] = m_vertices[0];
    }
}
