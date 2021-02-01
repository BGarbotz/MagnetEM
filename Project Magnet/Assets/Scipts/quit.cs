using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.PlayerLoop;
using System;
using System.Linq;

public class quit : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Application.Quit();
    }

}
