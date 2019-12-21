using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleGrab : MonoBehaviour
{
    OVRGrabbable grab;
    public CraneController craneCon;
    public bool isGrabbed;
    public float minAngle, maxAngle;
    Vector3 startPos;

    void Start()
    {
        grab = GetComponent<OVRGrabbable>();
        startPos = transform.localPosition;
    }


    void Update()
    {
        transform.localPosition = startPos;

        //Vérification de l'état de l'objet
        if (grab.grabbedBy != null)
        {
            isGrabbed = true;
        }
        else
            isGrabbed = false;
    
        if (isGrabbed)
        {
            float rotX = ClampAngle(transform.eulerAngles.x, minAngle, maxAngle);
            float rotZ = ClampAngle(transform.eulerAngles.z, minAngle, maxAngle);
            transform.eulerAngles = new Vector3(rotX, transform.eulerAngles.y, rotZ);
            GetHandleAxis(rotX, rotZ);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            GetHandleAxis(0, 0);
        }
    }

    //Convertit l'angle des manettes
    void GetHandleAxis(float axisX, float axisZ)
    {
        float Xvalue = Mathf.Sin(axisX * Mathf.Deg2Rad);
        float Zvalue = Mathf.Sin(axisZ * Mathf.Deg2Rad);
        craneCon.ManageAxis(Xvalue, Zvalue, gameObject.name);
    }

    //Clamp la rotation entre deux valeurs
    public static float ClampAngle (float angle, float min, float max)
    {
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + min);
        return Mathf.Min(angle, max);
    }   
}
