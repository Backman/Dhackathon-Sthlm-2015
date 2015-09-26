using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

public class Tile : MonoBehaviour
{
    private Vector3 _goalPosition;

    public Tile Init(Vector3 goalPosition)
    {
        _goalPosition = goalPosition;
        return this;
    }

    public void MoveToGoalPosition(float duration, AnimationCurve curve, float waitDuration)
    {
        StartCoroutine(MoveToGoal(duration, curve, waitDuration));
    }

    private IEnumerator MoveToGoal(float duration, AnimationCurve curve, float waitDuration)
    {
        yield return new WaitForSeconds(waitDuration);
		
        transform.DOMove(_goalPosition, duration).SetEase(curve);
        transform.DORotate(Vector3.zero, duration);
    }
}
