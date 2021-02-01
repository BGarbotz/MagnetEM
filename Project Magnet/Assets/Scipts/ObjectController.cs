using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public PlayerController.pol pol;

    //direction the object is moving in -1 = left, 1 = right
    public int hdirection;
    
    public float speed;
    public GameObject game;

    public ParticleSystem particles;

 

    private void Start()
    {
        particles = GetComponent<ParticleSystem>();
  
    }
    void Update()
    {
        transform.Translate(hdirection * speed * Time.deltaTime, 0, 0);
        
    }

    public void SetPol(PlayerController.pol pol)
    {
        this.pol = pol;
    }

    public PlayerController.pol GetPol()
    {
        return this.pol;
    }

    public void setDirection (int direction)
    {
        this.hdirection = direction; 
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed; 
    }

}
