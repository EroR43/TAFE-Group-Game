using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public CanvasGroup travel;
    public CanvasGroup town;
    public CanvasGroup forest;
    //public CanvasGroup swamp;
    //public CanvasGroup mount;
    public Player_Map player;

    public Animator anim;

    private void Awake()
    {
        PressTravel();
        anim = GetComponentInChildren<Animator>();
    }

    public void ShowTown()
    {
        travel.interactable = true;
        travel.alpha = 1f;
        travel.blocksRaycasts = true;
        town.interactable = true;
        town.alpha = 1f;
        town.blocksRaycasts = true;
    }

    public void ShowForest()
    {
        travel.interactable = true;
        travel.alpha = 1f;
        travel.blocksRaycasts = true;
        forest.interactable = true;
        forest.alpha = 1f;
        forest.blocksRaycasts = true;
    }

    //public void ShowSwamp()
    //{
    //    travel.interactable = true;
    //    travel.alpha = 1f;
    //    travel.blocksRaycasts = true;
    //    swamp.interactable = true;
    //    swamp.alpha = 1f;
    //    swamp.blocksRaycasts = true;
    //}

    //public void ShowMount()
    //{
    //    travel.interactable = true;
    //    travel.alpha = 1f;
    //    travel.blocksRaycasts = true;
    //    mount.interactable = true;
    //    mount.alpha = 1f;
    //    mount.blocksRaycasts = true;
    //}

    public void PressTravel()
    {
        travel.interactable = false;
        travel.alpha = 0f;
        travel.blocksRaycasts = false;
        town.interactable = false;
        town.alpha = 0f;
        town.blocksRaycasts = false;
        forest.interactable = false;
        forest.alpha = 0f;
        forest.blocksRaycasts = false;
        //swamp.interactable = false;
        //swamp.alpha = 0f;
        //swamp.blocksRaycasts = false;
        //mount.interactable = false;
        //mount.alpha = 0f;
        //mount.blocksRaycasts = false;
        player.movementEnabled = true;
    }

    public void NoGo()
    {
        anim.SetTrigger("noGo");
    }
}
