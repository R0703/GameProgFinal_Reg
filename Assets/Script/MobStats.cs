using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStats : MonoBehaviour
{
    public int maxHealth = 60;
    public int currHealth;
    public int mobdmg = 10;
    public AudioClip skeletonHitSFX;
    public AudioClip slimeHitSFX;
    public Animator animator;

    private AudioSource audioSource;

    private bool isDead = false;

    void Start()
    {
        currHealth = maxHealth;
        GameManager.instance.setMobHPText(currHealth.ToString());
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        currHealth -= damage;
        GameManager.instance.setMobHPText(currHealth.ToString());

        PlayHitSound();

        if (currHealth <= 0)
        {
            Die();
        }
    }

    private void PlayHitSound()
    {
        if (gameObject.name.Contains("Skeleton") && skeletonHitSFX != null)
        {
            audioSource.PlayOneShot(skeletonHitSFX);
        }
        else if (gameObject.name.Contains("Slime") && slimeHitSFX != null)
        {
            audioSource.PlayOneShot(slimeHitSFX);
        }
    }

    private void Die()
    {
        //GameManager.instance.IncrementEnemiesDefeated();
        //Destroy(gameObject);
        if (!isDead)
        {
            isDead = true;
            Debug.Log($"{gameObject.name} has died. Incrementing enemies defeated.");
            GameManager.instance?.IncrementEnemiesDefeated();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(mobdmg);
            }
        }
    }
}
