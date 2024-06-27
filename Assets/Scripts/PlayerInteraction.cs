using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRadius = 2f; // Radius to detect interaction points
    public LayerMask interactionLayer; // Layer mask to filter interaction points
    public GameObject dialoguePopupPrefab; // Prefab for the dialogue pop-up
    private GameObject currentDialoguePopup;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactionRadius, interactionLayer);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Interactable"))
            {
                ShowDialogue(hit.transform.position);
                break;
            }
        }
    }

    private void ShowDialogue(Vector3 position)
    {
        if (currentDialoguePopup != null)
        {
            Destroy(currentDialoguePopup);
        }
        currentDialoguePopup = Instantiate(dialoguePopupPrefab, position + new Vector3(0, 1, 0), Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}