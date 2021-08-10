using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleScript : MonoBehaviour
{
    public ParticleSystem bubble; 


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            PlayParticles(); 
           
    }

    void PlayParticles()
    {
        bubble.Play(); 
    }

}
