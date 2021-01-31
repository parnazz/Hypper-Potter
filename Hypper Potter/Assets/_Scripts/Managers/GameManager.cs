using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int playerPlace = 1;

    private void Start()
    {
        EventManager.onCrossFinishLine += ChangePlayerPlace;
        EventManager.onGameEnd += ShowEndScreen;
    }

    public void StartGame()
    {
        EventManager.onGameStarted?.Invoke();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ChangePlayerPlace()
    {
        playerPlace++;
    }

    private void ShowEndScreen()
    {
        EventManager.onGameEndScreenOpened?.Invoke(playerPlace);
    }

    private void OnDisable()
    {
        EventManager.onCrossFinishLine -= ChangePlayerPlace;
        EventManager.onGameEnd -= ShowEndScreen;
    }
}
