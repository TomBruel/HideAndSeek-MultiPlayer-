using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectorControl : Player
{

    [SerializeField]
    private float SpeedMove = 10f;
    [SerializeField]
    private float minFOV = 15f;
    [SerializeField]
    private float maxFOV = 90f;
    [SerializeField]
    private float SensitivityZoom = 30f;
    [SerializeField]
    private float FOV;



    private void Update()
{
        PlayerMovement();
        PlayerZoomMovement();
    }

    private void PlayerZoomMovement()
    {
        FOV = inspectorCam.fieldOfView;
        FOV += (Input.GetAxis("Mouse ScrollWheel") * SensitivityZoom) * -1;
        FOV = Mathf.Clamp(FOV, minFOV, maxFOV);
        inspectorCam.fieldOfView = FOV;
    }

    private void PlayerMovement()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(hor, 0f, ver).normalized;
        if(direction.magnitude >0.1f)
        {
            direction *= SpeedMove/6 * Time.fixedDeltaTime;
            transform.Translate(direction, Space.Self);
        }
        else
        {
            if (Input.mousePosition.y > Screen.height - 10)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * SpeedMove, Space.World);
            }
            if (Input.mousePosition.y < 10)
            {
                transform.Translate(Vector3.back * Time.deltaTime * SpeedMove, Space.World);
            }
            if (Input.mousePosition.x < 10)
            {
                transform.Translate(Vector3.left * Time.deltaTime * SpeedMove, Space.World);
            }
            if (Input.mousePosition.x > Screen.width - 10)
            {
                transform.Translate(Vector3.right * Time.deltaTime * SpeedMove, Space.World);
            }
        }
    }
}
