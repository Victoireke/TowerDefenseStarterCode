using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform target;
    public float speed;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        // Rotate the projectile towards the target
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * speed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // When target is null, it no longer exists and this
        // object has to be removed
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Move the projectile towards the target
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        // Check if the distance between this object and
        // the target is smaller than 0.2. If so, apply damage and destroy this object.
        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            // Apply damage to the target
            DealDamage();

            // Destroy the projectile
            Destroy(gameObject);
        }
    }

    void DealDamage()
    {
        // Check if the target has a collider
        Collider targetCollider = target.GetComponent<Collider>();
        if (targetCollider != null)
        {
            // Apply damage directly
            target.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
        }
    }
}
