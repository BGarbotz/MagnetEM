using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pole : MonoBehaviour
{
    //Polarization of this pole 
    public PlayerController.pol pol;
    public bool isFlipped;
    public GameObject field;
    public GameController game;
    public GameObject particleContainerP;
    public GameObject particleContainerM;
    public GameObject particleContainerFP;
    public GameObject particleContainerFM;

    public ParticleSystem particlesPlus;
    public ParticleSystem particlesMinus; 
    public ParticleSystem flippedParticlesPlus;
    public ParticleSystem flippedParticlesMinus;

    private AudioSource destructionSoundEffect;


    public void Start()
    {
        game = field.GetComponent<GameController>();
        isFlipped = false;
        particlesPlus = particleContainerP.GetComponent<ParticleSystem>();
        particlesMinus = particleContainerM.GetComponent<ParticleSystem>();
        flippedParticlesPlus = particleContainerFP.GetComponent<ParticleSystem>();
        flippedParticlesMinus = particleContainerFM.GetComponent<ParticleSystem>();
        destructionSoundEffect = GetComponent<AudioSource>();
    }

    /*
     * Returns the polarization of this pole
     * 
     * @return polarization of pole
     */
    public PlayerController.pol GetPol()
    {
        return pol;
    }

    /*
     * Sets polarization of this pole
     * 
     * @param newPol new polarization of the pole
     */
    public void SetPol(PlayerController.pol newPol)
    {
        this.pol = newPol;
    }


    /*
     * Behavior of the pole when another objects coolides with it 
     */
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.GetComponent<ObjectController>().GetPol() == pol)
        {
            Debug.Log("Collision");
            if (pol == PlayerController.pol.Minus && !isFlipped)
            {
                particlesMinus.Play();
            }
            else if (pol == PlayerController.pol.Plus && !isFlipped)
            {
                particlesPlus.Play();
            }
            else if (pol == PlayerController.pol.Plus && isFlipped)
            {
                flippedParticlesPlus.Play();
            }
            else if (pol == PlayerController.pol.Minus && isFlipped)
            {
                flippedParticlesMinus.Play();
            }

            destructionSoundEffect.Play();
      
            Destroy(collision.gameObject); 
            game.DecreaseNumberOfObjects();
            game.IncreaseScore();
            PlayerPrefs.SetInt("Score", game.GetScore());
            game.SpawnRandomObject();     
        }
        else
        {
            game.EndGame();
            Debug.Log("Personal best " + PlayerPrefs.GetInt("Personal Best"));
            Debug.Log("Score " + PlayerPrefs.GetInt("Score"));
        }
    }
}
