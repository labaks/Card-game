using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public List<GameObject> cards = new List<GameObject>();
    public CurrentDeck currentDeck;
    public int handLength = 4;
    void Start()
    {
        currentDeck = gameObject.GetComponent<CurrentDeck>();
        fillHand();
    }

    public void fillHand()
    {
        for (int i = 0; i < handLength; i++)
        {
            DrawCard();
        }
    }

    public void removeCardFromHand(GameObject card)
    {
        cards.Remove(card);
    }

    public void addCardToHand(GameObject card)
    {
        cards.Add(card);
    }

    public void DrawCard()
    {
        if (currentDeck.deck.Count > 0)
        {
            int cardPlace = Random.Range(0, currentDeck.deck.Count);
            GameObject card = Instantiate(currentDeck.deck[cardPlace], new Vector3(0, 0, 0), Quaternion.identity);
            currentDeck.removeCardFromDeck(currentDeck.deck[cardPlace]);
            card.transform.SetParent(transform, false);
            addCardToHand(card);
        }
    }
}
