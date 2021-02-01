using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Resources;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Describes the possible states of the player
    public enum pol { Neutral, Plus, Minus };

    //original pol, left and right
    public pol leftPolOriginal;

    public pol rightPolOriginal;

    //Target Rotation of the current Rotation 
    public Quaternion target;

    //Is the player currently rotating 
    public bool isRotating;

    //The start rotation of the player object
    public Quaternion start;

    //Left Collider of the player
    public GameObject leftPole;

    //right Collider of the player 
    public GameObject rightPole;


    //Scrits of both poles, left and right
    private Pole leftPoleScript;

    private Pole rightPoleScript;

    public GameObject model;

    public Material[] materials;

    public GameObject particleContainerRed;

    public GameObject particleContainerBlue;

    public ParticleSystem particleRed;

    public ParticleSystem particleBlue;

    //This method gets always executed on startup
    void Start()
    {
        isRotating = false;
        start = transform.rotation;
        target = transform.rotation;

        leftPoleScript = leftPole.GetComponent<Pole>();
        rightPoleScript = rightPole.GetComponent<Pole>();

        rightPolOriginal = rightPoleScript.GetPol();
        leftPolOriginal = leftPoleScript.GetPol();

        particleBlue = particleContainerBlue.GetComponent<ParticleSystem>();
        particleRed = particleContainerRed.GetComponent<ParticleSystem>();


    }
    void FixedUpdate()
    {
        // Rotates the gameobject towards the target rotation (Startrotation + 180°) 
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, 15f);

        //If the target rotation has been reached the target becomes the new start rotation
        if (transform.rotation == target)
        {
            start = transform.rotation;
        }

        Material[] mats = { materials[0], materials[1] };
        model.GetComponent<MeshRenderer>().materials = mats;
    }

    //Sets the player's pol to a specified value
    public void setPlayerPol(pol newPol)
    {
        if (newPol == pol.Neutral)
        {
            leftPoleScript.SetPol(leftPolOriginal);
            rightPoleScript.SetPol(rightPolOriginal);

            Material[] mats = { materials[0], materials[1] };
            model.GetComponent<MeshRenderer>().materials = mats;

            if (particleRed.isPlaying)
            {
                particleRed.Stop();
            }

            if (particleBlue.isPlaying)
            {
                particleBlue.Stop();
            }
            
        }
        else
        {
            if (newPol == pol.Minus)
            {
                Material[] mats = { materials[3], materials[3] };
                model.GetComponent<MeshRenderer>().materials = mats;
                
                if (!particleBlue.isPlaying)
                {
                    particleBlue.Play();
                }
            }
            else
            {
                UnityEngine.Debug.Log(materials[2].ToString());
                Material[] mats = { materials[2], materials[2] };
                model.GetComponent<MeshRenderer>().materials = mats;

                if (!particleRed.isPlaying)
                {
                    particleRed.Play();
                }
            }

            leftPoleScript.SetPol(newPol);
            rightPoleScript.SetPol(newPol);

        }
    }

    //Sets the new target rotation of the player object, the player gets rotated along the y-Axis 
    public void playerRotate()
    { 
        //Only change the target position if target and are the same which means the player currently is not rotating
        if (start == target)
        {
            start = transform.rotation;
            target = start * Quaternion.Euler(0, 180, 0);
        }

        leftPoleScript.isFlipped = !(leftPoleScript.isFlipped && leftPoleScript.isFlipped);
        rightPoleScript.isFlipped = !(rightPoleScript.isFlipped && rightPoleScript.isFlipped);

    }

}