using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendlyZone : MonoBehaviour
{
    public List<GameObject> cards = new List<GameObject>();
    public List<GameObject> cells = new List<GameObject>();
    public Text zonePoints;

    public GameObject mainCanvas;
    private Game game;
    public enum FieldColor
    {
        None, Red, Green, Blue
    }
    public FieldColor fieldColor;

    public bool canAddCard = true;

    private void Awake()
    {
        game = mainCanvas.GetComponent<Game>();
    }

    public void receiveCard(GameObject card)
    {
        if (cards.Count < 4)
        {
            cards.Add(card);
            game.tempCards.Add(card);

            placeCard(card);
        }
        if (cards.Count == 4) canAddCard = false;
    }

    public void placeCard(GameObject card)
    {
        int index = cards.IndexOf(card);
        GameObject parentCell = cells[index];
        card.transform.SetParent(parentCell.transform, false);
        RectTransform cardRect = card.GetComponent<RectTransform>();
        float scale = parentCell.GetComponent<RectTransform>().sizeDelta.x / cardRect.sizeDelta.x;
        card.transform.localPosition = new Vector3(0, 0, 0);
        cardRect.localScale = new Vector3(scale, scale, scale);
    }

    public void updateZonePoints()
    {
        int points = 0;
        for (int i = 0; i < cards.Count; i++)
        {
            int power = cards[i].gameObject.GetComponent<Card>().potencial;
            points += power;
        }
        zonePoints.text = points.ToString();
    }
}
