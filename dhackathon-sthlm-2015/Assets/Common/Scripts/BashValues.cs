using UnityEngine;

public static class BashValues
{
    private static BashConfig _config;
	
	public static float BashTime { get { return _config.BashTime; } }
	public static float BashMultiplier { get { return _config.BashMutliplier; } }

    static BashValues()
    {
        _config = Resources.Load<BashConfig>("Configs/BashConfig");
    }
}