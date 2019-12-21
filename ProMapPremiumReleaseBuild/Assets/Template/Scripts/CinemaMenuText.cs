using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Headjack;
using UnityEngine.UI;
public class CinemaMenuText : MonoBehaviour {
	public TextMeshProUGUI message, deviceName, sizeText, projectsText;
	private CinemaListenerExample cinemaListener;
	private CinemaHandler cinemaHandler;
	public GameObject progressObject;
	public RawImage bar;
	private Material barMaterial;

	private void OnEnable()
	{
		cinemaListener = CinemaListenerExample.instance;
		cinemaHandler = CinemaHandler.instance;

		barMaterial = bar.material;
	}

	void Update ()
	{
		if (cinemaHandler != null && cinemaHandler.IsConnected) {
			if (!string.IsNullOrEmpty(cinemaHandler.LatestMessage)) {
				// show custom message
				message.text = cinemaHandler.LatestMessage;
			} else {
				message.text = "Vous êtes connecté !\nVotre expérience est sur le point de commencer.";
			}

			// show given device name
			deviceName.text = $"<color=\"green\"><b>o</b></color> {cinemaHandler.DeviceName}";
			// show download progress
			List<string> downloadQueue = cinemaHandler.DownloadQueue;
			if (downloadQueue.Count > 0)
			{
				progressObject.SetActive(true);
				float progress = App.GetProjectProgress(downloadQueue[0]) * 0.01f;
				int total = (int)App.GetProjectMetadata(downloadQueue[0], App.ByteConversionType.Megabytes).TotalSize;
				sizeText.text = Mathf.RoundToInt(progress * total).ToString() + " / " + total.ToString() + " MB";
				projectsText.text = "Project 1 / " + downloadQueue.Count.ToString();
				barMaterial.SetFloat("_Progress", progress);
			}
			else
			{
				progressObject.SetActive(false);
			}
		} else if (cinemaListener != null) {
			if (!string.IsNullOrEmpty(cinemaListener.message)) {
				// show custom message
				message.text = cinemaListener.message;
			} else if (cinemaListener.cinemaConnected) {
				message.text = "Vous êtes connecté !\nVotre expérience est sur le point de commencer.";
			} else {
				message.text = $"<size=150%>Bienvenu(e) dans le mode Ciné-conférence</size>\n\nPour controler cet appareil,\ninstallez et lancez l'application Headjack Operator sur votre tablette\n\nou allez sur app.headjack.io/cinema";

            }
			// show given device name
			deviceName.text = "<color=\""+(cinemaListener.cinemaConnected?"green":"red")+"\"><b>o</b></color> "+ (cinemaListener.cinemaConnected?cinemaListener.deviceName:"Appareil(s) non connecté");
			// show download progress
			List<string> downloadQueue = cinemaListener.downloadQueue;
			if (downloadQueue.Count > 0)
			{
				progressObject.SetActive(true);
				float progress = App.GetProjectProgress(downloadQueue[0]) * 0.01f;
				int total = (int)App.GetProjectMetadata(downloadQueue[0], App.ByteConversionType.Megabytes).TotalSize;
				sizeText.text = Mathf.RoundToInt(progress * total).ToString() + " / " + total.ToString() + " MB";
				projectsText.text = "Project 1 / " + downloadQueue.Count.ToString();
				barMaterial.SetFloat("_Progress", progress);
			}
			else
			{
				progressObject.SetActive(false);
			}
		} else {
			// reset menu texts
			message.text = $"<size=150%>Bienvenu(e) dans le mode Ciné-conférence</size>\n\nPour controler cet appareil,\ninstallez et lancez l'application Headjack Operator sur votre tablette\n\nou allez sur app.headjack.io/cinema";
			deviceName.text = "<color=\"red\"><b>o</b></color> Appareil(s) non connecté";
			progressObject.SetActive(false);
		}
	}
}
