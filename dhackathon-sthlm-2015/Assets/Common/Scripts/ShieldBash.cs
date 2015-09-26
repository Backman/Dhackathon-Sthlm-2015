using UnityEngine;

[RequireComponent(typeof(Character))]
public class ShieldBash : MonoBehaviour
{
    [SerializeField]
    private Shield _shield;

    private Character _character;

    private void Awake()
    {
        _character = GetComponent<Character>();
    }

    private void Update()
    {
        DoInput();
    }

    private void DoInput()
    {
        string buttonName = InputManager.GetInputName(_character.PlayerValue, InputManager.InputType.ShieldBash);
        if (Input.GetButtonDown(buttonName))
        {
            _shield.Bash(BashValues.BashTime, BashValues.BashMultiplier);
        }
    }
}