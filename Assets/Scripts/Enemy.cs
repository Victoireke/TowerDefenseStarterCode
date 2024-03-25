using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1f;
    public float health = 10f;
    public int points = 1;
    public Path path { get; set; }
    public GameObject target { get; set; }
    private int pathIndex = 1;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
            if (Vector2.Distance(transform.position, target.transform.position) < 0.1f)
            {
                pathIndex++;
                target = EnemySpawner.instance.RequestTarget(path, pathIndex);

                if (target == null)
                {
                    AttackGate(); // Call GameManager's AttackGate function when reaching the last waypoint
                    Destroy(gameObject);
                    GameManager.Get.RemoveInGameEnemy(); // Call GameManager's RemoveInGameEnemy function when reaching the last waypoint
                }
            }

        }
    }
    public void Damage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            AddCredits(points);
            Destroy(gameObject);
            GameManager.Get.RemoveInGameEnemy(); // Call GameManager's RemoveInGameEnemy function when health reaches 0
        }
    }

    private void AttackGate()
    {
        GameManager.Instance.AttackGate();
    }

    private void AddCredits(int amount)
    {
        GameManager.Instance.AddCredits(amount);
    }
}
