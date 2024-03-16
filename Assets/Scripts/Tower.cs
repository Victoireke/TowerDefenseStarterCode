using UnityEngine;


public class Tower : MonoBehaviour
{
    public float attackRange = 1f;
    public float attackRate = 1f;
    public int attackDamage = 1;
    public float attackSize = 1f;
    public GameObject bulletPrefab;
    public TowerType type;

    private float nextAttackTime;

    void Update()
    {
        // Check if it's time to attack
        if (Time.time >= nextAttackTime)
        {
            // Scan for enemies within range and shoot at one of them
            Attack();
            // Update next attack time
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    void Attack()
    {
        // Find all enemies within attack range
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                // Create a bullet instance
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bullet.transform.localScale = new Vector3(attackSize, attackSize, 1f); // Set bullet scale

                // Set target and damage in the Projectile component of the bullet
                Projectile projectile = bullet.GetComponent<Projectile>();
                if (projectile != null)
                {
                    projectile.target = collider.transform;
                    projectile.damage = attackDamage;
                }

                // Break after shooting one enemy per attack
                break;
            }
        }
    }

    // Draw the attack range in the editor for easier debugging
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
