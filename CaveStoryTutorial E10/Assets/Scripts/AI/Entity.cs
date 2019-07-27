using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    public int maxHealth = 5;
    private int health;

    protected virtual void Awake()
    {
        health = maxHealth;
    }

    public void AddHealth(int m) { ModHealth(m); }
    public void SubtractHealth(int m) { ModHealth(-m); }

    private void ModHealth(int m)
    {
        health += m;
        CapHealth();
    }

    private void CapHealth()
    {
        if(health <= 0)
        {
            Die();
        } else if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    protected virtual void Die()
    {
        Debug.Log(name + " died!");
    }

}
