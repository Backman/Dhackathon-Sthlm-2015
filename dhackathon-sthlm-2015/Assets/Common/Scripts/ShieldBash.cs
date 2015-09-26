using UnityEngine;

[RequireComponent(typeof(Character))]
public class ShieldBash : MonoBehaviour
{
    [SerializeField]
    private Shield _shield;

    private bool _isBashing;

    private Character _character;
    private Animator _animator;

    private void Awake()
    {
        _character = GetComponent<Character>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        DoInput();
    }

    public void ResetIsBashing()
    {
        _isBashing = false;
    }

    private void DoInput()
    {
        string buttonName = InputManager.GetInputName(_character.PlayerValue, InputManager.InputType.ShieldBash);
        if (!_isBashing && Input.GetButtonDown(buttonName))
        {
            _isBashing = true;
            _animator.SetTrigger("Bash");
            _shield.Bash(BashValues.BashTime, BashValues.BashMultiplier);
        }
    }
}