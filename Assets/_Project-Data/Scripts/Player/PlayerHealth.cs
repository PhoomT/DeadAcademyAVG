using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health; // Player's current health
    public int maxHealth = 100; // Player's maximun health
    public AudioSource damageSound; // Sound effect that will play when the player takes damage
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth; // Sets your current health to full when the game starts
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This function will be called when the player takes damage
    public void TakeDamage(int amount) // amount = how much damage the player takes
    {
        damageSound.Play(); // Plays the damage sound effect

        // If the player's health is down to (or equal to) zero, the player gameobject will be destroyed
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
