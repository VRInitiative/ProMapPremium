using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//[RequireComponent(typeof(Image))]
public class OnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject go;
    public GameObject go2;

    

    public void OnPointerEnter(PointerEventData eventData)
    {
        go.SetActive(true);
        go2.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        go.SetActive(false);
        go2.SetActive(false);
    }

    
}
