using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{

    public float intensity;
    public float smooth;

    private Quaternion origin_rotation;

    // Start is called before the first frame update
    void Start()
    {
        origin_rotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        float mouse_X = Input.GetAxis("Mouse X");
        float mouse_Y = Input.GetAxis("Mouse Y");

        Quaternion x_adj = Quaternion.AngleAxis(intensity * mouse_X, -Vector3.up);
        Quaternion y_adj = Quaternion.AngleAxis(intensity * mouse_Y, Vector3.right);
        Quaternion target_rotation = origin_rotation * x_adj * y_adj;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, target_rotation, Time.deltaTime * smooth);


    }

















}//class
