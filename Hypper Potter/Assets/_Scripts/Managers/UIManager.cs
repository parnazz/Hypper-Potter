using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Button startButton;

    [SerializeField]
    private Text coinCounterText;

    [SerializeField]
    private GameObject endScreen;

    [SerializeField]
    private Text playerPlaceText;

    [SerializeField]
    private Text totalCoinsText; // ѕоказывает число монет, собранных в конце игры

    [SerializeField]
    private Button restartButton;

    private int coinCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(FindObjectOfType<GameManager>().StartGame);
        restartButton.onClick.AddListener(FindObjectOfType<GameManager>().RestartGame);
        EventManager.onGameStarted += HideStartButton;
        EventManager.onCoinPickup += IncreaseCoinCounter;
        EventManager.onGameEndScreenOpened += OpenEndScreen;
    }

    private void HideStartButton()
    {
        startButton.gameObject.SetActive(false);
    }

    private void IncreaseCoinCounter()
    {
        coinCounter++;
        coinCounterText.text = "Coins: " + coinCounter;
    }

    private void OpenEndScreen(int playerPlace)
    {
        endScreen.gameObject.SetActive(true);
        coinCounterText.gameObject.SetActive(false);

        playerPlaceText.text = "You finished " + playerPlace;
        totalCoinsText.text = "You collected " + coinCounter + " coins";
    }

    private void OnDisable()
    {
        EventManager.onGameStarted -= HideStartButton;
        EventManager.onCoinPickup -= IncreaseCoinCounter;
        EventManager.onGameEndScreenOpened -= OpenEndScreen;
    }
}
