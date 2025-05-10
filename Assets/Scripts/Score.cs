using UnityEngine;
using UnityEngine.UI; // Use TMPro if using TextMeshPro

public class Score : MonoBehaviour
{
    public Text ScoreText; // Or use TMP_Text for TextMeshPro

    void Update()
    {
        ScoreText.text = "Score: " + GameManager.instance.matchedPairs;
    }
}
