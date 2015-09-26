using UnityEngine;

[RequireComponent(typeof(Character))]
public class ShieldBash : MonoBehaviour
{
    [SerializeField]
    private Shield _shield;

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
        _character.IsBashing = false;
    }

    private void DoInput()
    {
        string buttonName = InputManager.GetInputName(_character.PlayerValue, InputManager.InputType.ShieldBash);
        if (!_character.IsBashing && Input.GetButtonDown(buttonName))
        {
            _character.IsBashing = true;
            _animator.SetTrigger("Bash");
            _shield.Bash(BashValues.BashTime, BashValues.BashMultiplier);
        }
    }
}