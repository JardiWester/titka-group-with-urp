using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcInteract : MonoBehaviour
{

    private bool hasInteracted = false;
    public Transform player;
    public float distanceThreshold = 10f;
    public Dialogue dialogueScript;

    // Start is called before the first frame update
    void Start()
    {
        dialogueScript = gameObject.GetComponent<Dialogue>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasInteracted == false)
            {
                // Calculate the distance between the player and the current puzzle
                float distance = Vector3.Distance(player.position, transform.position);

                // Check if the distance is less than or equal to the threshold
                if (distance <= distanceThreshold)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                       dialogueScript.triggerDialogue();
                    }
                }
            }
    }
}
