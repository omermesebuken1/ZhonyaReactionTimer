using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingsAnim : MonoBehaviour
{
    [HideInInspector] public bool status;
    
    public void layout_opened()
    {
            status = true;
    }

    public void layout_closed()
    {
            status = false;
        
    }
}
