using UnityEngine;

public class CardZoom : MonoBehaviour
{
    private GameObject Canvas;
    private GameObject zoomCard;

    void Awake()
    {
        Canvas = GameObject.Find("Main Canvas");
    }

    public void OnHoverEnter()
    {
        zoomCard = Instantiate(gameObject, new Vector2(Input.mousePosition.x, Input.mousePosition.y + 200), Quaternion.identity);
        zoomCard.GetComponent<Collider2D>().enabled = false;
        zoomCard.transform.SetParent(Canvas.transform, false);
        zoomCard.layer = LayerMask.NameToLayer("Zoom");

        RectTransform rect = zoomCard.GetComponent<RectTransform>();
        rect.localScale = new Vector3(2, 2, 2);
        rect.sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x, gameObject.GetComponent<RectTransform>().sizeDelta.y);
    }

    public void OnHoverExit()
    {
        Destroy(zoomCard);
    }
}
