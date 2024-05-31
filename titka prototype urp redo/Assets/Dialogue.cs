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

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        //StartDialogue();
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
            }
        }

    }

    void StartDialogue ()
    {
        index = 0;
        StartCoroutine(TypeLine()); 
    }

    IEnumerator TypeLine() 
    { 
    // Type things one by one
    foreach (char c in lines[index]. ToCharArray()) 
        {
            textComponent.text += c; 
            yield return new WaitForSeconds (textSpeed) ;
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
        }
    }
}
