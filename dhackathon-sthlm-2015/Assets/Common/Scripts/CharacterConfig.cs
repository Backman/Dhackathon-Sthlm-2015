using UnityEngine;

[CreateAssetMenuAttribute]
public class CharacterConfig : ScriptableObject
{
    public int Health;
    public float MovementSpeed;
    public GameObject DeathParticle;
    public KnockbackConfig KnockbackConfig;
}