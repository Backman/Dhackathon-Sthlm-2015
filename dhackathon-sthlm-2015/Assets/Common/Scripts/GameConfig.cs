using UnityEngine;
using System.Collections;

[CreateAssetMenuAttribute]
public class GameConfig : ScriptableObject
{
    [System.SerializableAttribute]
    public struct TileDropSetting
    {
		public Vector2 TilesToDropInterval;
        public Vector2 AfterTimeInterval;
    }
    public TileDropSetting[] TileDropSettings;
}
