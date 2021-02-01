using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.PlayerLoop;
using System;
using System.Linq;

/*
 * Script to determine logic behind Spin button
 */

public class SpinButton : MonoBehaviour, IPointerClickHandler
{
    //The player object
    public GameObject player;

    //The Script Component that controls the player object
    private PlayerController playerScript;

    /*
     * This method gets always executed on startup
     */
    void Start()
    {
        playerScript = player.GetComponent<PlayerController>(); 
    }

    /*
     * This method gets executed when the pointer clicks on the button, making the player spin 
     */
    public void OnPointerClick(PointerEventData eventData)
    {
        playerScript.playerRotate();
        int[] numbers = new int[] { 1, 2, 3, 4, 5 };
        float[] weights = new float[] { 0.5f, 0.1f, 0.1f, 0.1f, 0.1f };
   }
}
