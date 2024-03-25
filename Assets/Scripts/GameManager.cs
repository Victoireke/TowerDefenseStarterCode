using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private ConstructionSite selectedSite;
    private TopMenu topMenu;
    private int credits;
    private int health;
    private int currentWave;
    private bool waveActive = false;
    private int enemyInGameCounter = 0;

    public GameObject TowerMenu;
    private TowerMenu towerMenu;
    public List<GameObject> Archers = new List<GameObject>();
    public List<GameObject> Swords = new List<GameObject>();
    public List<GameObject> Wizards = new List<GameObject>();

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        towerMenu = TowerMenu.GetComponent<TowerMenu>();
        topMenu = FindObjectOfType<TopMenu>();
        StartGame();
    }

    private void StartGame()
    {
        credits = 200;
        health = 10;
        currentWave = 0;
        topMenu.UpdateTopMenuLabels(credits, health, currentWave);
    }

    public void SelectSite(ConstructionSite site)
    {
        this.selectedSite = site;
        towerMenu.SetSite(site);
    }

    public void Build(TowerType type, SiteLevel level)
    {
        if (selectedSite == null)
        {
            return;
        }

        List<GameObject> towerList = null;
        switch (type)
        {
            case TowerType.Archer:
                towerList = Archers;
                break;
            case TowerType.Sword:
                towerList = Swords;
                break;
            case TowerType.Wizard:
                towerList = Wizards;
                break;
        }

        GameObject towerPrefab = towerList[(int)level];
        Vector3 buildPosition = selectedSite.BuildPosition();

        if (level == SiteLevel.Onbebouwd)
        {
            AddCredits(GetCost(type, level, true));
        }
        else
        {
            int cost = GetCost(type, level);
            if (cost <= credits)
            {
                credits -= cost;
                GameObject towerInstance = Instantiate(towerPrefab, buildPosition, Quaternion.identity);
                selectedSite.SetTower(towerInstance, level, type);
                towerMenu.SetSite(null);
            }
            else
            {
                Debug.Log("Niet genoeg credits om deze toren te bouwen!");
            }
        }

        topMenu.UpdateTopMenuLabels(credits, health, currentWave);
    }

    public void AttackGate()
    {
        health--;
        topMenu.SetHealthLabel("Health: " + health);
    }

    public void AddCredits(int amount)
    {
        credits += amount;
        topMenu.SetCreditsLabel("Credits: " + credits);
        towerMenu.EvaluateMenu();
    }

    public void RemoveCredits(int amount)
    {
        credits -= amount;
        topMenu.SetCreditsLabel("Credits: " + credits);
        towerMenu.EvaluateMenu();
    }

    public int GetCredits()
    {
        return credits;
    }

    public int GetCost(TowerType type, SiteLevel level, bool selling = false)
    {
        return 0;
    }

    public static GameManager Get
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    public void StartWave()
    {
        currentWave++;
        topMenu.UpdateTopMenuLabels(credits, health, currentWave);
        waveActive = true;
        enemyInGameCounter = 0;
    }

    public void EndWave()
    {
        waveActive = false;
        if (enemyInGameCounter <= 0)
        {
            if (currentWave == 10) // Golfnummer veranderd naar 10
            {
                // Logica voor het einde van het spel
            }
            else
            {
                topMenu.EnableWaveButton();
            }
        }
    }

    public void AddInGameEnemy()
    {
        enemyInGameCounter++;
    }

    public void RemoveInGameEnemy()
    {
        enemyInGameCounter--;

        if (!waveActive && enemyInGameCounter <= 0)
        {
            if (currentWave == 10) // Golfnummer veranderd naar 10
            {
                // Logica voor het einde van het spel
            }
            else
            {
                topMenu.EnableWaveButton();
            }
        }
    }
}
