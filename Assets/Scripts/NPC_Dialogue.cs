using UnityEngine;
using Fungus;

public class NPCInteractable : MonoBehaviour
{
    public Flowchart flowchart;       // drag GameObject Flowchart ke sini
    public string blockName;          // nama block, misal "NPC_Toko_Dialog"

    private bool playerInRange = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            flowchart.ExecuteBlock(blockName);
        }
    }
}