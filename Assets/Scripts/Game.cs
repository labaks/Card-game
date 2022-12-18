using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public int manaPoints = 0;
    public int currentMana = 0;
    public int turnsCount = 5;
    public int currentTurn;
    public Text manaCounter;
    public GameObject Hand;
    public List<GameObject> friendlyZones = new List<GameObject>();
    private Hand hand;

    public GameObject currentTurnPanel, endGamePanel;
    public Text currentTurnPanelCounter;

    public List<GameObject> tempCards = new List<GameObject>();

    public enum GameState
    {
        PlayingCards, RevealingCards
    }

    public GameState currentState = GameState.PlayingCards;
    private void Awake()
    {
        endGamePanel.SetActive(false);
        hand = Hand.GetComponent<Hand>();
        manaPoints = currentMana = currentTurn = 1;
        updateManaCounter();
        showStartTurn(currentTurn);
    }

    public void endTurn()
    {
        currentState = GameState.RevealingCards;
        revealingCards();
    }

    public void revealingCards()
    {
        for (int i = 0; i < tempCards.Count; i++)
        {
            StartCoroutine(tempCards[i].GetComponent<Card>().revealAnimation());
        }
        foreach (GameObject zone in friendlyZones)
        {
            zone.GetComponent<FriendlyZone>().updateZonePoints();
        }
        tempCards.Clear();

        if (currentTurn < turnsCount)
        {
            currentState = GameState.PlayingCards;
            currentTurn++;
            manaPoints = currentMana = currentTurn;
            updateManaCounter();
            showStartTurn(currentTurn);
            hand.DrawCard();
        }
        else
        {
            endGame();
        }
    }

    public void undoActions()
    {
        for (int i = 0; i < tempCards.Count; i++)
        {
            GameObject card = tempCards[i];
            GameObject prevParent = card.transform.parent.parent.parent.gameObject;
            prevParent.GetComponent<FriendlyZone>().cards.Remove(card.gameObject);
            prevParent.GetComponent<FriendlyZone>().canAddCard = true;
            card.transform.SetParent(Hand.transform, false);
            card.GetComponent<Card>().goOutField();
            card.GetComponent<DragDrop>().canDrag = true;
            RectTransform cardRect = card.GetComponent<RectTransform>();
            card.transform.localPosition = new Vector3(0, 0, 0);
            cardRect.localScale = new Vector3(1, 1, 1);
        }
        tempCards.Clear();
        currentMana = currentTurn;
        updateManaCounter();
    }

    public void showStartTurn(int turn)
    {
        currentTurnPanel.SetActive(true);
        currentTurnPanelCounter.text = currentTurn.ToString();
        StartCoroutine(hideStartTurn());
    }

    IEnumerator hideStartTurn()
    {
        yield return new WaitForSeconds(2);
        currentTurnPanel.SetActive(false);
    }

    IEnumerator waitCardReveal()
    {
        yield return new WaitForSeconds(3);
    }

    public void updateManaCounter()
    {
        manaCounter.text = currentMana.ToString();
    }

    private void endGame()
    {
        endGamePanel.SetActive(true);
        Debug.Log("END GAME");
    }
}
