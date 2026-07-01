using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public enum BattleState
{
    Start,
    PlayerTurn,
    EnemyTurn,
    Won,
    Lost
}

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    public BattleUnit playerUnit;
    public BattleUnit enemyUnit;
    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;
    public GameObject battleActionButtons; // panel tombol Attack/Skill/Run
    public TextMeshProUGUI battleLogText;  // teks log pertarungan

    public BattleState currentState;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Ambil data enemy dari BattleData yang dikirim world map
        if (BattleData.enemyToFight != null)
        {
            enemyUnit.enemyData = BattleData.enemyToFight;
        }

        currentState = BattleState.Start;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        playerUnit.Setup();
        enemyUnit.Setup();

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        battleActionButtons.SetActive(false);
        SetBattleLog($"A wild {enemyUnit.unitName} appears!");

        yield return new WaitForSeconds(1.5f);

        PlayerTurn();
    }

    void PlayerTurn()
    {
        currentState = BattleState.PlayerTurn;
        battleActionButtons.SetActive(true);
        SetBattleLog("Your turn! Choose an action.");
    }

    // Dipanggil saat tombol "Attack" diklik
    public void OnAttackButton()
    {
        if (currentState != BattleState.PlayerTurn) return;
        battleActionButtons.SetActive(false);
        StartCoroutine(PlayerAttack());
    }

    public void OnRunButton()
    {
        if (currentState != BattleState.PlayerTurn) return;
        SetBattleLog("You fled from battle!");
        StartCoroutine(RunAway());
    }

    IEnumerator RunAway()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("SampleScene");
    }

    IEnumerator PlayerAttack()
    {
        int dmg = Mathf.Max(1, playerUnit.attack - enemyUnit.defense);
        SetBattleLog($"{playerUnit.unitName} attacks {enemyUnit.unitName} for {dmg} damage!");
        bool enemyAlive = enemyUnit.TakeDamage(playerUnit.attack);
        enemyHUD.UpdateHP(enemyUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if (!enemyAlive)
        {
            currentState = BattleState.Won;
            StartCoroutine(EndBattle());
        }
        else
        {
            currentState = BattleState.EnemyTurn;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        SetBattleLog($"{enemyUnit.unitName} is preparing to strike...");
        yield return new WaitForSeconds(1f);

        int dmg = Mathf.Max(1, enemyUnit.attack - playerUnit.defense);
        bool playerAlive = playerUnit.TakeDamage(enemyUnit.attack);
        SetBattleLog($"{enemyUnit.unitName} attacks {playerUnit.unitName} for {dmg} damage!");
        playerHUD.UpdateHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if (!playerAlive)
        {
            currentState = BattleState.Lost;
            StartCoroutine(EndBattle());
        }
        else
        {
            PlayerTurn();
        }
    }

    IEnumerator EndBattle()
    {
        if (currentState == BattleState.Won)
        {
            SetBattleLog("Victory! You defeated the enemy!");
            Debug.Log("Menang!");
        }
        else
        {
            SetBattleLog("Defeated... Returning to World Map.");
            Debug.Log("Kalah!");
        }

        yield return new WaitForSeconds(2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("WorldMap");
    }

    void SetBattleLog(string message)
    {
        if (battleLogText != null)
            battleLogText.text = message;
        Debug.Log($"[BattleLog] {message}");
    }
}