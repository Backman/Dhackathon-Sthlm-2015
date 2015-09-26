using UnityEngine;

[CreateAssetMenu]
public class TileSpawnerConfig : ScriptableObject 
{
    public int Width;
    public int Height;
    public int TileSize;
    public GameObject[] Tiles;
}