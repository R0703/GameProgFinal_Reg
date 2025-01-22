using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 200;
    public int currhealth;
    public int damage = 15;

    void Start()
    {
        currhealth = maxHealth;
        GameManager.instance.setPlayerHPText(currhealth.ToString());
    }

    public void TakeDamage(int damage)
    {
        currhealth -= damage;
        GameManager.instance.setPlayerHPText(currhealth.ToString());
        if (currhealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        //Debug.log("you ded");
    }
}
