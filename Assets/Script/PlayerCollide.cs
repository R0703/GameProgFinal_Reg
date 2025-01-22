using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerCollide : MonoBehaviour
{
    [Header("Audio Setup")]
    public AudioSource audioSource;
    public AudioClip cowSound;
    public AudioClip chickenSound;
    public AudioClip pigSound;

    private bool isCollidingWithAnimal;
    private GameObject currentAnimal;

    private bool isCollidingWithMob;
    private GameObject currentMob;

    private void Update()
    {
        if (isCollidingWithMob && Input.GetKeyDown(KeyCode.Space))
        {
            AttackMob();
        }

        if (isCollidingWithAnimal && Input.GetKeyDown(KeyCode.E))
        {
            PlayAnimalSound();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Animal"))
        {
            isCollidingWithAnimal = true;
            currentAnimal = collision.gameObject;
        }
        if (collision.gameObject.CompareTag("Mob"))
        {
            isCollidingWithMob = true;
            currentMob = collision.gameObject;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
       
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mob"))
        {
            isCollidingWithMob = false;
            currentMob = null;
        }
        if (collision.gameObject.CompareTag("Animal"))
        {
            isCollidingWithAnimal = false;
            currentAnimal = null;
        }
    }

    private void AttackMob()
    {
        if (currentMob != null)
        {
            MobStats mobStats = currentMob.GetComponent<MobStats>();
            if (mobStats != null)
            {
                mobStats.TakeDamage(20);
            }
        }
    }

    private void PlayAnimalSound()
    {
        if (currentAnimal != null)
        {
            string animalName = currentAnimal.name; 

            if (animalName.Contains("Cow") && cowSound != null)
            {
                UnityEngine.Debug.Log("moo");
                audioSource.PlayOneShot(cowSound);
            }
            else if (animalName.Contains("Chicken") && chickenSound != null)
            {
                audioSource.PlayOneShot(chickenSound);
            }
            else if (animalName.Contains("Pig") && pigSound != null)
            {
                audioSource.PlayOneShot(pigSound);
            }
        }
    }
}
