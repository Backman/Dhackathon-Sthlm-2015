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
    public Type GeometryType = Type.InvalidType;
    public int Resolution;

    private void Start ()
    {
        m_originalVertices = new Vector3[Resolution];
        m_customVertices = new Vector3[Resolution];
        GenerateShape();
    }

    private void Update ()
    {

    }

    private void GenerateShape() 
    {
        switch(GeometryType) 
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
