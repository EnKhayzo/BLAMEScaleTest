using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public float speed = 4f;

    public float mouseSensitivity = 2f;

    public GameObject[] mainCameras;

    void setCamerasFov(float fov){
        foreach(GameObject camera in mainCameras){
            ((Camera)camera.GetComponent(typeof(Camera))).fieldOfView = fov;
        }
    }

    void changeCamerasFov(float multDelta){
        foreach(GameObject camera in mainCameras){
            ((Camera)camera.GetComponent(typeof(Camera))).fieldOfView *= multDelta;
        }
    }

    void toggleCursorLock() {
        Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
        if(Cursor.lockState == CursorLockMode.Locked)   Cursor.visible = false;
        else                                            Cursor.visible = true;
    }

    bool cursorLocked() { return Cursor.lockState == CursorLockMode.Locked; }

    // Start is called before the first frame update
    void Start()
    {
        mainCameras = GameObject.FindGameObjectsWithTag("MainCamera");

        mouseSensitivity = PlayerPrefs.GetFloat("mouseSensitivity", 2f);
        //speed = PlayerPrefs.GetFloat("speed", 4f);
        setCamerasFov(PlayerPrefs.GetFloat("fov", 60f));
    }

    int toi(bool b) { return Convert.ToInt32(b); }

    // Update is called once per frame
    void Update()
    {
        Vector3 axis = new Vector3(
            toi(Input.GetKey(KeyCode.D))        -       toi(Input.GetKey(KeyCode.A)),
            toi(Input.GetKey(KeyCode.Space))    -       toi(Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.LeftControl)),
            toi(Input.GetKey(KeyCode.W))        -       toi(Input.GetKey(KeyCode.S))
       );

        transform.position +=   (Camera.main.transform.right   * axis.x + 
                                Camera.main.transform.up      * axis.y+
                                Camera.main.transform.forward * axis.z) * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Z)) toggleCursorLock();
        
        if(Input.GetKey(KeyCode.LeftShift)) {
            if(Input.mouseScrollDelta.y > 0) mouseSensitivity *= 2f;
            if(Input.mouseScrollDelta.y < 0) mouseSensitivity /= 2f;
        }
        else if(Input.GetKey(KeyCode.LeftAlt)){
            const float fraction = 1f/8f;
            if(Input.mouseScrollDelta.y > 0) changeCamerasFov(1f - fraction);
            if(Input.mouseScrollDelta.y < 0) changeCamerasFov(1f + fraction);
        }
        else{
            if(Input.mouseScrollDelta.y > 0) speed *= 2f;
            if(Input.mouseScrollDelta.y < 0) speed /= 2f;
        }

        Vector3 mAxis = new Vector3(
            -Input.GetAxis("Mouse Y") * mouseSensitivity,
            Input.GetAxis("Mouse X") * mouseSensitivity,
            toi(Input.GetKey(KeyCode.Q)) - toi(Input.GetKey(KeyCode.E))
        ) * 100.0f * Time.deltaTime;

        if(cursorLocked()) transform.rotation *= Quaternion.Euler(mAxis);
    }
}
