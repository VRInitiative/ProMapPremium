using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinceGrab : MonoBehaviour
{
    public CraneController controller;
    Animation anim;

    private void Start()
    {
        anim = GetComponent<Animation>();
    }

    public void Release()
    {
        anim.Play("Hook_Release");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item")           
        {
            anim.Play("Hook_Catch");
            controller.GrabObject(other.gameObject);
        }
    }
}
