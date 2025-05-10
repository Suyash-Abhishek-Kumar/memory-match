using UnityEngine;
using UnityEngine.UI;

public class FlipCounterUI : MonoBehaviour
{
    public Text flipCounterText;

    void Update()
    {
        flipCounterText.text = "Flips: " + GameManager.instance.flipCount + " / " + GameManager.instance.flipLimit;
    }
}
