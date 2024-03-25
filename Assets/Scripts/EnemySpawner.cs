using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    public List<GameObject> Path1 = new List<GameObject>();
    public List<GameObject> Path2 = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();
    private int ufoCounter = 0;

    public static EnemySpawner Get{get { return instance; }}

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void SpawnEnemy(int type, Path path)
    {
        Vector3 spawnPosition;
        Quaternion spawnRotation;
        if (path == Path.Path1)
        {
            spawnPosition = Path1[0].transform.position;
            spawnRotation = Path1[0].transform.rotation;
        }
        else if (path == Path.Path2)
        {
            spawnPosition = Path2[0].transform.position;
            spawnRotation = Path2[0].transform.rotation;
        }
        else
        {
            spawnPosition = Vector3.zero;
            spawnRotation = Quaternion.identity;
            Debug.LogError("invalid path secified!");
            return;
        }
        var newEnemy = Instantiate(enemies[type],spawnPosition,spawnRotation);
        var script = newEnemy.GetComponent<Enemy>();

        script.path = path;
        script.target = Path1[1];
    }
    public GameObject RequestTarget(Path path, int index)
    {
        List<GameObject> currentPath = null;
        switch (path)
        {
            case Path.Path1:
                currentPath = Path1;
                break;
            case Path.Path2:
                currentPath = Path2;
                break;
            default:
                Debug.LogError("invalid path secified!");
                break;
        }
        if (currentPath == null || index < 0 || index >= currentPath.Count)
        {
            Debug.LogError("invalid path or index!");
            return null;
        }
        else { return currentPath[index]; }
    }
    void Start()
    {
        InvokeRepeating("SpawnTester", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SpawnTester() { SpawnEnemy(0, Path.Path1); }
    public void StartWave(int number)
    {
        ufoCounter = 0; // Reset counter

        switch (number)
        {
            case 1:
                InvokeRepeating("StartWave1", 1f, 1.5f); // Start invoking the wave function
                break;
                // Add more cases for additional waves
        }
    }

    public void StartWave1()
    {
        ufoCounter++;

        // Spawn enemies based on wave progression
        if (ufoCounter % 6 <= 1) return;

        if (ufoCounter < 30)
        {
            SpawnEnemy(0, Path.Path1);
        }
        else
        {
            SpawnEnemy(1, Path.Path1);
        }

        // End the wave after a certain condition
        if (ufoCounter > 30)
        {
            CancelInvoke("StartWave1");
            GameManager.Get.EndWave(); // Notify GameManager that the wave has ended
        }
    }

}
