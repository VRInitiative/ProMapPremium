using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Headjack
{
	public class TrackedController : MonoBehaviour
	{
		public Controller controllerType;
		public List<Mapping> mapping;
		private GameObject controller, optionalLeft;
		private bool initialized;
		public enum Controller
		{
			None = -1,
			OculusCV = 0,
			HTCVive = 1,
			Wave = 2,
			OculusGo = 3,
			GearVR = 4
		}
		[System.Serializable]
		public class Mapping
		{
			public Controller controllerType;
			public GameObject gameObject, optionalLeftController;
		}
		public void Start()
		{
			controllerType = GetControllerType();
			foreach (Mapping m in mapping)
			{
				if (m.controllerType == this.controllerType)
				{
					controller = m.gameObject;
					optionalLeft = m.optionalLeftController;
				}
			}
			//Debug.Log("Controller mesh loaded: " + controller);
			if (controller == null || !VRInput.MotionControllerAvailable) this.gameObject.SetActive(false);
			initialized = true;
		}
		private void Update()
		{
			if (!initialized || controllerType == Controller.None) return;
			//if (optionalLeft == null)
			//{
				controller.SetActive(VRInput.MotionControllerShow );
            //}
            //else
            //{
            //	if (VRInput.remote.node == UnityEngine.XR.XRNode.LeftHand)
            //	{
            //		controller.SetActive(false);
            //		optionalLeft.SetActive(VRInput.MotionControllerShow);
            //	}
            //	else
            //	{
            //		controller.SetActive(VRInput.MotionControllerShow);
            //		optionalLeft.SetActive(false);
            //	}
            //}

            transform.position = VRInput.LaserTransform.position;
            //new Vector3(0.30f, 2f, 2.30f);
            //VRInput.LaserTransform.position;
            transform.rotation = VRInput.LaserTransform.rotation;
        }
		public Controller GetControllerType()
		{
			switch (App.CurrentPlatform)
			{
				case App.VRPlatform.Oculus:
					OVRPlugin.SystemHeadset headset = OVRPlugin.GetSystemHeadsetType();
					if (headset == OVRPlugin.SystemHeadset.GearVR_R320) return Controller.GearVR;
					if (headset == OVRPlugin.SystemHeadset.GearVR_R321) return Controller.GearVR;
					if (headset == OVRPlugin.SystemHeadset.GearVR_R322) return Controller.GearVR;
					if (headset == OVRPlugin.SystemHeadset.GearVR_R323) return Controller.GearVR;
					if (headset == OVRPlugin.SystemHeadset.GearVR_R324) return Controller.GearVR;
					if (headset == OVRPlugin.SystemHeadset.GearVR_R325) return Controller.GearVR;
					if (headset == OVRPlugin.SystemHeadset.Oculus_Go) return Controller.OculusGo;
					if (headset == OVRPlugin.SystemHeadset.Rift_CV1) return Controller.OculusCV;
					return Controller.None;
				case App.VRPlatform.OpenVR:
					return Controller.HTCVive;
				case App.VRPlatform.ViveWave:
					return Controller.Wave;
				default:
					return Controller.None;
			}
		}
		}
}