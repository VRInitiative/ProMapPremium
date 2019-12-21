using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Headjack;

public class TabSwitcher : MonoBehaviour
{
	public EssentialsManager essentialsManager;
	public enum Tab
	{
        Map,
		//Grid,
		Cinema,
		Job
        //kiosk
	}
	public Animator animator;
	public static Tab currentTab = Tab.Map;
    public ProjectButton map, cinema, job;
        //kiosk, grid;

	private void Start()
	{
		CurvedCanvasInputModule.forceGaze = false;
		switch (essentialsManager.template)
		{
			default:
			case EssentialsManager.Template.AllInOne:
				ShowTab(currentTab);
				break;
			/*case EssentialsManager.Template.Grid:
				ShowGrid();
				break;*/
			case EssentialsManager.Template.Cinema:
				ShowCinema();
				break;
            case EssentialsManager.Template.Map:
                ShowMap();
                break;
            case EssentialsManager.Template.Job:
                ShowMap();
                break;
                /*case EssentialsManager.Template.Kiosk:
                    ShowKiosk();
                    break;*/
        }
		gameObject.SetActive(essentialsManager.template == EssentialsManager.Template.AllInOne);
	}
	/*public void ShowGrid()
	{
		ShowTab(Tab.Grid);
	}*/

	public void ShowCinema()
	{
		ShowTab(Tab.Cinema);
	}

    /*public void ShowKiosk()
	{
		ShowTab(Tab.Kiosk);
	}*/

    public void ShowMap()
    {
        ShowTab(Tab.Map);
    }

    public void ShowJob()
    {
        ShowTab(Tab.Job);
    }

    public void ShowTab(Tab tab)
	{
		//if (tab == currentTab) return;
		animator.SetBool("Default", false);
		animator.SetBool("Cinema", false);
        //animator.SetBool("Kiosk", false);
        animator.SetBool("Map", false);
        animator.SetBool("Job", false);
        map.stayHighlighted = cinema.stayHighlighted  = job.stayHighlighted = false; //grid.stayHighlighted = kiosk.stayHighlighted = false;
        map.rawImage.texture = cinema.rawImage.texture = job.rawImage.texture = map.normalTexture; // grid.rawImage.texture = kiosk.rawImage.texture = grid.normalTexture;
        App.ShowCrosshair = !VRInput.MotionControllerAvailable;
		VRInput.MotionControllerLaser = VRInput.MotionControllerAvailable;
		CurvedCanvasInputModule.forceGaze = false;
		switch (tab)
		{
			/*case Tab.Grid:
				animator.SetBool("Default", true);
				grid.stayHighlighted = true;
				break;*/
			case Tab.Cinema:
				animator.SetBool("Cinema", true);
				cinema.stayHighlighted = true;
				break;
            /*case Tab.Kiosk:
				animator.SetBool("Kiosk", true);
				kiosk.stayHighlighted = true;
				break;*/
            case Tab.Map:
                animator.SetBool("Map", true);
                map.stayHighlighted = true;
                break;
            case Tab.Job:
                animator.SetBool("Job", true);
                job.stayHighlighted = true;
                break;
        }
		currentTab = tab;
	}
}
