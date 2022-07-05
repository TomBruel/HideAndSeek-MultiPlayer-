using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Zoom();
    }
    private void Zoom()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0 && transform.rotation.x > 0.3f)
        {
            //cameraOffset.y -= .4f;
            //cameraOffset.z += .3f;
            transform.Rotate(-2, 0, 0);
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0 && transform.rotation.x < 0.5f)
        {
            //cameraOffset.y += .4f;
            //cameraOffset.z -= .3f;
            transform.Rotate(+2, 0, 0);
        }
    }
}
