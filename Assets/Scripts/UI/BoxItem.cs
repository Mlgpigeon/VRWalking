using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoxItem : MonoBehaviour
{
    [Header("UI")] public Image image;
    [HideInInspector] public Transform parentAfterDrag;
    public Transform previous;
    
    public Pokemon pokemon;
    
    private void Start()
    {
        InitialiseItem(pokemon);
    }

    public void InitialiseItem(Pokemon newPokemon)
    {
        pokemon = newPokemon;
        if(!newPokemon.shiny){
            this.GetComponent<Image>().sprite = newPokemon.species.sprite;
        }
        else
        {
            this.GetComponent<Image>().sprite = newPokemon.species.shinySprite;
        }
    }
    
    
}
