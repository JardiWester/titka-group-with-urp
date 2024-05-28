using UnityEngine;

public class DeactivateMark : MonoBehaviour
{
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] GameObject exclamationMark;
    private bool playerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player entered NPC range");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player exited NPC range");
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            HideExclamationMark();            
        }
    }

    void HideExclamationMark()
    {  
        Debug.Log("Trying to hide Exclamation mark");

        //Transform exclamationMark = transform.Find("Exclamation Mark");
        if (exclamationMark == null)
        {            
            Debug.LogError("No exclamation connected");
            return;
        }

        exclamationMark.gameObject.SetActive(false);
        Debug.Log("Exclamation mark hidden");
    }
}