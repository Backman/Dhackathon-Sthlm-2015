using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    public enum InputType
    {
		Vertical,
		Horizontal,
		XRotation,
		YRotation,
		ShieldBash
    }
	
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
	private static string PlayerOneVert = "P1_Vertical_WIN";
	private static string PlayerOneHoriz = "P1_Horizontal_WIN";
	
	private static string PlayerTwoVert = "P2_Vertical_WIN";
	private static string PlayerTwoHoriz = "P2_Horizontal_WIN";
	
	private static string PlayerOneXRot = "P1_XRot_WIN";
	private static string PlayerOneYRot = "P1_YRot_WIN";
	
	private static string PlayerTwoXRot = "P2_XRot_WIN";
	private static string PlayerTwoYRot = "P2_YRot_WIN";
	
    private static string PlayerOneShieldBash = "P1_ShieldBash_WIN";
    private static string PlayerTwoShieldBash = "P2_ShieldBash_WIN";
#else
    private static string PlayerOneVert = "P1_Vertical";
	private static string PlayerOneHoriz = "P1_Horizontal";
	
	private static string PlayerTwoVert = "P2_Vertical";
	private static string PlayerTwoHoriz = "P2_Horizontal";
	
	private static string PlayerOneXRot = "P1_XRot";
	private static string PlayerOneYRot = "P1_YRot";
	
	private static string PlayerTwoXRot = "P2_XRot";
	private static string PlayerTwoYRot = "P2_YRot";

    private static string PlayerOneShieldBash = "P1_ShieldBash";
    private static string PlayerTwoShieldBash = "P2_ShieldBash";
#endif

    private static Dictionary<Player, Dictionary<InputType, string>> _playerInput;

    static InputManager()
    {
        _playerInput = new Dictionary<Player, Dictionary<InputType, string>>();
        InitInput();

        string[] names = Input.GetJoystickNames();
		for (int i = 0; i < names.Length; i++)
		{
            Debug.Log(names[i]);
        }
    }

    private static void InitInput()
    {
        Dictionary<InputType, string> playerOneInput = new Dictionary<InputType, string>();
        Dictionary<InputType, string> playerTwoInput = new Dictionary<InputType, string>();
		
        playerOneInput.Add(InputType.Horizontal, PlayerOneHoriz);
        playerOneInput.Add(InputType.Vertical, PlayerOneVert);
        playerOneInput.Add(InputType.XRotation, PlayerOneXRot);
        playerOneInput.Add(InputType.YRotation, PlayerOneYRot);
        playerOneInput.Add(InputType.ShieldBash, PlayerOneShieldBash);
		
        playerTwoInput.Add(InputType.Horizontal, PlayerTwoHoriz);
        playerTwoInput.Add(InputType.Vertical, PlayerTwoVert);
        playerTwoInput.Add(InputType.XRotation, PlayerTwoXRot);
        playerTwoInput.Add(InputType.YRotation, PlayerTwoYRot);
        playerTwoInput.Add(InputType.ShieldBash, PlayerTwoShieldBash);

        _playerInput.Add(Player.PlayerOne, playerOneInput);
        _playerInput.Add(Player.PlayerTwo, playerTwoInput);
    }

    public static string GetInputName(Player player, InputType type)
    {
        return _playerInput[player][type];
    }
}