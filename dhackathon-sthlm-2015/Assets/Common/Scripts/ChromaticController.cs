using UnityEngine;
using System.Collections;

public class ChromaticController : MonoBehaviour
{
    public static ChromaticController Instance;

    [SerializeField]
    private AnimationCurve _buildUpCurve;
    [SerializeField]
    private float _chromaticMultiplier = 2f;

    private UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration _chromatic;
    private bool _isImba;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
		
        Instance = this;
        _chromatic = GetComponent<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration>();
    }

    public void StartImbaland(float duration)
    {
        StartCoroutine(Imbaland(duration));
        _isImba = true;
    }

    private IEnumerator Imbaland(float duration)
    {
        if (_isImba)
        {
            yield break;
        }

        var startTime = Time.unscaledTime;
        var startChromatic = _chromatic.chromaticAberration;
        while (startTime + duration > Time.unscaledTime)
        {
            var t = (Time.unscaledTime - startTime) / duration;
            var eval = _buildUpCurve.Evaluate(t);
            _chromatic.chromaticAberration = startChromatic + eval * _chromaticMultiplier;;
            yield return null;
        }

        _chromatic.chromaticAberration = startChromatic;
        _isImba = false;
    }
}
