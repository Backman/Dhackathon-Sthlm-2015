using UnityEngine;
using System.Collections.Generic;

public class LinePrinter : MonoBehaviour
{
    public struct DrawObject
    {
        public DrawObject(Vector3[] l, Color c)
        {
            Trs = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one);
            List = l;
            Color = c;
        }
        
        public Matrix4x4 Trs;
        public Vector3[] List;
        public Color Color;
    }

    public Material m_material;
    public Queue<DrawObject> Objects;
    private Camera m_camera;

    private void Awake()
    {
        Objects = new Queue<DrawObject>();
    }

    private void Start()
    {
        m_camera = gameObject.GetComponent<Camera>();
    }

    private void OnPostRender()
    {
        if (!m_material)
        {
            Debug.LogError("Please Assign a material on the inspector");
            return;
        }

        // Push matrix and stuff, and draw whole queue.
        GL.PushMatrix();
        m_material.SetPass(0);
        GL.LoadIdentity();
        GL.MultMatrix(m_camera.worldToCameraMatrix);
        DrawQueue();
        GL.PopMatrix();
    }

    private void DrawQueue()
    {
        while (Objects.Count > 0)
        {
            var obj = Objects.Dequeue();
            var points = obj.List;
            var color = obj.Color;

            for (int i = 0; i < points.Length - 1; ++i)
            {
                Line(points[i], points[i + 1], color);
            }
        }
    }

    private void Line(Vector3 point0, Vector3 point1, Color color)
    {
        GL.Begin(GL.LINES);
        GL.Color(color);
        GL.Vertex(point0);
        GL.Vertex(point1);
        GL.End();
    }
}
