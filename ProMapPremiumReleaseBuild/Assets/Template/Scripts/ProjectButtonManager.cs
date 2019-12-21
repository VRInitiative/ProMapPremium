﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Headjack;
using TMPro;

public class ProjectButtonManager : MonoBehaviour
{
	public GameObject play, playSmall, download, stream, delete, cancel, progress;
	public string projectId;
	public State state;
	
	public RawImage progressGraphic;
	public TextMeshProUGUI progressText;

	public Animator animator;
	private EssentialsManager essentialsManager;
	private Material progressMaterial;
	private long fileSize;
	public enum State
	{
		BigPlay,
		GotFiles,
		NoFiles,
		Downloading
	}

	public void Initialize(string projectId, EssentialsManager essentialsManager)
	{
		this.essentialsManager = essentialsManager;
		this.projectId = projectId;
		animator.SetTrigger("Restart");
		if (App.GotFiles(projectId))
		{
			Debug.Log("Setting to gotfiles");
			SetState(State.GotFiles);
		}
		else
		{
			SetState(State.BigPlay);
		}
		animator.SetTrigger("Restart");
		progressMaterial = progressGraphic.material;
		fileSize = (long)App.GetProjectMetadata(projectId, App.ByteConversionType.Megabytes).TotalSize;
	}

	public void BigPlayButton()
	{
		if (App.GotFiles(projectId))
		{
			SetState(State.GotFiles);
		} 
		else if (App.ProjectIsDownloading(projectId))
		{
			SetState(State.Downloading);
		}
		else
		{
			SetState(State.NoFiles);
		}
	}

	public void Download()
	{
		essentialsManager.DownloadSubtitles(projectId);
		App.Download(projectId, false, delegate (bool s, string e)
		{
			if (s)
			{
				BigPlayButton();
			}
			else
			{
				if (e != "Cancel")
				{
					PopupMessage.instance.Show("Download Failed \n " + e, PopupMessage.ButtonMode.Confirm, null);
				}
				SetState(State.NoFiles);
			}
		});
		SetState(State.Downloading);
	}
	public void Cancel()
	{
		PopupMessage.instance.Show("Are you sure you want to cancel this download?", PopupMessage.ButtonMode.YesNo, delegate (bool confirm)
		{
			if (confirm)
			{
				if (App.GotFiles(projectId))
				{
					App.Delete(projectId);
					SetState(State.NoFiles);
                    GameObject.Find("CanvasDownload").SetActive(false);
				}
				else
				{
					App.Cancel(projectId);
					SetState(State.NoFiles);
                    GameObject.Find("CanvasDownload").SetActive(false);
				}
                
			}
		});
	}
	public void Delete()
	{
		PopupMessage.instance.Show("Are you sure you want to delete this project?", PopupMessage.ButtonMode.YesNo, delegate (bool confirm)
		{
			if (confirm)
			{
				App.Delete(projectId);
				SetState(State.NoFiles);
			}
		});
	}

	public void Play()
	{
		App.Fade(true, 1f, delegate (bool s, string e)
		  {
			  VideoPlayerManager.Instance.Initialize(projectId, false, delegate (bool ss, string ee)
				{
					//
				});
		  });
	}

	public void Stream()
	{
		App.Fade(true, 1f, delegate (bool s, string e)
		{
			VideoPlayerManager.Instance.Initialize(projectId, true, delegate (bool ss, string ee)
			{
				//
			});
		});
	}

	public void SetState(State state)
	{
		DeactivateAllButtons();
		this.state = state;
		switch (state)
		{
			case State.BigPlay:
				animator.SetBool("ShowBigPlay", true);
				//Debug.Log("Setting bigplay");
				break;
			case State.Downloading:
				animator.SetBool("ShowBigPlay", false);
				animator.SetBool("Downloading", true);
				animator.SetBool("GotFiles", false);
				//Debug.Log("Setting downloading");
				break;
			case State.GotFiles:
				animator.SetBool("ShowBigPlay", false);
				animator.SetBool("Downloading", false);
				animator.SetBool("GotFiles", true);
				delete.GetComponent<Button>().interactable = !App.IsLiveStream(projectId);
				//Debug.Log("Setting gotfiles");
				break;
			case State.NoFiles:
				animator.SetBool("ShowBigPlay", false);
				animator.SetBool("Downloading", false);
				animator.SetBool("GotFiles", false);
				download.GetComponent<Button>().interactable = !App.IsLiveStream(projectId);
				//Debug.Log("Setting nofiles");
				break;
		}
	}

	private void Update()
	{
		if (state == State.Downloading)
		{
			float progress = App.GetProjectProgress(projectId) * 0.01f;
			progressMaterial.SetFloat("_Progress", progress );
			progressText.text = ((long)(fileSize * progress)).ToString() + " / " + fileSize.ToString() + " MB";
		}
		if (state == State.GotFiles)
		{
			SetState(State.GotFiles);
		}
	}

	public void DeactivateAllButtons()
	{
		play.SetActive(false);
		playSmall.SetActive(false);
		download.SetActive(false);
		stream.SetActive(false);
		delete.SetActive(false);
		cancel.SetActive(false);
		progress.SetActive(false);
	}
}
