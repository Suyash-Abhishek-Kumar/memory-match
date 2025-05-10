using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int flipLimit = 60;       // Set in Inspector
    private int currentFlips = 0;

    public Text flipCounterText;
    public Text endMessage;
    public static GameManager instance;

    public GameObject homeScreen;
    public GameObject gameScreen;
    public GameObject endScreen;

    public GameObject easyButton, mediumButton, hardButton, playButton, quitButton;

    public int flipCount = 0;
    public int matchedPairs = 0;
    private int totalPairs = 12;
    private List<TokenController> flippedTokens = new List<TokenController>();

    public bool lockInput = false;

    private void Awake()
    {
        instance = this;
    }

    public void OnTokenFlipped(TokenController token)
    {
        flipCount++;
        flippedTokens.Add(token);

        if (currentFlips >= flipLimit && !AllPairsFound())
        {
            EndGame(false); // Player failed
        }

        if (flippedTokens.Count == 2)
        {
            lockInput = true;
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(0.8f); // Delay to let player see second flip

        if (flipCount >= flipLimit)
        {
            EndGame(false);
        }

        if (flippedTokens[0].tokenID == flippedTokens[1].tokenID)
        {
            matchedPairs += 1;
            if (AllPairsFound())
            {
                EndGame(true); // Player won
            }
        }
        else
        {
            flippedTokens[0].FlipBack();
            flippedTokens[1].FlipBack();
        }

        flippedTokens.Clear();
        lockInput = false;
    }

    bool AllPairsFound()
    {
        return matchedPairs >= totalPairs; // Track this in GameManager
    }

    public void EndGame(bool won)
    {
        endScreen.SetActive(true);
        endMessage.text = won ? "You Win!" : "Out of Flips!";
        lockInput = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnPlayClicked()
    {
        playButton.SetActive(false);
        quitButton.SetActive(false);
        easyButton.SetActive(true);
        mediumButton.SetActive(true);
        hardButton.SetActive(true);
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }

    public void OnDifficultySelected(int level)
    {
        flipLimit = level;

        homeScreen.SetActive(false);
        gameScreen.SetActive(true);
    }
}
