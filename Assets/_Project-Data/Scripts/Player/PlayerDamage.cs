using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public EnemyAI enemyHealth;
    public int damage = 20;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy") // If this gameobject collides with a gameobject with the "Player" tag...
        {
            enemyHealth.TakeDamage(damage);
        }
    }
}
