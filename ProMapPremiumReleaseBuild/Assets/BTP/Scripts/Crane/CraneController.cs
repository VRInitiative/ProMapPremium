using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraneController : MonoBehaviour
{
    public GameObject MovingPart;
    private GameObject Rope, ItemGrabbed;
    private Vector3 ItemScale;

    public Transform Front, Back, Claw, Socle;

    public float RotationSpeed, ClawSpeed;
    private float HorizValue, VerticValue, CraneAxis, SocleAxis, RopeAxis;

    private bool Windows, Android;
    public bool Grabbing;

    void Start()
    {
        Grabbing = false;
        CreateRope();
        SwitchPlatform();
    }

    
    void Update()
    {
        //gestion des input si actuellement en editeur
        if (Windows)
        {
            CraneAxis = Input.GetAxis("Horizontal");
            //SocleAxis = Input.GetAxis("Vertical");
            RopeAxis = Input.GetAxis("Vertical");
        }

        //Gestion de la corde
        float RopeY = (Socle.position.y + Claw.position.y) / 2;
        Vector3 parentScale = Rope.transform.parent.localScale;
        Rope.transform.position = new Vector3(Rope.transform.position.x, RopeY, Rope.transform.position.z);
        Rope.transform.localScale = new Vector3(0.05f / parentScale.x, ((Socle.transform.position.y - Claw.transform.position.y) / 2)/parentScale.y, 0.05f / parentScale.z);

        //Relacher les objets attrapés
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) == 1 || OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) == 1 && Grabbing)
        {
            Grabbing = false;
            ItemGrabbed.GetComponent<Rigidbody>().useGravity = true;
            ItemGrabbed.transform.parent = null;
            ItemGrabbed.transform.localScale = ItemScale;
            ItemGrabbed = null;
            OVRInput.SetControllerVibration(.5f, 0.3f, OVRInput.Controller.RTouch);
            OVRInput.SetControllerVibration(.5f, 0.3f, OVRInput.Controller.LTouch);
            Claw.GetComponent<PinceGrab>().Release();
        }

        //Garde le scale de l'objet attrapé 
        if (ItemGrabbed != null)
        {
            ItemGrabbed.transform.position = new Vector3(Claw.transform.position.x, Claw.transform.position.y - 1.5f, Claw.transform.position.z);
        }

        RotateCrane();
        MoveSocle();
        MoveRope();
        ManageHaptics();
    }
    //Recupere les valeurs des manettes
    public void ManageAxis(float axisX, float axisZ, string name)
    {
        if (name == "ManetteD")
        {
            SocleAxis = axisX;
        }

        if (name == "ManetteG" && Mathf.Abs(axisX) > 0.3f)
        {
            RopeAxis = -axisX;
        }
        else
            RopeAxis = 0;

        if (name == "ManetteG" && Mathf.Abs(axisZ) > 0.3f)
        {
            CraneAxis = -axisZ;
        }
        else
            CraneAxis = 0;
    }

    void ManageHaptics()
    {
        if (Socle.transform.localPosition.z > 16.95f || Socle.transform.localPosition.z < 5.05f)
        {
            OVRInput.SetControllerVibration(.5f, 0.3f, OVRInput.Controller.RTouch);
        }
        else
        {
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        }

        if (Claw.transform.position.y > 24.95f)
        {
            OVRInput.SetControllerVibration(.5f, 0.3f, OVRInput.Controller.LTouch);
        }
        else
        {
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        }
    }

    //Rotation de la partie supérieur
    void RotateCrane ()
    {
        HorizValue = CraneAxis * RotationSpeed * Time.deltaTime;
        MovingPart.transform.Rotate(new Vector3(0, HorizValue, 0));
    }

    //Déplacement du socle  
    void MoveSocle ()
    {
        VerticValue = SocleAxis * Time.deltaTime * ClawSpeed;
        //Socle.transform.Translate(VerticValue, 0, 0, Space.Self);
        Socle.transform.localPosition = new Vector3(Mathf.Clamp(Socle.transform.localPosition.x,-1.0f,-0.5f), Socle.transform.localPosition.y, Mathf.Clamp(Socle.transform.localPosition.z + VerticValue, 5,17));
    }

    //Déplacement de la corde
    void MoveRope ()
    {
        float ropeSpeed = ClawSpeed * Time.deltaTime * RopeAxis;
        Claw.transform.position = new Vector3(Claw.transform.position.x, Mathf.Clamp(Claw.transform.position.y + ropeSpeed, 2, 25), Claw.transform.position.z);
    }

    //Création de la corde
    void CreateRope()
    {
        Rope = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        Rope.GetComponent<CapsuleCollider>().enabled = false;
        Rope.transform.parent = Socle.transform;
        Rope.transform.localScale = new Vector3(0.05f / 100, Rope.transform.localScale.y / 100, 0.05f / 100);
        Rope.transform.position = new Vector3(Socle.position.x, Socle.position.y - Rope.transform.localScale.y, Socle.position.z);
    }

    //Detecte la plateforme
    void SwitchPlatform()
    {
        if(Application.platform  == RuntimePlatform.Android)
        {
            Windows = false;
            Android = true;
        }
        if(Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Windows = true;
            Android = false;
        }
    }

    //Attraper un objet
    public void GrabObject(GameObject go)
    {
        if (!Grabbing)
        {
            Grabbing = true;
            ItemScale = go.transform.lossyScale;
            go.GetComponent<Rigidbody>().useGravity = false;
            ItemGrabbed = go.gameObject;
            OVRInput.SetControllerVibration(.5f, 0.3f, OVRInput.Controller.RTouch);
            OVRInput.SetControllerVibration(.5f, 0.3f, OVRInput.Controller.LTouch);
        }
    }
    
}
