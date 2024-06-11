using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Dialogue : MonoBehaviour
{
    [Header("dailogue talking settings")]

    private bool manualTalkOrder = false;
    [SerializeField] private bool paigeStarts = true;
    [SerializeField] private bool onlyPaige = false;
    [SerializeField] private bool manNPC;
    public float textSpeed;

    [Header("(only aplicable when manualTalkOrder is true)")]
    public string[] lines;
    [SerializeField] private bool[] talkOrder;




    [Header("things that have to be assigned")]
    public TextMeshProUGUI textComponent;
    public GameObject Dialoguee;
    public GameObject paigeBox;
    public GameObject NPCBox;
    private int index;
    public Interractable InterractableScript;
    public GameObject player;
    public GameObject dialogueTrigger;
    public SFX SFX;

    private bool turnedOn;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;

        if (lines.Length == talkOrder.Length)
        {
            manualTalkOrder = true;
        }
        else
        {
            manualTalkOrder = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (turnedOn)
        {
            if (InterractableScript && InterractableScript.ActivateDialogue == true)
            {
                if (manualTalkOrder)
                {
                    if (talkOrder[0])
                    {
                        paigeBox.SetActive(true);
                        NPCBox.SetActive(false);
                        paigeStarts = true;
                    }
                    else
                    {
                        paigeStarts = false;
                        paigeBox.SetActive(false);
                        NPCBox.SetActive(true);
                    }
                }
                else if (paigeStarts)
                {
                    paigeBox.SetActive(true);
                    NPCBox.SetActive(false);
                }
                else
                {
                    paigeBox.SetActive(false);
                    NPCBox.SetActive(true);
                }
                Dialoguee.SetActive(true);
                StartDialogue();
                InterractableScript.ActivateDialogue = false;
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
                }
            }
        }

    }

    void StartDialogue()
    {
        index = 0;



        textComponent.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        if (manualTalkOrder && talkOrder[index])
        {
            NPCBox.SetActive(false);
            paigeBox.SetActive(true);
            SFX.PlayDialogueSound();
        }
        else if (paigeStarts)
        {
            NPCBox.SetActive(false);
            paigeBox.SetActive(true);
            SFX.PlayDialogueSound();
        }
        else
        {
            NPCBox.SetActive(true);
            paigeBox.SetActive(false);
            if (manNPC)
            {
                SFX.PlayManHmmSound();
            }
            else
            {
                SFX.PlayDialogueSound();
            }
        }
        // Type things one by one
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }


    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {

            index++;
            if (!onlyPaige && !manualTalkOrder)
            {
                paigeStarts = !paigeStarts;
            }
            else if (onlyPaige && !manualTalkOrder)
            {
                paigeStarts = true;
            }
            else
            {
                paigeStarts = talkOrder[index];
            }
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            Dialoguee.SetActive(false);
            //dialogueTrigger.SetActive(false);
            //Time.timeScale = 1f;
            turnedOn = false;
            player.GetComponent<Movement>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the one we are interested in
        if (other.gameObject == player)
        {
            triggerDialogue();
            
        }

    }
    public void triggerDialogue()
    {
        Dialoguee.SetActive(true);
            turnedOn = true;
            StartDialogue();
            player.GetComponent<Movement>().enabled = false;
            //Time.timeScale = 0f;
    }


}







/*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Dialogue : MonoBehaviour
{
    [ContextMenuItem("Reset", "ResetBiography")]
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
           dialogueTrigger.SetActive(false);
            Time.timeScale = 1f;
            //player.GetComponent<Movement>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the one we are interested in
        if (other.gameObject == player)
        {
            
            Dialoguee.SetActive(true);
            StartDialogue();
            //player.GetComponent<Movement>().enabled = false;
            Time.timeScale = 0f;

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
        
    }


}
*/