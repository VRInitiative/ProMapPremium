using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Headjack;
using TMPro;
public class Splash : MonoBehaviour
{
	public RectTransform scaleRect;
	public RawImage splashImage;
	public AspectRatioFitter aspectRatioFitter;
	public TextMeshProUGUI text;
	public RawImage progressBar;
	public GameObject cinemaListener;

	private bool alreadyGotTextures = true;
	private bool trackingOriginWasSet = false;

	void Start()
	{
		App.ShowCrosshair = false;
		VRInput.MotionControllerLaser = false;

		progressBar.material.SetFloat("_Progress", 0);
		splashImage.texture = Resources.Load<Texture2D>("Textures/Splash");
		//First load Headjack. This creates the VR Camera
		App.Initialize(OnHeadjackInitialized, true, true);
	}

	public static bool willShowError=false;
	public static bool willShowMessage=false;
	public static string messageToShow = null;

	void OnHeadjackInitialized(bool succes, string error)
	{
		cinemaListener.SetActive(true);
		// Now that there is a VR Camera, show the splash animation
		StartCoroutine(SplashAnimation(SplashAnimationType.Show));
		App.Fade(false, 0, null);

		//If initialization failed, show an error message and return
		if (!succes)
		{
			willShowError = true;
			if (error == "Unpublished")
			{
				messageToShow = "<size=150%>Cette application n'as pas été publié.</size>";
			}
			else
			{
				messageToShow= "<size=150%>Pas de connection internet.</size>\n\n" +"<size=50%>( "+error+" )</size>";
			}
			OnAllTexturesDownloaded(true, null);
			return;
		}
		else
		{
			if (error == "Offline")
			{
				text.text = "Préparation du mode hors-ligne.";
			}
			if(App.GetProjects().Length==0)
			{
				willShowError = true;
				messageToShow = "<size=150%>Pas de projets disponibles.</size>\n\nPour pouvoir utiliser cette application, veuillez créer un ou plusieurs projets sur votre espace Headjack.\nLes projets seront mis à jour à chaque redémarrage de l'application.";
			}
		}
		alreadyGotTextures = true;
		foreach (string s in App.GetProjects())
		{
			//Debug.Log(s + " " + App.GotMediaFile(App.GetProjectMetadata(s).ThumbnailId));
			//Debug.Log(s);
			alreadyGotTextures &= App.GotMediaFile(App.GetProjectMetadata(s).ThumbnailId);
		}


		EssentialsManager.variables = new EssentialsVariables();
		string disableControls="False";
		//string gazeMenu = "True";
		string backgroundStereo = "Monoscopic";

		CustomVariables.TryGetVariable("background_panorama", out EssentialsManager.variables.backgroundPano, null);
		if (!CustomVariables.TryGetVariable("background_stereo", out backgroundStereo, null)) backgroundStereo = "Monoscopic";
		CustomVariables.TryGetVariable("logo_on_top", out EssentialsManager.variables.logoOnTop, null);

		if (!CustomVariables.TryGetVariable("color_1", out EssentialsManager.variables.color1, null)) EssentialsManager.variables.color1 = new Color(204f/255f, 204f/255f, 204f/255f);
		if (!CustomVariables.TryGetVariable("color_2", out EssentialsManager.variables.color2, null)) EssentialsManager.variables.color2 = new Color(153f/255f, 217f/255f, 255f/255f);
		if (!CustomVariables.TryGetVariable("color_3", out EssentialsManager.variables.color3, null)) EssentialsManager.variables.color3 = new Color(208f/255f, 238f/255f, 255f/255f);

		if (!CustomVariables.TryGetVariable("disable_media_controls", out disableControls, null)) disableControls = "False";
		//if (!CustomVariables.TryGetVariable("kiosk_gaze", out gazeMenu, null)) gazeMenu = "True";
		if(disableControls!=null) EssentialsManager.variables.disableMediaControls = (disableControls.ToLower() == "true");
		//if (gazeMenu != null) EssentialsManager.variables.kioskGazeMenu = (gazeMenu.ToLower() == "true");
		if (backgroundStereo != null) EssentialsManager.variables.backgroundStereo = (backgroundStereo.ToLower() == "stereoscopic");

		StartCoroutine(DownloadMediaCoroutine());
	}

	private IEnumerator DownloadMediaCoroutine()
	{
		bool wait = false;
		if (!string.IsNullOrEmpty(EssentialsManager.variables.backgroundPano))
		{
      wait = true;
      App.DownLoadSingleTexture(EssentialsManager.variables.backgroundPano, delegate(bool s, string e)
			{
				wait = false;
			}, true, false);
		}
		while (wait) yield return null;
		
		if (!string.IsNullOrEmpty(EssentialsManager.variables.logoOnTop))
		{
      wait = true;
      App.DownLoadSingleTexture(EssentialsManager.variables.logoOnTop, delegate (bool s, string e)
			{
				wait = false;
			}, false, true);
		}
		while (wait) yield return null;
		//Download and decode all textures before entering main menu
		//If textures are already downloaded, the app will only decode
		//Add a progress listener to show percentage
		App.DownloadAllTextures(OnAllTexturesDownloaded, true, OnDownloadProgress);
	}

	void OnDownloadProgress(ImageLoadingState loadingState, float processed, float total)
	{
		string message = "";
		float progress01;
		int progressPercentage;
		switch (loadingState)
		{
			case ImageLoadingState.Downloading:
				message += "Téléchargement de l'environnement \n";
				progress01 = processed / Mathf.Max(total, Mathf.Epsilon); //< avoids division by 0
				progressPercentage = Mathf.RoundToInt(Mathf.Clamp(progress01 * 100, 0, 100));
				message += (progressPercentage/2).ToString() + " %";
				progressBar.material.SetFloat("_Progress", (progressPercentage / 2) * 0.01f);
				break;
			case ImageLoadingState.Decoding:
				message += "Préparation de l'environnement \n";
				progress01 = processed / Mathf.Max(total, Mathf.Epsilon); //< avoids division by 0
				progressPercentage = Mathf.RoundToInt(Mathf.Clamp(progress01 * 100, 0, 100));
				if (!alreadyGotTextures)
				{
					//message += (progressPercentage / 2 + 50).ToString() + " %";
					progressBar.material.SetFloat("_Progress", (progressPercentage / 2 + 50) * 0.01f);
				}
				else
				{
					//message += (progressPercentage).ToString() + " %";
					progressBar.material.SetFloat("_Progress", progressPercentage * 0.01f);
				}
				break;
		}
		
		//text.text = message;
	}

	void OnAllTexturesDownloaded(bool succes, string error)
	{
		//If there was an error, show an message but do continue to menu
		if (!succes)
		{
			App.ShowMessage(error);
		}
		App.Fade(true, 1f, delegate (bool s, string e)
		  {
			  UnityEngine.SceneManagement.SceneManager.LoadScene("Template/MainMenu");
		  });
	}

	public enum SplashAnimationType
	{
		Show,
		Hide
	}
	private IEnumerator SplashAnimation(SplashAnimationType animationType)
	{
		yield return null;
	}

	void Update() {
		if (!trackingOriginWasSet && App.camera != null && App.CameraParent != null) {
			trackingOriginWasSet = true;
			// set tracking origin to floor
			App.SetTrackingOrigin(App.TrackingOrigin.FloorLevel);
		}
 
    }

}
