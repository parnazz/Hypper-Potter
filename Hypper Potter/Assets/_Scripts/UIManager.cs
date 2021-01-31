using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Button startButton;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(FindObjectOfType<GameManager>().StartGame);
        EventManager.onGameStarted += HideStartButton;
    }

    private void HideStartButton()
    {
        startButton.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        EventManager.onGameStarted -= HideStartButton;
    }
}
