using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class IntroScene : MonoBehaviour
{
    public Button startButton;
    public Button quitButton;
    public TextField textField;

    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        startButton = root.Q<Button>("startButton");
        quitButton = root.Q<Button>("quitButton");
        textField = root.Q<TextField>("TextField");

        // Voeg de callbackfuncties toe aan de knoppen
        startButton.clicked += StartGame;
        quitButton.clicked += QuitGame;
    }

    private void OnDestroy()
    {
        // Verwijder de callbackfuncties om geheugenlekken te voorkomen
        startButton.clicked -= StartGame;
        quitButton.clicked -= QuitGame;
    }

    private void StartGame()
    {
        // Laad de GameScene wanneer op de Start Button wordt geklikt
        SceneManager.LoadScene("GameScene");
    }

    private void QuitGame()
    {
        // Sluit de game af wanneer op de Quit Button wordt geklikt
        Application.Quit();
    }
}
