using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Headjack;
public class LaserPointerDot : MonoBehaviour
{
	public bool show=true;
	public Transform laserMesh;
	public Transform laserStart;
	public Transform laserDot;
	private RaycastHit hitInfo;

	void LateUpdate()
	{
		if (VRInput.MotionControllerLaser)
		{
			laserMesh.gameObject.SetActive(true);
			laserStart.gameObject.SetActive(true);
			laserMesh.transform.position = VRInput.LaserTransform.position;
                //new Vector3(0.30f, 2f, 2.30f);
                                           //VRInput.LaserTransform.position;
            laserMesh.transform.rotation = VRInput.LaserTransform.rotation;
			laserStart.transform.position = VRInput.LaserTransform.position;
            //new Vector3(0.30f, 2f, 2.30f);
            //VRInput.LaserTransform.position;
            laserStart.rotation = Quaternion.LookRotation(App.camera.forward, App.camera.up);
			VRInput.LaserTransform.gameObject.SetActive(false);
			laserMesh.rotation = Quaternion.LookRotation(laserMesh.forward, (App.camera.position - laserMesh.position).normalized);
			laserStart.rotation = Quaternion.LookRotation(App.camera.forward, App.camera.up);
			laserDot.position = App.LaserHit.point;
            laserDot.forward = App.camera.forward;
			laserDot.gameObject.SetActive(App.LaserHit.collider != null);
		}
		if(App.ShowCrosshair)
		{
			laserMesh.gameObject.SetActive(false);
			laserStart.gameObject.SetActive(false);
			Crosshair.crosshair.gameObject.SetActive(false);
			RaycastHit crosshairHit = App.CrosshairHit;
			laserDot.position = crosshairHit.point;
			laserDot.forward = App.camera.forward;
			laserDot.gameObject.SetActive(App.CrosshairHit.collider != null);
			
		}
		if (!App.ShowCrosshair && !VRInput.MotionControllerLaser)
		{
			laserDot.gameObject.SetActive(false);
			laserStart.gameObject.SetActive(false);
			laserMesh.gameObject.SetActive(false);
		}
	}

	void OnDisable() {
		VRInput.LaserTransform?.gameObject?.SetActive(true);
		Crosshair.crosshair?.gameObject?.SetActive(true);
	}
}