using UnityEngine;
using System.Collections;

public class TileSpawner : MonoBehaviour
{
    [SerializeField]
    private TileSpawnerConfig _config;

    private void Awake()
    {
        StartCoroutine(SpawnTiles());
    }

    private IEnumerator SpawnTiles()
    {
        Random.seed = (int)System.DateTime.Now.Ticks;

        var width = _config.Width;
        var height = _config.Height;
        var tiles = _config.Tiles;
        var tileSize = _config.TileSize;

        var xPos = -width / (float)tileSize;
        var yPos = -height / (float)tileSize;
        var pos = new Vector2(xPos, yPos);

        var tileParent = new GameObject("Tiles");

        for (int x = 0; x < width; x++)
		{
            for (int y = 0; y < height; y++)
            {
                int i = Random.Range(0, tiles.Length);
                var tile = (GameObject)Instantiate(tiles[i], new Vector3(pos.x + x, pos.y + y, 0f), Quaternion.identity);
                tile.transform.SetParent(tileParent.transform, true);
                pos.y += tileSize;
            }
            pos.y = -height / (float)tileSize;
            pos.x += tileSize;
        }

        yield return null;
    }
}