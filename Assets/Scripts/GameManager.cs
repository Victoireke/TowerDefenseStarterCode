using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private ConstructionSite selectedSite;
    private TopMenu topMenu; // Nieuwe referentie naar het TopMenu script
    private int credits; // Nieuwe variabele voor credits
    private int health; // Nieuwe variabele voor health
    private int currentWave; // Nieuwe variabele voor currentWave
    private bool waveActive = false;

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
        topMenu = FindObjectOfType<TopMenu>(); // Zoek het TopMenu script in de scene
        StartGame(); // Roep de StartGame functie aan bij het starten van het spel
    }

    // Functie om het spel te starten en variabelen in te stellen
    private void StartGame()
    {
        credits = 200; // Start met 200 credits
        health = 10; // Start met 10 health
        currentWave = 0; // Start de wave op 0

        // Update de labels in het topmenu
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

        if (level == SiteLevel.Onbebouwd) // Als level 0, dan verkoop
        {
            // Credits toevoegen voor de verkoop
            AddCredits(GetCost(type, level, true));
        }
        else // Anders bouwen
        {
            int cost = GetCost(type, level);
            if (cost <= credits) // Controleer of er genoeg credits zijn
            {
                credits -= cost; // Credits aftrekken
                GameObject towerInstance = Instantiate(towerPrefab, buildPosition, Quaternion.identity);
                selectedSite.SetTower(towerInstance, level, type);
                towerMenu.SetSite(null);
            }
            else
            {
                Debug.Log("Niet genoeg credits om deze toren te bouwen!");
            }
        }

        // Update de labels in het topmenu na het bouwen/verkopen van een toren
        topMenu.UpdateTopMenuLabels(credits, health, currentWave);
    }

    // Functie om de gate aan te vallen en health te verminderen
    public void AttackGate()
    {
        health--;
        topMenu.SetHealthLabel("Health: " + health);
    }

    // Functie om credits toe te voegen
    public void AddCredits(int amount)
    {
        credits += amount;
        topMenu.SetCreditsLabel("Credits: " + credits);
        towerMenu.EvaluateMenu(); // Evaluatie van de torenmenu na het toevoegen van credits
    }

    // Functie om credits te verwijderen
    public void RemoveCredits(int amount)
    {
        credits -= amount;
        topMenu.SetCreditsLabel("Credits: " + credits);
        towerMenu.EvaluateMenu(); // Evaluatie van de torenmenu na het verwijderen van credits
    }

    // Functie om het aantal credits op te halen
    public int GetCredits()
    {
        return credits;
    }

    // Functie om de kosten van een toren te bepalen
    public int GetCost(TowerType type, SiteLevel level, bool selling = false)
    {
        // Bepaal de kosten op basis van het type en level van de toren
        // Het argument 'selling' wordt gebruikt om te bepalen of het gaat om de verkoop van een toren
        // De exacte implementatie van de kostenbepaling hangt af van je spellogica
        return 0; // Placeholder return, vervang dit met de daadwerkelijke implementatie
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
        currentWave++; // Increase the value of currentWave
        topMenu.UpdateTopMenuLabels(credits, health, currentWave); // Update the label for the current wave in topMenu
        waveActive = true; // Change waveActive to true
    }

    public void EndWave()
    {
        waveActive = false; // Change waveActive to false
    }

}
