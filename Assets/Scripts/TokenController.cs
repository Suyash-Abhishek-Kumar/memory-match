using UnityEngine;
using UnityEngine.UI;

public class TokenController : MonoBehaviour
{
    public GameObject front;
    public GameObject back;
    public int tokenID;

    private bool isFlipped = false;

    public void OnClick()
    {
        if (isFlipped || GameManager.instance.lockInput) return;

        Flip();
        GameManager.instance.OnTokenFlipped(this);
    }

    public void Flip()
    {
        isFlipped = true;
        front.SetActive(true);
        back.SetActive(false);
    }

    public void FlipBack()
    {
        isFlipped = false;
        front.SetActive(false);
        back.SetActive(true);
    }
}
