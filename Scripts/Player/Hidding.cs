using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Hidding : NetworkBehaviour
{
    [SerializeField]
    private LayerMask mask;
    [SerializeField]
    Player scriptPlayer;
    //[SerializeField]
    //Camera cam;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Click on left mouse button
        {
            GameObject Go = iClickOn();
            if (Go != null)
            {
                takeTheFormOf(Go); // if i don't click on nothing and not on Terrain -> i copy the gameObject
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            //Debug.LogError(" Etat de Ready : " + scriptPlayer.isReady);
            if (scriptPlayer.isReady == false)
            {
                ReadyUI();
            }
            else
            {
                NotReadyUI();
            }
            //Debug.LogError(" Etat de Ready : " + scriptPlayer.isReady);
        }
    }

    private GameObject iClickOn()
    {
        RaycastHit hit;
        if (Physics.Raycast(scriptPlayer.hiddingCam.transform.position, scriptPlayer.hiddingCam.transform.forward, out hit, 20, mask))
        {
            //Debug.Log("Objet touché : " + hit.collider.name);
            return hit.collider.gameObject;
        }
        return null;
    }

    [Client]
    private void takeTheFormOf(GameObject go)
    {
        /*GameObject parent = null;
        if (eye1 != null) Destroy(eye1);
        if (eye2 != null) Destroy(eye2);
        if (body != null)
        {
            parent = body.transform.parent.gameObject;
            Destroy(body);
        }
        GameObject newGo;
        newGo = Instantiate(go, transform.position + new Vector3(0f, go.transform.localScale.y/2f, 0f), parent.transform.rotation, parent.transform);
        newGo.name = "Body";
        newGo.layer = LayerMask.NameToLayer("Body");
        body = newGo;*/
        CmdHasBeenMetamorphosed(gameObject.name, go.name);
    }

    [Command]
    private void CmdHasBeenMetamorphosed(string playerId, string NameGo)
    {
        if(NameGo == null)
        {
            Debug.Log("NameGo est nul");
        }
        Debug.Log("le joueur " + playerId + " s'est métamorphosé !");
        Player player = GameManager.GetPlayer(playerId);
        player.HasBeenMetamorphosed(NameGo);
    }

    [Client]
    public void ReadyUI()
    {
        scriptPlayer.ready.SetActive(true);
        scriptPlayer.notReady.SetActive(false);
        scriptPlayer.isReady = true;
        //CmdHasBeenReady(gameObject.name);
    }

    [Client]
    public void NotReadyUI()
    {
        scriptPlayer.ready.SetActive(false);
        scriptPlayer.notReady.SetActive(true);
        scriptPlayer.isReady = false;
        //CmdHasBeenNotReady(gameObject.name);
    }

    public void DisableReadyUI()
    {
        scriptPlayer.ready.SetActive(false);
        scriptPlayer.notReady.SetActive(false);
        scriptPlayer.isReady = false;
    }

}
