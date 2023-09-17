using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class BoxSlot : MonoBehaviour
{
    private bool lastPoke;
    private int lastPokeCounter;
    public bool selected;
    public GameObject boxmanager;

    public void selectThis()
    {
        boxmanager.GetComponent<BoxManager>().selectSlot(transform.gameObject);
    }
    public Pokemon getPokemon()
    {
        return transform.GetChild(0).GetComponent<BoxItem>().pokemon;
    }

    public bool checkLast(Transform droppedPoke)
    {
        GameObject teambox = droppedPoke.parent.gameObject;
        
        if (teambox.name == "TeamBox")
        {
            lastPokeCounter = 1;
            lastPoke = false;
            
            for (int i = 0; i < 6; i++)
            {
                if (teambox.transform.GetChild(i).childCount != 0)
                {
                    lastPokeCounter++;
                }
            }
            if (lastPokeCounter==1)
            {
                lastPoke = true;
            }
        }

        return lastPoke;
    }

    
}
