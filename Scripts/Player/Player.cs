using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    [SyncVar]
    public int score;
    [SyncVar]
    public int XTimesInspector;
    [SyncVar]
    public bool isReady;

    [SerializeField]
    protected bool isInspector = false;
    [SerializeField]
    protected Camera inspectorCam;
    [SerializeField]
    public Camera hiddingCam;
    [SerializeField]
    protected string playerName;
    [SerializeField]
    private InspectorControl myInspectorControlScript;
    [SerializeField]
    private ThirdPersonCharacterControl myThirdPersonCharacterControl;
    [SerializeField]
    private ThirdPersonCameraControl myThirdPersonCameraControl;
    [SerializeField]
    protected GameObject eye1;
    [SerializeField]
    protected GameObject eye2;
    [SerializeField]
    protected GameObject body;
    [SerializeField]
    private GameObject hidding;
    [SerializeField]
    private GameObject inspector;
    [SerializeField]
    Behaviour[] componentsHiddingToDisable;
    [SerializeField]
    Behaviour[] componentsInspectorToDisable;
    [SerializeField]
    private GameObject scoreboard;
    [SerializeField]
    public GameObject ready;
    [SerializeField]
    public GameObject notReady;

    private void Start()
    {
        isReady = false;
        playerName = gameObject.name;
        XTimesInspector = 0;
        //if(myPlayerUI!=null) myPlayerUI.NotReadyUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            scoreboard.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            scoreboard.SetActive(false);
        }
    }

    private void DisableHiddingComponents()
    {
        //disable components if the player is the local player
        for (int i = 0; i < componentsHiddingToDisable.Length; i++)
        {
            componentsHiddingToDisable[i].enabled = false;
        }
    }
    
    private void EnableHiddingComponents()
    {
        //disable components if the player is the local player
        for (int i = 0; i < componentsHiddingToDisable.Length; i++)
        {
            componentsHiddingToDisable[i].enabled = true;
        }
    }

    private void DisableInspectorComponents()
    {
        //disable components if the player is the local player
        for (int i = 0; i < componentsInspectorToDisable.Length; i++)
        {
            componentsInspectorToDisable[i].enabled = false;
        }
    }

    private void EnableInspectorComponents()
    {
        //disable components if the player is the local player
        for (int i = 0; i < componentsInspectorToDisable.Length; i++)
        {
            componentsInspectorToDisable[i].enabled = true;
        }
    }

    [Client]
    public void becomeInspector()
    {
        isInspector = true;
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        hidding.SetActive(false);
        inspector.SetActive(true);
        DisableHiddingComponents();
        EnableInspectorComponents();
        transform.rotation = Quaternion.identity;
        XTimesInspector++;
    }

    [Client]
    public void becomeHidding()
    {
        isInspector = false;
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        hidding.SetActive(true);
        inspector.SetActive(false);
        EnableHiddingComponents();
        DisableInspectorComponents();
    }

    public void IncreaseXTimesInspector()
    {
        XTimesInspector++;
    }

    public void inscreaseScore(int points)
    {
        score += points;
    }

    private GameObject GetChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }
    
    [ClientRpc]
    public void HasBeenMetamorphosed(string NameGo)
    {
        if(NameGo == null)
        {
            Debug.Log("L'objet est nul");
            return;
        }
        else
        {
            GameObject parent = GetChildWithName(gameObject, "Hidding");
            if (eye1 != null) Destroy(eye1);
            if (eye2 != null) Destroy(eye2);
            if (body != null) Destroy(body);
            GameObject newGo;
            GameObject tempGo;
            tempGo = Resources.Load(("SceneObjects/" + NameGo)) as GameObject;
            if(tempGo == null)
            {
                Debug.Log("L'objet est vraiment nul");
                return;
            }
            else
            {
                newGo = Instantiate(tempGo, transform.position + new Vector3(0f, tempGo.transform.localScale.y/2f, 0f), parent.transform.rotation, parent.transform);
                newGo.name = "Body";
                newGo.layer = LayerMask.NameToLayer("Body");
                body = newGo;
            }
        }
    }

}


