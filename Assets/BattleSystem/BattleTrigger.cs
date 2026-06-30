// BattleTrigger.cs — attach ke Enemy di world map
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTrigger : MonoBehaviour
{
    public EnemyData enemyData; // drag enemy ScriptableObject ke sini
    public float triggerRange = 1.5f;

    private Transform player;
    private bool battleStarted = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (battleStarted) return;

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= triggerRange)
        {
            battleStarted = true;
            // Simpan data enemy yang akan dihadapi
            BattleData.enemyToFight = enemyData;
            SceneManager.LoadScene("BattleScene");
        }
    }
}