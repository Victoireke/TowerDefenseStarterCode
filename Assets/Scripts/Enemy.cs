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
                    AttackGate(); // Roep AttackGate functie van GameManager aan als het laatste waypoint bereikt is
                    Destroy(gameObject);
                }
            }

        }
    }
    public void Damage(int damage)
    {
        health -= damage; // Verminder de gezondheidswaarde met de opgegeven schade

        // Controleer of de gezondheidswaarde kleiner is dan of gelijk is aan nul
        if (health <= 0)
        {
            AddCredits(points); // Roep AddCredits functie van GameManager aan als de gezondheid 0 is
            Destroy(gameObject); // Vernietig het gameobject
        }
    }

    // Functie om de poort aan te vallen
    private void AttackGate()
    {
        GameManager.Instance.AttackGate(); // Roep AttackGate functie van GameManager aan
    }

    // Functie om credits toe te voegen
    private void AddCredits(int amount)
    {
        GameManager.Instance.AddCredits(amount); // Roep AddCredits functie van GameManager aan en geef het aantal punten mee
    }
}
