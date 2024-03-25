using UnityEngine;
using UnityEngine.UIElements;

public class TowerMenu : MonoBehaviour
{
    private Button archerButton;
    private Button swordButton;
    private Button wizardButton;
    private Button updateButton;
    private Button destroyButton;
    private ConstructionSite selectedSite;
    private VisualElement root;

    public void EvaluateMenu()
    {
        if (selectedSite == null)
        {
            // If selectedSite is null, return without enabling any buttons
            return;
        }

        // Access the site level property of selectedSite
        int siteLevel = (int)selectedSite.Level;

        // Get the available credits from GameManager
        int availableCredits = GameManager.Instance.GetCredits();

        // Disable all buttons initially
        archerButton.SetEnabled(false);
        swordButton.SetEnabled(false);
        wizardButton.SetEnabled(false);
        updateButton.SetEnabled(false);
        destroyButton.SetEnabled(false);

        // Enable buttons based on site level and available credits using a switch statement
        switch (siteLevel)
        {
            case 0:
                // For site level 0, enable archer, wizard, and sword buttons if credits are sufficient
                if (availableCredits >= GameManager.Instance.GetCost(TowerType.Archer, selectedSite.Level) &&
                    availableCredits >= GameManager.Instance.GetCost(TowerType.Sword, selectedSite.Level) &&
                    availableCredits >= GameManager.Instance.GetCost(TowerType.Wizard, selectedSite.Level))
                {
                    archerButton.SetEnabled(true);
                    wizardButton.SetEnabled(true);
                    swordButton.SetEnabled(true);
                }
                break;
            case 1:
            case 2:
                // For site levels 1 and 2, enable update and destroy buttons
                updateButton.SetEnabled(true);
                destroyButton.SetEnabled(true);
                break;
            case 3:
                // For site level 3, only enable the destroy button
                destroyButton.SetEnabled(true);
                break;
            default:
                // Handle any other site levels if necessary
                break;
        }
    }

    public void SetSite(ConstructionSite site)
    {
        selectedSite = site;
        EvaluateMenu(); // Zorg ervoor dat je EvaluateMenu aanroept om de menu-evaluatie te updaten
    }

}
