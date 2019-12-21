using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Headjack;
using TMPro;
public class ProjectInstance : MonoBehaviour
{
	public string id;
	private Texture texture;
	public RawImage rawImage;
	private EssentialsManager essentialsManager;
	public TextMeshProUGUI title, duration;
	private App.ProjectMetadata projectMetadata;
	private App.VideoMetadata videoMetadata;
	public void Load(string id, EssentialsManager essentialsManager)
	{
		//Debug.Log("Loading " + id);
		this.id = id;
		gameObject.name = id;
		texture = App.GetImage(id, false);
		if (texture != null)
		{
			texture.filterMode = FilterMode.Trilinear;
			rawImage.texture = texture;
		}
		this.essentialsManager = essentialsManager;
		projectMetadata = App.GetProjectMetadata(id);
		videoMetadata = App.GetVideoMetadata(projectMetadata.VideoId);
		if (projectMetadata != null) {
			title.text = projectMetadata.Title;
		} else {
			title.text = "Unknown project";
		}
		if (videoMetadata != null) {
			duration.text = videoMetadata.DurationMMSS;
		} else {
			duration.text = "<color=red>no video</color>";
		}
	}
	public void Show()
	{
		essentialsManager.ShowProject(id);
	}
}
