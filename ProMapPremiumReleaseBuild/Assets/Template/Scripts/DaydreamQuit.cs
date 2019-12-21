using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Headjack;

public class DaydreamQuit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
		if (App.CurrentPlatform == App.VRPlatform.Daydream && Input.GetKeyDown(KeyCode.Escape)) {
			Debug.Log("Quitting application due to Escape button pressed on Daydream");
			Application.Quit();
		}
	}
}
