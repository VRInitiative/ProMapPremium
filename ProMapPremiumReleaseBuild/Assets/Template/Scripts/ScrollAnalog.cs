﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Headjack;
public class ScrollAnalog : MonoBehaviour {
	public bool horizontal, vertical;
	public Transform top, bottom;
	private float previousVer, previousHor;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//if (VRInput.Confirm.Pressed) Debug.Log(VRInput.Confirm.Pressed);
		//if (VRInput.Confirm.Hold) Debug.Log(VRInput.Confirm.Hold);
		bool swipe = false;
		float hor = 0;
		hor = CheckAxis(hor, "Horizontal");
		hor = CheckAxis(hor, "Oculus_CrossPlatform_PrimaryThumbstickHorizontal");
		hor = CheckAxis(hor, "Oculus_CrossPlatform_SecondaryThumbstickHorizontal");
		float ver = 0;
		ver = CheckAxis(ver, "Vertical");
		ver = CheckAxis(ver, "Oculus_CrossPlatform_PrimaryThumbstickVertical");
		ver = CheckAxis(ver, "Oculus_CrossPlatform_SecondaryThumbstickVertical");
		if (App.CurrentPlatform == App.VRPlatform.Oculus) {
			Vector2 f = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
			if (f.x != 0) hor = f.x;
			if (f.y != 0) ver = f.y;
			f = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
			if (f.x != 0) hor = f.x;
			if (f.y != 0) ver = f.y;

			OVRInput.Controller con = OVRInput.GetActiveController();
			if (con == OVRInput.Controller.RTrackedRemote || con == OVRInput.Controller.LTrackedRemote) swipe = true;
			//Debug.Log($"Swipe {swipe}");
		} else if (App.CurrentPlatform == App.VRPlatform.Daydream) {
			swipe = true;
			//Debug.Log("Daydream");
			if (GvrControllerInput.State == GvrConnectionState.Connected)
			{
				//Debug.Log("connected");
				if (GvrControllerInput.IsTouching)
				{
					
					Vector2 f = GvrControllerInput.TouchPos;
					//Debug.Log($"touching {f}");
					if (f.x != 0) hor = f.x*2f;
					if (f.y != 0) ver = -f.y*2f;
				}
				else
				{
					//Debug.Log($"not touching {GvrControllerInput.TouchPos}");
					hor = 0;
					ver = 0;
				}
			} else {
				hor = 0;
				ver = 0;
			}
		} else if (App.CurrentPlatform == App.VRPlatform.OpenVR) {
			swipe = false;
		} else if (App.CurrentPlatform == App.VRPlatform.ViveWave) {
			swipe = true;
			WaveVR_Controller.Device left = WaveVR_Controller.Input(wvr.WVR_DeviceType.WVR_DeviceType_Controller_Left);
			WaveVR_Controller.Device right = WaveVR_Controller.Input(wvr.WVR_DeviceType.WVR_DeviceType_Controller_Right);
			Vector2 v = left.GetAxis(wvr.WVR_InputId.WVR_InputId_Alias1_Touchpad);
			if (!left.GetTouch(wvr.WVR_InputId.WVR_InputId_Alias1_Touchpad)) v = Vector2.zero;
			if (v == Vector2.zero)
			{
				v = right.GetAxis(wvr.WVR_InputId.WVR_InputId_Alias1_Touchpad);
				if (!right.GetTouch(wvr.WVR_InputId.WVR_InputId_Alias1_Touchpad)) v = Vector2.zero;
			}
			hor = v.x;
			ver = v.y;
		} else if (App.CurrentPlatform == App.VRPlatform.Pico) {
			swipe = true;

			if (PicoVRHeadjack.instance != null && PicoVRHeadjack.instance.mainController != null && PicoVRHeadjack.instance.mainController.isTouching) {
				Vector2 touchPos = PicoVRHeadjack.instance.mainController.touchPadPosition;
				hor = 1.5f * (touchPos.y - 127.5f) / 127.5f;
				ver = 1.5f * (touchPos.x - 127.5f) / 127.5f;
			} else {
				hor = 0;
				ver = 0;
			}
		}

		if (Input.GetKey(KeyCode.UpArrow)) ver = 1;
		if (Input.GetKey(KeyCode.DownArrow)) ver = -1;
		if (Input.GetKey(KeyCode.RightArrow)) hor = 1;
		if (Input.GetKey(KeyCode.LeftArrow)) hor = -1;
		//Debug.Log(hor + " - " + ver);
		if (vertical)
		{
			Vector3 temp = transform.position;
			if (!swipe)
			{
				temp.y -= (ver * Time.deltaTime);
			}
			else
			{
				if (previousVer != 0 && ver != 0)
				{
					temp.y -= (previousVer - ver)*0.3f;
				}
			}
			transform.position = temp;
		}
		if (horizontal)
		{
			if (top != null && bottom != null)
			{
				RaycastHit hit = VRInput.MotionControllerAvailable ? App.LaserHit : App.CrosshairHit;
				if (hit.point.y < bottom.position.y || hit.point.y > top.position.y)
				{
					return;
				}
			}
			Vector3 temp = transform.position;
			if (!swipe)
			{
				temp.x -= (hor * Time.deltaTime);
			}
			else
			{
				if (previousHor != 0 && hor != 0)
				{
					temp.x -= (previousHor - hor) * 0.3f;
				}
			}

			transform.position = temp;
		}
		previousVer = ver;
		previousHor = hor;
	}
	
	private float CheckAxis(float current, string axis)
	{
		float f = Input.GetAxis(axis);
		//Debug.Log(f + " - " + axis);
		if (f != 0) return f;
		return current;
	}
}
