using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Diagnostics;

public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler 
{
    //Is the button currently being pressed?
    public bool isPressed;

    public GameObject other;

    public Button thisButton;

    public Button otherButton;


    void Start() {
        isPressed = false;
        thisButton = this.GetComponent<Button>();
        otherButton = other.GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (thisButton.interactable) {
            isPressed = true;
            otherButton.interactable = false;
            UnityEngine.Debug.Log("This button was pressed");
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        otherButton.interactable = true;
        UnityEngine.Debug.Log("This button is no longer being pressed");
    }

    public bool getIsPressed()
    {
        return isPressed;
    }

}
