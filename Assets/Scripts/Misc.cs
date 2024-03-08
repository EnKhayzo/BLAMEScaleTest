using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Misc : MonoBehaviour
{
    GameObject pl;

    float oldSpeed = 0f;
    GameObject speed;
    Text speedt;
    Color textColor = new Color(1,1,1,2);

    GameObject universeSphere;
    
    GameObject helperUI;

    GameObject[] rotationTexts;

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log(Screen.currentResolution.refreshRate);
        Application.targetFrameRate = Screen.currentResolution.refreshRate; 

        speed = GameObject.FindGameObjectWithTag("Speed");
        pl = GameObject.FindGameObjectWithTag("Player");
        speedt = speed.GetComponent(typeof(Text)) as Text;

        helperUI = GameObject.FindGameObjectWithTag("HelperUI");
        rotationTexts = GameObject.FindGameObjectsWithTag("RotationText");

        universeSphere = GameObject.Find("Universe Sphere");
        universeSphere.SetActive(false);
    }

    //converts the value to string and if it's big adds K,M,B,TR,QU,QI,SE,SEP,OCT,NON,DEC...
    string toStrScaled(float val) {
        if (val == 0f) return "0";

        string res;

        int count = 0;
        while (System.Math.Abs(val) > 1e03) { count++; val /= 1e03f; }

        res = Convert.ToString(val);
        res = res.Substring(0, System.Math.Min(res.Length, 6));
        switch (count) {
            case 1: res += "K"; break;
            case 2: res += "M"; break;
            case 3: res += "B"; break;
            case 4: res += "TR"; break;
            case 5: res += "QU"; break;
            case 6: res += "QI"; break;
            case 7: res += "SE"; break;
            case 8: res += "SEP"; break;
            case 9: res += "OCT"; break;
            case 10: res += "NON"; break;
            case 11: res += "DEC"; break;
        }

        int dcount = 0;
        while(System.Math.Abs(val) < 1) { val *= 1e03f; dcount++; }
        if(dcount > 0) {
            res = Convert.ToString(val);
            res = res.Substring(0, System.Math.Min(res.Length, 6));
        }

        switch (dcount) {
            case 1: res += "m"; break;
            case 2: res += "µ"; break;
            case 3: res += "n"; break;
            case 4: res += "f"; break;
            case 5: res += "a"; break;
            case 6: res += "z"; break;
            case 7: res += "y"; break;
        }

        return res;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = ((CameraControl)pl.GetComponent(typeof(CameraControl))).speed;

        if(!Input.GetKey(KeyCode.LeftShift) && speed != oldSpeed){
            speedt.text = "Speed: " + toStrScaled(speed*10f) + "m/s";
            textColor = new Color(1, 1, 1, 2); 
        }

        oldSpeed = speed;

        if(textColor.a > 0) {
            textColor = new Color(textColor.r, textColor.g, textColor.b, textColor.a-1f*Time.deltaTime); 
            speedt.color = textColor;
        }


        if(Input.GetKeyDown(KeyCode.H))
            helperUI.SetActive(!helperUI.activeSelf);

        if(Input.GetKeyDown(KeyCode.T)){
            foreach (GameObject text in rotationTexts){
                text.SetActive(!text.activeSelf);
            }
        }

        if (Input.GetKeyDown(KeyCode.V)){
            universeSphere.SetActive(!universeSphere.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            CameraControl cm = ((CameraControl)pl.GetComponent(typeof(CameraControl)));
            PlayerPrefs.SetFloat("mouseSensitivity", cm.mouseSensitivity);
            //PlayerPrefs.SetFloat("speed", cm.speed);

            PlayerPrefs.SetFloat("fov", ((Camera)GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent(typeof(Camera))).fieldOfView);

            Application.Quit();
        }
    }
}
