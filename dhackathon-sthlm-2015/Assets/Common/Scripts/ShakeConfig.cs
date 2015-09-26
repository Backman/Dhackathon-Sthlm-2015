using UnityEngine;

[CreateAssetMenu]
public class ShakeConfig : ScriptableObject
{
    public AnimationCurve X;
    public AnimationCurve Y;
    public float Duration;
    public float Strength;
}