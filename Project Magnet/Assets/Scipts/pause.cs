using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PlayerLoop;
using System;
using System.Linq;

public class pause : MonoBehaviour, IPointerClickHandler
{
    public GameObject[] ui;
    public Sprite[] sprites;
    public GameObject player;
    public PlayerController playerScript;
    public Image image_component;
    public int image_index;
    public GameObject[] buttons;
    public bool isPaused = false;

    void Start()
    {
        image_component = GetComponent<Image>();
        image_index = 1;
        playerScript = player.GetComponent<PlayerController>();

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Time.timeScale = (Time.timeScale + 1f) % 2f;

        isPaused = !(isPaused && isPaused);
       
        if (!isPaused)
        {
            image_component.sprite = sprites[0];
            foreach (GameObject element in ui)
            {
                element.SetActive(true);

            }

            foreach (GameObject button in buttons)
            {
                ButtonController button_component = button.GetComponent<ButtonController>();
                button_component.isPressed = false;

            }           
        }

        else
        {
            image_component.sprite = sprites[1];
            foreach (GameObject element in ui)
            {
                element.SetActive(false);

            }
        }
        
    }

}
