using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "RPG/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public Sprite enemySprite;
    public int maxHP;
    public int attack;
    public int defense;
    public int expReward;
}