using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public int damage = 10; // Allows you to set different damage values for each different enemy.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This function is called whenever something enters the enemy's collider
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player") // If this gameobject collides with a gameobject with the "Player" tag...
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
