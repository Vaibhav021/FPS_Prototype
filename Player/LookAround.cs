using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour
{
    public static bool cursorLocked = true;

    public Transform player;
    public Transform cam;
    public Transform weapon;

    public float xSensitivity = 100f;
    public float ySensitivity = 100f;

    public float maxAngle = 60f;

    private Quaternion camCenter;

    // Start is called before the first frame update
    void Start()
    {
        camCenter = cam.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        SetYaxis();
        SetXaxis();
        CursorLocker();

    }

    public void SetYaxis()
    {
        float mouseInput = Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;
        Quaternion adj = Quaternion.AngleAxis(mouseInput, -Vector3.right);
        Quaternion delta = cam.localRotation * adj;

        if(Quaternion.Angle(camCenter, delta) < maxAngle)
        {
            cam.localRotation = delta;
        }

        weapon.rotation = cam.rotation;


    }
    
    public void SetXaxis()
    {
        float mouseInput = Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
        Quaternion adj = Quaternion.AngleAxis(mouseInput, Vector3.up);
        Quaternion delta = player.localRotation * adj;

        player.localRotation = delta;

    }

    public void CursorLocker()
    {
        if(cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if(Input.GetKey(KeyCode.Escape))
        {
            if (cursorLocked)
                cursorLocked = false;
            else
                cursorLocked = true;
        }



    }













}//class
