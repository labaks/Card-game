using System.Collections.Generic;
using UnityEngine;

public class CurrentDeck : MonoBehaviour
{
    public List<GameObject> allCards = new List<GameObject>();
    public List<GameObject> deck = new List<GameObject>();
    int deckLength = 10;
    void Start()
    {
        instantiateCurrentDeck();
    }

    void instantiateCurrentDeck()
    {
        for (int i = 0; i < deckLength; i++)
        {
            deck.Add(allCards[Random.Range(0, allCards.Count)]);
        }
    }

    public void removeCardFromDeck(GameObject card)
    {
        deck.Remove(card);
    }
}
