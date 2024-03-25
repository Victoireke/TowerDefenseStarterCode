using UnityEngine;
using UnityEngine.UIElements;

public class TopMenu : MonoBehaviour
{
    public Label wavelabel;
    public Label creditslabel;
    public Label healthlabel;
    public Button startWaveButton;
    private VisualElement root;

    public void UpdateTopMenuLabels(int credits, int health, int currentWave)
    {
        Debug.Log("Updating top menu labels: Credits: " + credits + ", Health: " + health + ", Wave: " + currentWave);
        creditslabel.text = "Credits: " + credits;
        healthlabel.text = "Health: " + health;
        wavelabel.text = "Wave: " + currentWave;
    }
    public void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        startWaveButton = root.Q<Button>("start-button");
        wavelabel = root.Q<Label>("wavelabel");
        creditslabel = root.Q<Label>("creditslabel");
        healthlabel = root.Q<Label>("healthlabel");

    }


    // Functie om de wave-label bij te werken
    public void SetWaveLabel(string text)
    {
        wavelabel.text = text;
    }

    // Functie om de credits-label bij te werken
    public void SetCreditsLabel(string text)
    {
        creditslabel.text = text;
    }

    // Functie om de health-label bij te werken
    public void SetHealthLabel(string text)
    {
        healthlabel.text = text;
    }


}