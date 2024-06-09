using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public GameObject Dialoguee;
    private int index;
    public Interractable PuzzleInterractableScript;
    public GameObject player;
    public GameObject dialogueTrigger;
    
    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( PuzzleInterractableScript.ActivateDialogue == true) 
        {
           
            Dialoguee.SetActive (true);
            StartDialogue ();
            PuzzleInterractableScript.ActivateDialogue = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
                Dialoguee.SetActive(false);
                player.GetComponent<Movement>().enabled = true;
            }
        }

    }

    void StartDialogue ()
    {
        index = 0;
        textComponent.text = string.Empty;
        StartCoroutine(TypeLine()); 
    }

    IEnumerator TypeLine() 
    { 
    // Type things one by one
    foreach (char c in lines[index]. ToCharArray()) 
        {
            textComponent.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }
    
    void NextLine ()
    {
        if (index < lines.Length -1 ) 
        {
            index++; 
            textComponent.text = string.Empty ;
            StartCoroutine(TypeLine());
        }
        else 
        { 
           Dialoguee.SetActive(false);
           //dialogueTrigger.SetActive(false);
            Time.timeScale = 1f;
            player.GetComponent<Movement>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the one we are interested in
        if (other.gameObject == player)
        {
            
            Dialoguee.SetActive(true);
            StartDialogue();
            player.GetComponent<Movement>().enabled = false;
            //Time.timeScale = 0f;
            

            if (Input.GetMouseButtonDown(0))
            {
                if (textComponent.text == lines[index])
                {
                    NextLine();
                }
                else
                {
                   /* StopAllCoroutines();
                    textComponent.text = lines[index];
                    Dialoguee.SetActive(false);*/
                    

                }
            }
        }
        
    }


}
