using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;




public class Inv_open : MonoBehaviour

{
    public Image page1Image;
    public Image page2Image;

   

    private void Start()
    {
        // Deactivate the images when the game starts
        page1Image.gameObject.SetActive(false);
        page2Image.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TogglePage();
        }
    }

    

    public void NextPage()
    {
        Debug.Log("Switching page...");

        if (page1Image.gameObject.activeSelf)
        {
            page1Image.gameObject.SetActive(false);
            page2Image.gameObject.SetActive(true);
        }
    }

    public void LastPage()
    {
        Debug.Log("Switching page...");

        if (page2Image.gameObject.activeSelf)
        {
            page2Image.gameObject.SetActive(false);
            page1Image.gameObject.SetActive(true);
        }
    }

    public void TogglePage()
    {
        Debug.Log("Toggling page1Image...");

        // Toggle the active state of page1Image
        page1Image.gameObject.SetActive(!page1Image.gameObject.activeSelf);
    }
}
