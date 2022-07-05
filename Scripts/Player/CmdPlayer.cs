using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdPlayer : MonoBehaviour
{
    [SerializeField]
    private Player playerScript;
    [SerializeField]
    private PlayerUI playerUIscript;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (playerScript.isReady == false)
            {
                playerScript.isReady = true;
                playerUIscript.ReadyUI();
                
            }
            else
            {
                playerScript.isReady = false;
                playerUIscript.NotReadyUI();
            }
        }
    }


}
