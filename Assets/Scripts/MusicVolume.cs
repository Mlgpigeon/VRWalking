using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolume : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private AudioSource music;

    void Start()
    {
        slider.value = music.volume ;
        slider.onValueChanged.AddListener((v) => music.volume = v);
    }
}
