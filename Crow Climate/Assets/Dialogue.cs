using UnityEngine;
using TMPro;  // Import for TextMeshPro support

public class NPCDialogue : MonoBehaviour
{
    public TMP_Text dialogueText; // Reference to the dialogue UI text
    public string[] dialogueLines; // Array of dialogue lines
    private int currentLine = 0;
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ShowDialogue();
        }
    }

    void ShowDialogue()
    {
        if (dialogueLines.Length > 0)
        {
            dialogueText.text = dialogueLines[currentLine];
            currentLine++;

            if (currentLine >= dialogueLines.Length)
            {
                currentLine = 0; // Reset dialogue cycle
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            dialogueText.text = ""; // Clear dialogue when player leaves
        }
    }
}
