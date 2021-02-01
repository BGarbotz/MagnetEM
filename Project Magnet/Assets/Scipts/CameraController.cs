using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int animation_index;
    public string []animations;
    public Animation animation;

    private void Start()
    {
        animations = new string[] { "setting_menu", "setting_menu_back" };
        animation = GetComponent<Animation>();
    }
    public void changeLocation()
    {
        animation.Play(animations[animation_index]);
        animation_index = (animation_index + 1) % 2;

    }
}
