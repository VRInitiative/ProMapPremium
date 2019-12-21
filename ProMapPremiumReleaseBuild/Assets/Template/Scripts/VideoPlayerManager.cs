﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Headjack;
using UnityEngine.SceneManagement;
public class VideoPlayerManager : MonoBehaviour
{
	private static VideoPlayerManager _instance;
	public OnEnd onVideoEnd;
	public int kioskCurrentVideo = 0;
	public bool isKiosk;
	public bool isStream;
	public bool resetOnHmdPause = false;
	private float HMDOffTime = 0;
	private float onPauseTime = 0;
	public string[] projects; //kiosk playlist

	// store delegates for HMD mount/unmount actions so they can be removed after use
	private System.Action hmdUnmount = null;
	private System.Action hmdMount = null;

	public static VideoPlayerManager Instance
	{
		get
		{
			return _instance;
		}
	}
	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			gameObject.SetActive(false);
		}
	}

	public void Initialize(string projectId, bool stream, OnEnd onVideoEnd, bool resetOnPause = false, bool prepareOnly = false, long videoTime = 0)
	{
		this.onVideoEnd += onVideoEnd;
		Debug.Log("Playing project " + projectId);
		isKiosk = false;
		isStream = stream;
		resetOnHmdPause = resetOnPause;
		kioskCurrentVideo = 0;
		SceneManager.LoadScene("Template/VideoPlayer", LoadSceneMode.Single);
		App.Fade(false, 1f, delegate (bool s, string e)
		  {
			  if (prepareOnly) {
			  	App.Prepare(projectId, stream, false, OnVideoEnd, videoTime);
			  } else {
			  	App.Play(projectId, stream, false, OnVideoEnd);
			  }
			  QualitySettings.antiAliasing = 0;
			  Debug.Log("AA to 0");
		  });

		if (App.CurrentPlatform == App.VRPlatform.Oculus)
		{
			if (hmdUnmount != null) {
				OVRManager.HMDUnmounted -= hmdUnmount;
				hmdUnmount = null;
			}
			if (hmdMount != null) {
				OVRManager.HMDMounted -= hmdMount;
				hmdMount = null;
			}

			if (resetOnPause) {
				hmdUnmount = delegate () {
					if (App.Player == null) return;
					Debug.Log("OVR headset unmounted");
					HMDOffTime = Time.realtimeSinceStartup;
				};

				OVRManager.HMDUnmounted += hmdUnmount;
				hmdMount = delegate () {
					if (App.Player == null) return;
					Debug.Log("OVR headset mounted");
					if (Time.realtimeSinceStartup - HMDOffTime > 4f)
					{
						if (!isKiosk)
						{
							StopVideo();
						}
						else
						{
							App.DestroyVideoPlayer();
							App.Fade(true, 1f, delegate (bool s2, string e2)
							{
								App.Fade(false, 0f, null);
								kioskCurrentVideo = 0;
								PlayNextKiosk();
							});
						}

					}
					HMDOffTime = -1f;
				};
				OVRManager.HMDMounted += hmdMount;
			}
		}
	}

	public void Initialize(bool resetOnPause = false)
	{
		Initialize(App.GetProjects(), resetOnPause);
	}

	public void Initialize(string[] playlist, bool resetOnPause = false) //Kiosk mode if there are no parameters
	{
		isKiosk = true;
		resetOnHmdPause = resetOnPause;
		projects = playlist;
		SceneManager.LoadScene("Template/VideoPlayer", LoadSceneMode.Single);
		App.Fade(false, 1f, delegate (bool s, string e)
		{
			QualitySettings.antiAliasing = 0;
			Debug.Log("AA to 0");
			PlayNextKiosk();
		});


		//5 second thing
		if (App.CurrentPlatform == App.VRPlatform.Oculus)
		{
			if (hmdUnmount != null) {
				OVRManager.HMDUnmounted -= hmdUnmount;
				hmdUnmount = null;
			}
			if (hmdMount != null) {
				OVRManager.HMDMounted -= hmdMount;
				hmdMount = null;
			}

			if (resetOnPause) {
				hmdUnmount = delegate () {
					if (App.Player == null) return;
					Debug.Log("OVR headset unmounted");
					HMDOffTime = Time.realtimeSinceStartup;
				};
				OVRManager.HMDUnmounted += hmdUnmount;

				hmdMount = delegate () {
					if (App.Player == null) return;
					Debug.Log("OVR headset mounted");
					if (Time.realtimeSinceStartup - HMDOffTime > 4f)
					{
						if (!Instance.isKiosk)
						{
							StopVideo();
						}
						else
						{
							App.DestroyVideoPlayer();
							App.Fade(true, 1f, delegate (bool s2, string e2)
							{
								App.Fade(false, 0f, null);
								kioskCurrentVideo = 0;
								PlayNextKiosk();
							});
						}

					}
					HMDOffTime = -1f;
				};
				OVRManager.HMDMounted += hmdMount;
			}
		}
	}
	private void OnApplicationPause(bool pause)
	{
		if (App.Player == null) return;
		if (App.CurrentPlatform != App.VRPlatform.Oculus && resetOnHmdPause)
		{
			if (pause)
			{
				Debug.Log("headset unmounted");
				onPauseTime = Time.realtimeSinceStartup;
			}
			else
			{
				Debug.Log("headset mounted");
				if (Time.realtimeSinceStartup - onPauseTime > 4f)
				{
					if (!Instance.isKiosk)
					{
						StopVideo();
					}
					else
					{
						App.DestroyVideoPlayer();
						App.Fade(true, 1f, delegate (bool s2, string e2)
						{
							App.Fade(false, 0f, null);
							kioskCurrentVideo = 0;
							PlayNextKiosk();
						});
					}
				}
				onPauseTime = -1f;
			}
		}
	}

	public void PlayNextKiosk(bool increment=true)
	{
		Debug.Log("KIOSK Play");
		
		kioskCurrentVideo = (int)Mathf.Repeat(kioskCurrentVideo, projects.Length);
		Debug.Log("Current kiosk index: " + kioskCurrentVideo);
		Debug.Log("Current kiosk project: " + projects[kioskCurrentVideo]);
		isStream = false;
		App.Play(projects[kioskCurrentVideo], false, false, delegate (bool s, string e)
		{
			if(increment) kioskCurrentVideo++;
			App.DestroyVideoPlayer();
			App.Fade(true, 1f, delegate (bool s2, string e2)
			 {
				 App.Fade(false, 0f, null);
				 PlayNextKiosk();
			 });
			
		});
	}

	public void StopVideo()
	{
		OnVideoEnd(true,null);
	}

	public void OnVideoEnd(bool succes, string error)
	{
		Debug.Log("OnVideoEnd called");
		App.Fade(true, 1f, delegate (bool s, string e)
		  {
			  if (App.CurrentPlatform == App.VRPlatform.Oculus) {
			  	if (hmdUnmount != null) {
					OVRManager.HMDUnmounted -= hmdUnmount;
					hmdUnmount = null;
				}
				if (hmdMount != null) {
					OVRManager.HMDMounted -= hmdMount;
					hmdMount = null;
				}
			  }

			  SceneManager.LoadScene("Template/MainMenu", LoadSceneMode.Single);
			  App.Fade(false, 1f, null);
			  onVideoEnd?.Invoke(succes, error);
			  onVideoEnd = null;
		  });
	}
}
