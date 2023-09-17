using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PartnerManager : MonoBehaviour
{
    Keyboard _input;

    private int old_id;
    public int id;
    public Transform _target;
    private Animator _animator;
    private PokeAnimator _pokeAnimator;
    private FollowPlayer _followPlayer;

    public GameObject[] teamSlots;
    public GameObject[] barSlots;
    public GameObject musicPlayer;
    public bool[] isOut;

    private void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            isOut[i] = false;
        }
    }

    public void takeOutPokemon(int id, Vector3 newPosition)
    {
        if (teamSlots[id].transform.childCount > 0)
        {
          if (!isOut[id])
          {
              spawnPartner(id,newPosition);
          }
        }
        
    }

    public void spawnPartner(int id, Vector3 newPosition)
    {
        
        Pokemon pokemon = teamSlots[id].transform.GetChild(0).gameObject.GetComponent<BoxItem>().pokemon;
        
        GameObject newPartner = Instantiate(Resources.Load("Pokemon/"+pokemon.species.speciesId +"/"+pokemon.species.speciesId) as GameObject);
        print("Spawning " + pokemon.species.speciesId);
        
        if (pokemon.shiny)
        {
            foreach (Transform child in newPartner.transform)
            {
                SkinnedMeshRenderer skinned = child.GetComponent<SkinnedMeshRenderer>();
                if (skinned != null)
                {
                    string name = skinned.material.name;
                    name = name.Substring(0,name.Length - 11);
                    skinned.material = Resources.Load<Material>("Pokemon/" + pokemon.species.speciesId + "/Files/Materials/Shiny/"+name);
                }
            }
        }
        
        //newPartner.AddComponent<MeshCollider>();
        //newPartner.GetComponent<MeshCollider>().convex = true;
        newPartner.AddComponent<NavMeshAgent>();
        newPartner.GetComponent<NavMeshAgent>().speed = 1.7f;
        newPartner.AddComponent<FollowPlayer>();
        newPartner.GetComponent<FollowPlayer>().target = _target;

        newPartner.AddComponent<PokeAnimator>();
        newPartner.GetComponent<PokeAnimator>().pokemonName = pokemon.species.speciesId;
        newPartner.GetComponent<PokeAnimator>().generateAnimator();

        newPartner.transform.SetParent(barSlots[id].transform.GetChild(0));
        newPartner.transform.position = newPosition;
        newPartner.AddComponent<AudioSource>();
        newPartner.GetComponent<AudioSource>().spatialize =true;
        newPartner.GetComponent<AudioSource>().spatialBlend = 1.0f;
        newPartner.GetComponent<AudioSource>().clip =pokemon.species.cry;
        newPartner.GetComponent<AudioSource>().Play();
        //print(isOut[id]);
        isOut[id] = true;
        
        musicPlayer.GetComponent<AudioSource>().clip = pokemon.species.theme;
        musicPlayer.GetComponent<musicPlayer>().currentTheme = pokemon.species.speciesId;
        musicPlayer.GetComponent<AudioSource>().Play();
        //print(isOut[id]);
    }

    public void despawnPartner(int id)
    {
        if (isOut[id])
        {
            foreach (Transform child in barSlots[id].transform.GetChild(0)) {
                GameObject.Destroy(child.gameObject);
            }

            if (musicPlayer.GetComponent<musicPlayer>().currentTheme.Equals(teamSlots[id].transform.GetChild(0)
                    .gameObject.GetComponent<BoxItem>().pokemon.species.speciesId))
            {
                musicPlayer.GetComponent<musicPlayer>().currentTheme = " ";
                musicPlayer.GetComponent<AudioSource>().Stop();
            }
            isOut[id] = false;
        }
        
    }
}
