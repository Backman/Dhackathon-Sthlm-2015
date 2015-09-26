using UnityEngine;
using System.Collections;

public class FreezeManager : MonoBehaviour
{
    public static FreezeManager Instance;

    [SerializeField, Range(0f, 1f)]
    private float _freezeValue;
    [SerializeField]
    private float _freezeTime = 0.2f;
    private bool _isFreezing;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void Freeze(bool screenShakeAfter, float duration)
    {
        MusicController.Instance.StartFadeIn(duration);
        StartCoroutine(StartFreeze(screenShakeAfter, duration));
        _isFreezing = true;
    }

    private IEnumerator StartFreeze(bool screenShakeAfter, float duration)
    {
        float startTime = Time.unscaledTime;
        Time.timeScale = _freezeValue;
        while (startTime + duration > Time.unscaledTime)
        {
            yield return null;
        }

        Time.timeScale = 1f;
        //  if (screenShakeAfter)
        //  {
        //      ScreenShaker.Instance.ScreenShake();
        //  }
        _isFreezing = false;
    }
}