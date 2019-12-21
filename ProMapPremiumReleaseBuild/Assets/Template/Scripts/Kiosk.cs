using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Headjack;
using TMPro;
using UnityEngine.UI;
public class Kiosk : MonoBehaviour
{
	public bool canSelect = true;
	public EssentialsManager essentialsManager;
	public string[] projects;
	public List<string> projectsToDownload;
	public bool downloading = false;
	public int projectsFinished = 0;
	double totalSize = -1;
	public TextMeshProUGUI progressA, progressB, notReadyText;
	public Animator animator;
	public RawImage progressBar;
	public KioskSelect kioskSelect;
	public Dictionary<string, App.ProjectMetadata> projectMetadataCache;


	void Awake()
	{
		if (App.GetAppMetadata() == null) return;

		projects = App.GetProjects();
		projectsToDownload = new List<string>();
		projectMetadataCache = new Dictionary<string, App.ProjectMetadata>(projects.Length);
		if (projects != null) {
			for (int i = 0; i < projects.Length; ++i) {
				projectMetadataCache[projects[i]] = App.GetProjectMetadata(projects[i], App.ByteConversionType.Megabytes);
			}
		}
	}

	private void OnEnable()
	{
		if (projects == null) return;
		canSelect = essentialsManager.loadedVariables.kioskGazeMenu;
		animator.SetBool("KioskSelect", canSelect);
		if (!downloading)
		{
			animator.SetBool("Downloading", false);
			projectsToDownload.Clear();
			for (int i = 0; i < projects.Length; ++i)
			{
				if (!App.GotFiles(projects[i])&&!App.IsLiveStream(projects[i])) projectsToDownload.Add(projects[i]);
			}
			if (projectsToDownload.Count == 0)
			{
				animator.SetBool("GotFiles", true);
				if (canSelect)
				{
					App.ShowCrosshair = true;
					VRInput.MotionControllerLaser = false;
					CurvedCanvasInputModule.forceGaze = true;
				}
				
			}
			else
			{
				totalSize = 0;
				for (int i = 0; i < projectsToDownload.Count; ++i)
				{
					if (projectMetadataCache.ContainsKey(projectsToDownload[i]) && projectMetadataCache[projectsToDownload[i]] != null) {
						totalSize += projectMetadataCache[projectsToDownload[i]].TotalSize;
					} else {
						App.ProjectMetadata projectMeta = App.GetProjectMetadata(projectsToDownload[i], App.ByteConversionType.Megabytes);
						projectMetadataCache[projectsToDownload[i]] = projectMeta;
						totalSize += projectMeta.TotalSize;
					}
	
				}
				notReadyText.text = "Kiosk will be ready once all projects are downloaded\n(" + Mathf.RoundToInt((float)totalSize).ToString() + " MB)";
				animator.SetBool("GotFiles", false);
			}
		}
		else
		{
			animator.SetBool("Downloading", true);
		}
		}



	public void Download()
	{
		projectsFinished = 0;
		foreach (string s in projectsToDownload)
		{
			if (!App.IsLiveStream(s)) {
				essentialsManager.DownloadSubtitles(s);
				App.Download(s, false, delegate (bool downloadSuccess, string downloadError) {
					if (!downloadSuccess) {
						// TODO: display download error
					}
					projectsFinished++;
				});
			} 
		}
		downloading = true;
		animator.SetBool("Downloading", true);
	}

	public void Play()
	{
		App.Fade(true, 1f, delegate (bool s, string e)
		  {
			  VideoPlayerManager.Instance.Initialize(true);
		  });
	}

	public void Cancel()
	{
		PopupMessage.instance.Show("Are you sure you want to cancel this download ? ", PopupMessage.ButtonMode.YesNo, delegate (bool confirm)
		  {
			  if (confirm)
			  {
				  foreach (string s in projectsToDownload)
				  {
					  App.Cancel(s);
				  }
				  downloading = false;
				  OnEnable();
			  }
		  });

	}

	void Update()
	{
		if (downloading)
		{
			float progress=0;
			int total = 0;
			for (int i = 0; i < projectsToDownload.Count; ++i)
			{
				if (!App.GotFiles(projectsToDownload[i]))
				{
					progress = App.GetProjectProgress(projectsToDownload[i])*0.01f;
					if (projectMetadataCache.ContainsKey(projectsToDownload[i]) && projectMetadataCache[projectsToDownload[i]] != null) {
						total = (int)projectMetadataCache[projectsToDownload[i]].TotalSize;
					}
					break;
				}
			}
			if (projectsFinished >= projectsToDownload.Count)
			{
				animator.SetBool("Downloading", false);
				animator.SetBool("GotFiles", true);
				if (canSelect)
				{
					App.ShowCrosshair = true;
					VRInput.MotionControllerLaser = false;
					CurvedCanvasInputModule.forceGaze = true;
				}
			}
			else
			{
				if (progress < 0 || total < 0) {
					Debug.LogWarning("Displaying negative download progress?");
				}

				progressA.text = Mathf.RoundToInt(Mathf.Max(progress * total, 0f)).ToString() + " / " + total.ToString() + " MB";
				progressB.text = "Project "+(projectsFinished+1).ToString() + " / " + projectsToDownload.Count.ToString();
				progressBar.material.SetFloat("_Progress", progress);
			}
		}
	}
}
