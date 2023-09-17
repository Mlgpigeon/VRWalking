using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Species")]
public class Species : ScriptableObject
{
    public string speciesName;
    public string speciesId;
    public Sprite sprite;
    public Sprite shinySprite;
    public Type type1;
    public Type type2;
    public AudioClip cry;
    public AudioClip theme;
    public enum Type
    {  
        None,
        Normal,
        Fighting,
        Flying,
        Poison,
        Ground,
        Rock,
        Bug,
        Ghost,
        Steel,
        Fire, 
        Water,
        Grass,
        Electric,
        Psychic,
        Ice,
        Dragon,
        Dark,
        Fairy
      
    }
}
