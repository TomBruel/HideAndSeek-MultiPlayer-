using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private GameObject scoreboard;
    [SerializeField]
    private GameObject ready;
    [SerializeField]
    private GameObject notReady;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            scoreboard.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            scoreboard.SetActive(false);
        }
    }

    public void ReadyUI()
    {
        ready.SetActive(true);
        notReady.SetActive(false);
    }

    

    public void NotReadyUI()
    {
        ready.SetActive(false);
        notReady.SetActive(true);
    }
}
