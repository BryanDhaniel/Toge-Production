// BattleManager.cs
using UnityEngine;
using System.Collections;

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

    public BattleState currentState;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
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

        yield return new WaitForSeconds(1f);

        PlayerTurn();
    }

    void PlayerTurn()
    {
        currentState = BattleState.PlayerTurn;
        battleActionButtons.SetActive(true); // tampilkan tombol
    }

    // Dipanggil saat tombol "Attack" diklik
    public void OnAttackButton()
    {
        if (currentState != BattleState.PlayerTurn) return;
        battleActionButtons.SetActive(false);
        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
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
        yield return new WaitForSeconds(1f); // delay sebelum enemy menyerang

        bool playerAlive = playerUnit.TakeDamage(enemyUnit.attack);
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
        yield return new WaitForSeconds(1f);

        if (currentState == BattleState.Won)
        {
            Debug.Log("Menang!");
            // Load kembali ke world map
            UnityEngine.SceneManagement.SceneManager.LoadScene("WorldMap");
        }
        else
        {
            Debug.Log("Kalah!");
            // Game over / reload
            UnityEngine.SceneManagement.SceneManager.LoadScene("WorldMap");
        }
    }
}