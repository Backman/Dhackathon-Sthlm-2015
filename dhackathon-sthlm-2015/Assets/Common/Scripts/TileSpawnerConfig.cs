using UnityEngine;

[CreateAssetMenu]
public class TileSpawnerConfig : ScriptableObject 
{
    public int Width;
    public int Height;
    public Vector2 Offset;
    public int TileSize;
    public float TileTweenDuration;
    public float TimeUntilTileTweening;
    public Vector2 TileTweenWaitInterval;
    public AnimationCurve TileTweeningCurve;
    public GameObject[] Tiles;
}