using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class CardSpawner : MonoBehaviour
{
    public GameObject cardPrefab;  // Assign your card prefab in the inspector
    public Transform gridParent;   // Assign your CardGrid object in the inspector
    public int totalCards = 24;    // Must be even
    public Sprite[] frontImages;  // Assign 12 unique sprites in the Inspector

    void Start()
    {
        if (totalCards % 2 != 0)
        {
            Debug.LogError("Total cards must be even!");
            return;
        }

        if (totalCards % 2 != 0 || frontImages.Length < totalCards / 2)
        {
            Debug.LogError("Mismatch: Not enough frontImages for the number of token pairs.");
            return;
        }

        // Step 1: Create list of IDs, each ID appearing twice
        List<int> tokenIDs = new List<int>();
        for (int i = 0; i < totalCards / 2; i++)
        {
            tokenIDs.Add(i);
            tokenIDs.Add(i);
        }

        // Step 2: Shuffle the list
        Shuffle(tokenIDs);

        // Step 3: Instantiate and assign ID
        for (int i = 0; i < totalCards; i++)
        {
            GameObject card = Instantiate(cardPrefab, gridParent);
            TokenController token = card.GetComponent<TokenController>();

            int id = tokenIDs[i];
            token.tokenID = id;

            // Set the front sprite
            Image frontImage = token.front.GetComponent<Image>();
            frontImage.sprite = frontImages[id];
        }
    }

    // Fisher-Yates Shuffle
    void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = UnityEngine.Random.Range(i, list.Count);
            int temp = list[i];
            list[i] = list[rand];
            list[rand] = temp;
        }
    }
}
