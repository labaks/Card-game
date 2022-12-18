using UnityEngine;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour
{
    private GameObject Canvas;
    private bool isDragging = false;
    private bool isOverDropZone = false;
    public bool canDrag = true;
    private GameObject dropZone;
    private GameObject startParent;
    private Vector2 startPosition;
    private Game game;
    private Card thisCard;

    Color tempColor;

    void Awake()
    {
        Canvas = GameObject.Find("Main Canvas");
        game = Canvas.GetComponent<Game>();
        thisCard = gameObject.GetComponent<Card>();
    }

    void Update()
    {
        if (isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.SetParent(Canvas.transform, true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        FriendlyZone friendlyZone = collision.gameObject.GetComponent<FriendlyZone>();
        if (friendlyZone.canAddCard)
        {
            Image zoneBg = collision.gameObject.GetComponent<Image>();
            tempColor = zoneBg.color;
            tempColor.a = 1f;
            zoneBg.color = tempColor;

            thisCard.goToField(friendlyZone.fieldColor);

            isOverDropZone = true;
            dropZone = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        tempColor.a = .3f;
        collision.gameObject.GetComponent<Image>().color = tempColor;
        isOverDropZone = false;
        dropZone = null;
        thisCard.goOutField();
    }

    public void StartDrag()
    {
        if (canDrag && game.currentMana >= thisCard.manaCost)
        {
            startParent = transform.parent.gameObject;
            startPosition = transform.position;
            isDragging = true;
        }
    }

    public void EndDrag()
    {
        if (canDrag && isDragging)
        {
            isDragging = false;
            if (isOverDropZone)
            {
                dropZone.GetComponent<FriendlyZone>().receiveCard(gameObject);
                game.currentMana -= thisCard.manaCost;
                game.updateManaCounter();
                canDrag = false;
                tempColor.a = .3f;
                dropZone.GetComponent<Image>().color = tempColor;
                isOverDropZone = false;
            }
            else
            {
                transform.position = startPosition;
                transform.SetParent(startParent.transform, false);
            }
        }
    }
}
