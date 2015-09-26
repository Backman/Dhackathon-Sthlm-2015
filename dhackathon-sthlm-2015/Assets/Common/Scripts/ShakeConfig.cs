using UnityEngine;

[CreateAssetMenu]
public class ShakeConfig : ScriptableObject
{
    public AnimationCurve X;
    public AnimationCurve Y;
    public AnimationCurve Strength;
    public float Duration;
    public float UpdateRate;
    //  public float Strength;
}