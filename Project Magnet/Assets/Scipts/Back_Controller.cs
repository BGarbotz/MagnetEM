using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.PlayerLoop;
using System;
using System.Linq;

public class Back_Controller : MonoBehaviour, IPointerClickHandler
{
    public GameObject camera;
    public CameraController camera_controller;
    public GameObject settings_button;
    public GameObject[] scoreboard;

    // Start is called before the first frame update
    void Start()
    {
        camera_controller = camera.GetComponent<CameraController>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        camera_controller.changeLocation();
        foreach (GameObject element in scoreboard)
        {
            element.SetActive(true);
        }
        settings_button.SetActive(true);

    }
}
