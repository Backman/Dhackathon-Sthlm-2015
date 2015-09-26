using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private Transform _playerOne;
    [SerializeField]
    private Transform _playerTwo;
    [SerializeField]
    private GameObject _pointObject;

    private static int _scoreOne = 0;
    private static int _scoreTwo = 0;

    private bool _scoreAdded = false;

    void Awake ()
    {
        UpdateScore();
    }
	
    private void UpdateScore()
    {
        for(int i = 0; i < _scoreOne; ++i)
        {
            GameObject point = GameObject.Instantiate<GameObject>(_pointObject);
            point.transform.parent = _playerOne;
            int x = i % 5;
            int y = i / 5;
            point.transform.localPosition = new Vector3(x * 10, y * -10, 0);
            point.transform.localScale = new Vector3(5, 1, 1);
        }
        for(int i = 0; i < _scoreTwo; ++i)
        {
            GameObject point = GameObject.Instantiate<GameObject>(_pointObject);
            point.transform.parent = _playerTwo;
            int x = i % 5;
            int y = i / 5;
            point.transform.localPosition = new Vector3(x * -10, y * -10, 0);
            point.transform.localScale = new Vector3(5, 1, 1);
        }
    }

	public void AddScoreOne()
    {
        if (_scoreAdded) return;
        _scoreAdded = true;
        _scoreOne++;
        UpdateScore();
    }

    public void AddScoreTwo()
    {
        if (_scoreAdded) return;
        _scoreAdded = true;
        _scoreTwo++;
        UpdateScore();
    }
}
