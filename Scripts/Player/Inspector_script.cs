using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspector_script : Player
{
    [SerializeField]
    private int currentHealth;
    [SerializeField]
    private int healthMax = 100;
    [SerializeField]
    private LayerMask mask;

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = healthMax;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Click on left mouse button
        {
            GameObject Go = iClickOnv2();
            if (Go != null && (Go.layer == LayerMask.NameToLayer("RemotePlayer")|| (Go.layer == LayerMask.NameToLayer("Body"))))
            {
                Debug.Log("J'ai trouvé le joueur " +  Go.transform.parent.gameObject.transform.parent);
                //Debug.Log("layer : " + Go.layer);
            }
        }
    }

    private GameObject iClickOnv2()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, mask))
        {
            Debug.Log(hit.transform.name);
            return hit.collider.gameObject;
        }
        return null;
    }
}
