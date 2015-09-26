using UnityEngine;
using System.Collections.Generic;

public class ShapeDistorter : MonoBehaviour
{
    private struct ActiveDistort
    {
        public Shape Shape;
        public float StartTime;
        public float Duration;
        public float Intensity;
        public float[] Offsets;
        public float[] OriginalOffsets;
        public DistortCallback Callback;
    }

    public delegate void DistortCallback();

    public static ShapeDistorter Instance;

    private List<ActiveDistort> _activeDistorts = new List<ActiveDistort>();
    private List<ActiveDistort> _toRemove = new List<ActiveDistort>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        for (int i = 0; i < _activeDistorts.Count; ++i)
        {
            var distort = _activeDistorts[i];
            if (distort.StartTime + distort.Duration < Time.unscaledTime)
            {
                _toRemove.Add(distort);
                ResetShape(distort.Shape);
                if (distort.Callback != null)
                {
                    distort.Callback();
                }
                continue;
            }

            Distort(distort);
        }
		
		foreach (var index in _toRemove)
		{
            _activeDistorts.Remove(index);
        }

        _toRemove.Clear();
    }

    private void ResetShape(Shape shape)
    {
        var offsets = new float[shape.m_resolution];
        shape.SetOffset(offsets);
    }

    private void Distort(ActiveDistort distort)
    {
        var intensity = distort.Intensity;
        for (int i = 0; i < distort.Offsets.Length; i++)
		{
            distort.Offsets[i] = Random.insideUnitCircle.x * intensity + distort.OriginalOffsets[i];
        }
        distort.Offsets[distort.Offsets.Length - 1] = distort.Offsets[0];
        distort.Shape.SetOffset(distort.Offsets);
    }

    public void AddDistort(Shape shape, float intensity, float duration, DistortCallback callback = null)
    {
        var distort = new ActiveDistort()
        {
            Shape = shape,
            StartTime = Time.unscaledTime,
            Duration = duration,
            Intensity = intensity,
            Offsets = new float[shape.m_resolution],
            OriginalOffsets = shape.m_offsets,
        	Callback = callback
    	};
		
        _activeDistorts.Add(distort);
    }
}