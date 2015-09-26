using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLogic : MonoBehaviour
{
    public static GameLogic Instance;

    private List<Tile> _tiles = new List<Tile>();
    private List<Tile> _tilesToRemove = new List<Tile>();

	[SerializeField]
    private GameConfig _config;

    [SerializeField]
    private GameObject _gameplayObject;

    private int _currentTileDropSetting;
    private float _tileDropTime;
    private bool _tileTimerStarted;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if(_gameplayObject != null)
            _gameplayObject.SetActive(false);
    }

    private void Update()
    {
        if (_tileTimerStarted && _tileDropTime < Time.unscaledTime)
        {
            SetTileDropTime();
            DropTiles();
        }

        foreach (var tile in _tilesToRemove)
        {
            _tiles.Remove(tile);
        }

        _tilesToRemove.Clear();
    }

    public void AddTile(Tile tile)
    {
        _tiles.Add(tile);
    }

    public void RemoveTile(Tile tile)
    {
        _tilesToRemove.Add(tile);
    }

    public void StartTileTimer()
    {
        _tileTimerStarted = true;
        SetTileDropTime();

        if(_gameplayObject != null)
            _gameplayObject.SetActive(true);
    }

    private void SetTileDropTime()
    {
		if (!TileDropSettingsValid())
        {
            return;
        }
		
        var setting = _config.TileDropSettings[_currentTileDropSetting];
        _tileDropTime = Time.unscaledTime + Random.Range(setting.AfterTimeInterval.x, setting.AfterTimeInterval.y);
    }

    private void DropTiles()
    {
        if (!TileDropSettingsValid())
        {
            return;
        }
		
        var setting = _config.TileDropSettings[_currentTileDropSetting];
        var tilesToDrop = Random.Range(setting.TilesToDropInterval.x, setting.TilesToDropInterval.y);
        for (int i = 0; i < tilesToDrop; ++i)
        {
            var index = Random.Range(0, _tiles.Count);
            if (_tiles[index].IsAlive)
            {
                _tiles[index].RemoveTile();
                RemoveTile(_tiles[index]);
            }
        }
    }

    private bool TileDropSettingsValid()
    {
        return _currentTileDropSetting < _config.TileDropSettings.Length;
    }
}
