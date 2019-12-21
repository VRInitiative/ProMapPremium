using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PosteMenu : MonoBehaviour
{
    public ShowQualification SQ;
    public List<string> PosteSelection;
    public PlayVideoOnMap Load;

    private void Start()
    {
        
        
    }

    public void CreateMenu()
    {
        GameObject go =  EventSystem.current.currentSelectedGameObject;
       

        if(SQ.TotalEntreprise.Contains(go))
        {
            foreach(string poste in go.GetComponent<InfosEnt>().Poste)
            {
                PosteSelection.Add(poste);
            }

            foreach(string poste2 in PosteSelection)
            {
                GameObject go2 = GameObject.Find("TitleSectionPoste");

                GameObject Button = new GameObject();

                Button.transform.SetParent(go2.transform);
                Button.AddComponent<RectTransform>();
                Button.AddComponent<CanvasRenderer>();

                Button.AddComponent<Image>();
                Button.GetComponent<Image>().sprite = Resources.Load<Sprite>("UISprite");
                Button.GetComponent<Image>().raycastTarget = true;

                Button.AddComponent<Button>();
                Button.GetComponent<Button>().transition = Selectable.Transition.ColorTint;
                ColorBlock colorVar = Button.GetComponent<Button>().colors;
                colorVar.highlightedColor = new Color(255,255,174);

                Button.AddComponent<PlayVideoOnMap>();
                Load = Button.GetComponent<PlayVideoOnMap>();
                Button.GetComponent<PlayVideoOnMap>().projectId = "4864867680f";
                Button.GetComponent<PlayVideoOnMap>().progressText = GameObject.Find("MB").GetComponent<TMPro.TextMeshProUGUI>();
                Button.GetComponent<PlayVideoOnMap>().progressGraphic = GameObject.Find("ProgressBar").GetComponent<RawImage>();
                Button.GetComponent<PlayVideoOnMap>().essentialsManager = GameObject.Find("Essentials Manager").GetComponent<EssentialsManager>();

                Button.GetComponent<Button>().onClick.AddListener(delegate () { Load.PlayAndDownload(); });


            }
        }


    }
}
