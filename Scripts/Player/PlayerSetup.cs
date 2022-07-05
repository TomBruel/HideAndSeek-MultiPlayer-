using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;
    [SerializeField]
    private Camera sceneCamera;
    [SerializeField]
    private string remoteLayerName = "RemotePlayer";
    [SerializeField]
    private string localPlayerLayerName = "LocalPlayer";
    [SerializeField]
    private const string playerIdPrefix = "Player";

    private void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
            
        }
        else //si c'est notre joueur au moment ou il arrive ont ...
        {
            AssignLocalPlayerLayer();
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        string netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();
        GameManager.RegisterPlayer(netID, player);
    }

    private void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }    
    
    private void AssignLocalPlayerLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(localPlayerLayerName);
    }
    
    private void DisableComponents()
    {
        //disable components if the player is the local player
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    public void OnDisable() // quand le joueur est désactivé  (quitte le serveur)
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
        GameManager.UnregisterPlayer(transform.name);
    }
}
