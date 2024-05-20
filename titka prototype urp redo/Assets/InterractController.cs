using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InterractController : MonoBehaviour
{
    public Transform player; // Reference to the player object
    [SerializeField] private List<Transform> puzzles; // List of references to the puzzles
    [SerializeField] public float distanceThreshold = 10f; // Threshold distance for your condition
    public InputUIManager InputUIManager; // Reference to the script handling UI image instantiation

    private List<Interractable> interractables; // List of references to Interractable scripts for each puzzle
    public bool[] inRangeStatus; // Array to track whether the player is in range of each cave
    public Interractable Interractable;



    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Interractable>())
            {
               puzzles.Add(child);
            }	
        }
        // Initialize the list of Interractable scripts
        interractables = new List<Interractable>();
        foreach (var puzzle in puzzles)
        {
            interractables.Add(puzzle.GetComponent<Interractable>());
        }

        // Initialize the array to track in-range status for each puzzle
        inRangeStatus = new bool[puzzles.Count];
    }

    void Update()
    {
        // Loop through all puzzles
        for (int i = 0; i < puzzles.Count; i++)
        {
            if (interractables[i].hasInteracted == false)
            {
                // Calculate the distance between the player and the current puzzle
                float distance = Vector3.Distance(player.position, puzzles[i].position);

                // Check if the distance is less than or equal to the threshold
                if (distance <= distanceThreshold)
                {
                    // If the player is in range of this puzzle
                    if (!inRangeStatus[i])
                    {
                        inRangeStatus[i] = true;
                        
                        puzzles[i].GetComponent<Interractable>().glow();
                        Debug.Log("Player is close to puzzle " + (i + 1));
                        InputUIManager.ShowLogo(new Vector3 (puzzles[i].position.x , puzzles[i].position.y + 2, puzzles[i].position.z) ) ;
                    }

                    // Check if the player presses the interaction key
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        puzzles[i].GetComponent<Interractable>().ResetMaterial(); //stop glowing

                        // Call the interract method on the interractable instance of the current puzzle
                        interractables[i].interract();
                        InputUIManager.HideLogo();
                        
                    }
                }
                else
                {
                    // If the player is not in range of this puzzle
                    if (inRangeStatus[i])
                    {
                        puzzles[i].GetComponent<Interractable>().ResetMaterial();
                        inRangeStatus[i] = false;
                        Debug.Log("Player is not close to Cave " + (i + 1));
                        InputUIManager.HideLogo();
                    }
                }
            }
        }
    }
}
