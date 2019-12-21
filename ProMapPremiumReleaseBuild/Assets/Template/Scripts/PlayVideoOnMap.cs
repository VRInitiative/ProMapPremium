using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Headjack;
using TMPro;
using UnityEngine.UI;

public class PlayVideoOnMap : MonoBehaviour
{

    bool downloading = false;
    public string projectId;
    private Material progressMaterial;
    public TextMeshProUGUI progressText;
    private long fileSize;
    public RawImage progressGraphic;
    public EssentialsManager essentialsManager;
    public GameObject CanvasDownload;

    public void Start()
    {
        progressMaterial = progressGraphic.material;
        fileSize = (long)App.GetProjectMetadata(projectId, App.ByteConversionType.Megabytes).TotalSize;
    }


    public void PlayAndDownload()
    {
        CanvasDownload.SetActive(true);
        downloading = true;
        essentialsManager.DownloadSubtitles(projectId);
        App.Download(projectId, false, delegate (bool s, string e)
        {
           


            if (s)
            {
                
                PlayVideo();
                downloading = false;
            }
            else
            {
                if (e != "Cancel")
                {
                    PopupMessage.instance.Show("Download Failed \n " + e, PopupMessage.ButtonMode.Confirm, null);
                    downloading = false;
                    CanvasDownload.SetActive(false);


                }
                
            }
        });

       

    }

    public void PlayVideo()
    {
        App.Fade(true, 1f, delegate (bool s, string e)
        {
            VideoPlayerManager.Instance.Initialize(projectId, false, delegate (bool ss, string ee)
            {
                //
            });
        });
    }

    public void Update()
    {
        if(downloading == true)
        {
            float progress = App.GetProjectProgress(projectId) * 0.01f;
            progressMaterial.SetFloat("_Progress", progress);
            progressText.text = ((long)(fileSize * progress)).ToString() + " / " + fileSize.ToString() + " MB";
        }
        else
        {
            progressText.text = fileSize.ToString() + " / " + fileSize.ToString() + " MB";
        }
    }

}
