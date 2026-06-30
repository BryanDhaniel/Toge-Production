using UnityEngine;

public class BattleUnit : MonoBehaviour
{
    public CharacterData characterData; // untuk player
    public EnemyData enemyData;         // untuk enemy
    public bool isPlayerUnit;

    [HideInInspector] public string unitName;
    [HideInInspector] public int currentHP;
    [HideInInspector] public int maxHP;
    [HideInInspector] public int attack;
    [HideInInspector] public int defense;

    public void Setup()
    {
        if (isPlayerUnit)
        {
            unitName = characterData.characterName;
            maxHP = characterData.maxHP;
            attack = characterData.attack;
            defense = characterData.defense;
        }
        else
        {
            unitName = enemyData.enemyName;
            maxHP = enemyData.maxHP;
            attack = enemyData.attack;
            defense = enemyData.defense;
        }
        currentHP = maxHP;
    }

    // Return true kalau masih hidup
    public bool TakeDamage(int damage)
    {
        int actualDamage = Mathf.Max(1, damage - defense);
        currentHP -= actualDamage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        return currentHP > 0;
    }
}