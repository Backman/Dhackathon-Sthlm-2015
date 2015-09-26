using UnityEngine;
using System.Collections;
using DG.Tweening;


public class Tile : MonoBehaviour
{
    [SerializeField]
    private Vector2 _tileDropDuration = new Vector2(0.3f, 1f);
    [SerializeField]
    private AudioSource _audio;
    [SerializeField]
    private Color[] _dropColors;

    private Vector3 _goalPosition;
	
	public bool IsAlive { get; set; }

    public Tile Init(Vector3 goalPosition)
    {
        IsAlive = true;
        _goalPosition = goalPosition;
        return this;
    }

    public void RemoveTile()
    {
        StartCoroutine(DropTile());
    }

    private IEnumerator DropTile()
    {
        var duration = Random.Range(_tileDropDuration.x, _tileDropDuration.y);
        var index = Random.Range(0, _dropColors.Length);
        GetComponent<SpriteRenderer>().DOColor(_dropColors[index], duration);
        transform.DOShakeScale(duration, 0.5f);
        transform.DOScale(Vector3.zero, duration);
        transform.DOShakeRotation(duration, new Vector3(0f, 0f, 90f));
        transform.DORotate(new Vector3(0f, 0f, 360f), duration);
		
        yield return new WaitForSeconds(duration);
		
        IsAlive = false;
        _audio.Play();
    }

    public void MoveToGoalPosition(float duration, AnimationCurve curve, float waitDuration)
    {
        StartCoroutine(MoveToGoal(duration, curve, waitDuration));
    }

    private IEnumerator MoveToGoal(float duration, AnimationCurve curve, float waitDuration)
    {
        yield return new WaitForSeconds(waitDuration);
		
        var tween = transform.parent.DOMove(_goalPosition, duration).SetEase(curve);
        transform.parent.DORotate(Vector3.zero, duration);

        yield return tween.WaitForCompletion();
		
        GameLogic.Instance.AddTile(this);
    }
}
