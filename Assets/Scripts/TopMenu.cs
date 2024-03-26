using UnityEngine;
using UnityEngine.UIElements;

public class TopMenu : MonoBehaviour
{
    public Label wavelabel;
    public Label creditslabel;
    public Label healthlabel;
    public Button startWaveButton;
    private VisualElement root;

    void Start()
    {
        // Get the root visual element
        root = GetComponent<UIDocument>().rootVisualElement;

        // Find UI elements by name
        startWaveButton = root.Q<Button>("start-button");
        wavelabel = root.Q<Label>("wavelabel");
        creditslabel = root.Q<Label>("creditslabel");
        healthlabel = root.Q<Label>("healthlabel");

        // Example initialization
        SetCreditsLabel("Credits: 100");
        SetHealthLabel("Health: 100");
        SetWaveLabel("Wave: 1");
    }

    // Function to update the wave label
    public void SetWaveLabel(string text)
    {
        wavelabel.text = text;
    }

    // Function to update the credits label
    public void SetCreditsLabel(string text)
    {
        creditslabel.text = text;
    }

    // Function to update the health label
    public void SetHealthLabel(string text)
    {
        healthlabel.text = text;
    }

    // Function called when the wave button is clicked
    public void WaveButton_clicked()
    {
        GameManager.Get.StartWave(); // Call GameManager's StartWave function
        startWaveButton.SetEnabled(false); // Disable the wave button
    }

    // Function to enable the wave button
    public void EnableWaveButton()
    {
        startWaveButton.SetEnabled(true); // Enable the wave button
    }
}
