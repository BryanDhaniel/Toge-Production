using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "RPG/Character Data")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public Sprite battleSprite;
    public int maxHP;
    public int attack;
    public int defense;
}