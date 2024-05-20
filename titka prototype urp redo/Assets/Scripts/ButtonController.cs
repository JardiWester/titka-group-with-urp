using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public Inv_open Inv_open;

    public void OnButtonClickNext()
    {
        Inv_open.NextPage();
    }
    public void OnButtonClickLast()
    {
        Inv_open.LastPage();
    }

}



