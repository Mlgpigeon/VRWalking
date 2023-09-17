using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WristMenu : MonoBehaviour
{
    public Material off;
    public Material on;
    private Renderer renderer;
    public GameObject menu;
    public GameObject boxMenu;
    public GameObject boxManager;
    public BoxSlot[] BoxSlots;
    public GameObject[] Sprites;
    public GameObject[] Teambar;
    public GameObject settingsMenu;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    public void SetOn()
    {
        if (boxMenu.activeSelf)
        {
            boxManager.GetComponent<BoxManager>().boxToWrist();
        }
        
        if (settingsMenu.activeSelf)
        {
            boxManager.GetComponent<BoxManager>().settingsToWrist();
        }

        UpdateTeambar();
        var mats = renderer.materials;
        mats[4] = on;
        renderer.materials = mats;
        menu.SetActive(!menu.activeSelf);  
        
        
    }
    public void SetOff()
    {
        var mats = renderer.materials;
        mats[4] = off;
        renderer.materials = mats;
    }

    public void UpdateTeambar()
    {
        for (int i = 0; i < 6; i++)
        {
            if (BoxSlots[i].transform.childCount != 0)
            {
                Sprites[i].SetActive(true);
                Teambar[i].transform.GetChild(1).gameObject.SetActive(true);
                if (!BoxSlots[i].getPokemon().shiny)
                {
                    Sprites[i].GetComponent<UnityEngine.UI.Image>().sprite = BoxSlots[i].getPokemon().species.sprite;
                }
                else
                {
                    Sprites[i].GetComponent<UnityEngine.UI.Image>().sprite =
                        BoxSlots[i].getPokemon().species.shinySprite;
                }
            }
            else
            {
                Sprites[i].SetActive(false);
                Sprites[i].GetComponent<UnityEngine.UI.Image>().sprite = null;
                Teambar[i].transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }
}
