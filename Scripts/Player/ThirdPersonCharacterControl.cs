using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ThirdPersonCharacterControl : MonoBehaviour
{
    [SerializeField]
    public float Speed;

    private void Start()
    {
        Speed = 5f;
    }

    private void FixedUpdate() //délai entre chaque appel de cette fonction est fixe et ne dépend pas du nombre d'image 
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        float jump = Input.GetAxis("Jump");

        Vector3 direction = new Vector3(hor, jump, ver).normalized;

        //Debug.Log("hor : " + hor);
        //Debug.Log("ver : " + ver);
        //Debug.Log("magnitude : " + direction.magnitude);
        //Debug.Log("direction.magnitude > 0.3f : " + (direction.magnitude > 0.3f));
        

        if (direction.magnitude > 0.3f)
        {
            direction *= Speed * Time.fixedDeltaTime;
            transform.Translate(direction, Space.Self);
        }
    }
}