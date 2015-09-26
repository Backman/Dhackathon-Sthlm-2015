using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileSpawner : MonoBehaviour
{
    [SerializeField]
    private TileSpawnerConfig _config;

    private Transform _tileParent;

    private void Awake()
    {
        Spawn();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Spawn();
        }
    }

    public void Spawn()
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

        var xOffset = (-width * tileSize) * 0.5f;
        var yOffset = (-height * tileSize) * 0.5f;

        if (_tileParent)
        {
            Destroy(_tileParent.gameObject);
        }
		
        _tileParent = new GameObject("Tiles").transform;

        List<Vector3> positions = new List<Vector3>();
        List<Tile> tileList = new List<Tile>();
        var camera = Camera.main;

        for (int x = 0; x < width; x++)
		{
            for (int y = 0; y < height; y++)
            {
				int i = Random.Range(0, tiles.Length);
                Vector2 randomPos = Random.insideUnitCircle.normalized;
                var offset = Vector2.one;
                offset.x *= randomPos.x;
                offset.y *= randomPos.y;
                var position = randomPos + offset * camera.orthographicSize * 2f;
                var goalPos = new Vector3(tileSize * x + xOffset, tileSize * y + yOffset, 0f);

                var rot = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
                var go = (GameObject)Instantiate(tiles[i], position, rot);
                go.transform.SetParent(_tileParent, true);
                var tile = go.AddComponent<Tile>().Init(goalPos);
                tileList.Add(tile);
            }
        }

        yield return new WaitForSeconds(_config.TimeUntilTileTweening);

        foreach (var tile in tileList)
        {
            tile.MoveToGoalPosition(_config.TileTweenDuration, _config.TileTweeningCurve, Random.Range(_config.TileTweenWaitInterval.x, _config.TileTweenWaitInterval.y));
        }

        yield return null;
    }
}