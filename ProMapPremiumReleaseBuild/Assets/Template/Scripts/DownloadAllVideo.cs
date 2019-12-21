using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Headjack;
using TMPro;
using UnityEngine.UI;


public class DownloadAllVideo : MonoBehaviour
{
    bool downloading = false;
    public List<string> projectId;
    private List<long> fileSize;
    public EssentialsManager essentialsManager;
   /* private Material progressMaterial;
    public TextMeshProUGUI progressText;
    public RawImage progressGraphic;*/
    public GameObject TextState;


/*
    public void Start()
    {
        progressMaterial = progressGraphic.material;

        foreach(string id in projectId)
        {
            fileSize.Add((long)App.GetProjectMetadata(id, App.ByteConversionType.Megabytes).TotalSize);

        }

    }*/

    public void DownloadAllVid()
    {
        downloading = true;

        int i = 1;

        bool downloadingall = false;


        foreach (string id in projectId)
        {
            float progress = App.GetProjectProgress(id) * 0.01f;

            TextState.GetComponent<Text>().text = "All project are downloading..." + progress;

            essentialsManager.DownloadSubtitles(id);

            App.Download(id, false, delegate (bool s, string e)
            {



                if (s)
                {

                    TextState.GetComponent<Text>().text = "Project "+i+" downloaded.";
                    downloading = false;
                    i++;

                }
                else
                {
                    TextState.GetComponent<Text>().text = "Download Failed";
                    if (e != "Cancel")
                    {
                        PopupMessage.instance.Show("Download Failed \n " + e, PopupMessage.ButtonMode.Confirm, null);
                        downloading = false;
                    }

                }
            });

            
        }

        downloadingall = true;

        if (downloadingall == true)
        {
            TextState.GetComponent<Text>().text = "All project are downloaded.";

        }
        else
        {

        }



    }


    public void Update()
    {
        if (downloading == true)
        {
            foreach(string id in projectId)
            {
                float progress = App.GetProjectProgress(id) * 0.01f;

                TextState.GetComponent<Text>().text = "All project are downloading..." + progress;

            }
           
        }
        else
        {
            
        }
    }
}
