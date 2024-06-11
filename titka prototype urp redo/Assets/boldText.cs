using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class boldText : MonoBehaviour
{
    public TextMeshProUGUI textComponent;

    void Start()
    {
        textComponent.text = "The crystals are as <b>magical</b> as I remember.";
    }
}
