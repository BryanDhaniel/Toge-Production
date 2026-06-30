using UnityEngine;
using UnityEngine.InputSystem;
using Fungus;

public class NPCInteractable : MonoBehaviour
{
    public Flowchart flowchart;
    public string blockName = "NPC_Help_Dialogue";
    public float interactRange = 1.5f;

    private Transform player;
    private bool isDialogueActive = false; // flag tambahan

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        // Subscribe ke event Fungus saat block selesai
        flowchart.GetComponent<Flowchart>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        bool playerInRange = distance <= interactRange;

        if (playerInRange && Keyboard.current.spaceKey.wasPressedThisFrame && !isDialogueActive)
        {
            isDialogueActive = true;
            flowchart.ExecuteBlock(blockName);
        }

        // Reset flag saat block sudah tidak jalan lagi
        if (isDialogueActive && !flowchart.HasExecutingBlocks())
        {
            isDialogueActive = false;
        }
    }
}