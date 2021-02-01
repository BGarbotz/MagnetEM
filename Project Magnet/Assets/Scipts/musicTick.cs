using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PlayerLoop;
using System;
using System.Linq;

public class musicTick : MonoBehaviour, IPointerClickHandler
{
    public GameObject musicSource;
    public AudioSource musicSource_component;
    public Sprite[] states;
    public Image image;

    void Start()
    {
        PlayerPrefs.GetInt("Music");
        image = GetComponent<Image>();
    }

    void Update()
    {
        image.sprite = states[PlayerPrefs.GetInt("Music")];
        if (PlayerPrefs.GetInt("Music") == 0)
        {
            musicSource_component.Stop();
        }
        else if (!musicSource_component.isPlaying)
        {
            musicSource_component.Play();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayerPrefs.SetInt("Music", (PlayerPrefs.GetInt("Music") + 1) % 2);
    }
}
