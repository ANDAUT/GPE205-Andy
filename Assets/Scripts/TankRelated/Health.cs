using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] HealthBarScript healthBar;
    Rigidbody2D rb;

    public float currentHealth;
    public float maxHealth;
    public float bounty;

    private void Awake()
    {
        if(healthBar != null)
        {
            rb = GetComponent<Rigidbody2D>();
            healthBar = GetComponentInChildren<HealthBarScript>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void TakeDamage(float amount, Pawn attacker)
    {
        //The 'amount' float dictates how much damage went through, whereas the 'attacker' Pawn is the source of the damage

        currentHealth = currentHealth - amount;
        
        if(attacker != null)
        {
            Debug.Log("Attacker: " + attacker.name + " Damage: " + amount + " Target: " + gameObject.name);
            //This resembles Java's println function lol
        }


        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
        {
            healthBar.UpdateHealthCircle(currentHealth, maxHealth);
        }

        if (currentHealth <= 0)
        {
            Die(attacker);
        }
    }

    public void Die(Pawn attacker)
    {
        Destroy(gameObject);
        attacker.controller.AddToScore(bounty);
    }

    public void Heal(float amount, Pawn healer)
    {
        currentHealth = currentHealth + amount;
        Debug.Log("Healer: " + healer.name + " Amount Healed: " + amount + " Target: " + gameObject.name);

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if(healthBar != null)
        {
            healthBar.UpdateHealthCircle(currentHealth, maxHealth);
        }
    }
}
