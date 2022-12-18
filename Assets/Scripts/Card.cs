using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int id = 0;
    public string cardName;
    public int redPower, greenPower, bluePower, manaCost;
    public GameObject redCell, greenCell, blueCell;
    public int potencial = 0;
    public Text cardTitle, manaCostText;
    private Text redPoints, greenPoints, bluePoints;
    private GameObject redShadow, greenShadow, blueShadow;
    public FriendlyZone.FieldColor currentColor = FriendlyZone.FieldColor.None;
    public Animator animator;

    void Start()
    {
        cardTitle.text = cardName;
        manaCostText.text = manaCost.ToString();

        redPoints = GetChildByName(redCell, "Points").GetComponent<Text>();
        redPoints.text = redPower.ToString();
        redShadow = GetChildByName(redCell, "Shadow");

        greenPoints = GetChildByName(greenCell, "Points").GetComponent<Text>();
        greenPoints.text = greenPower.ToString();
        greenShadow = GetChildByName(greenCell, "Shadow");

        bluePoints = GetChildByName(blueCell, "Points").GetComponent<Text>();
        bluePoints.text = bluePower.ToString();
        blueShadow = GetChildByName(blueCell, "Shadow");
    }

    public void goToField(FriendlyZone.FieldColor color)
    {
        switch (color)
        {
            case FriendlyZone.FieldColor.Red:
                redShadow.SetActive(false);
                greenShadow.SetActive(true);
                blueShadow.SetActive(true);
                potencial = redPower;
                break;
            case FriendlyZone.FieldColor.Green:
                redShadow.SetActive(true);
                greenShadow.SetActive(false);
                blueShadow.SetActive(true);
                potencial = greenPower;
                break;
            case FriendlyZone.FieldColor.Blue:
                redShadow.SetActive(true);
                greenShadow.SetActive(true);
                blueShadow.SetActive(false);
                potencial = bluePower;
                break;
            default: break;
        }
        currentColor = color;
    }

    public void goOutField()
    {
        redShadow.SetActive(false);
        greenShadow.SetActive(false);
        blueShadow.SetActive(false);
        currentColor = FriendlyZone.FieldColor.None;
        potencial = 0;
    }

    public void reveal()
    {
        animator.SetTrigger("Reveal");
        // StartCoroutine(revealAnimation());
    }

    public IEnumerator revealAnimation()
    {
        animator.SetTrigger("Reveal");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
    }

    private GameObject GetChildByName(GameObject parent, string childName)
    {
        Transform parentTransform = parent.transform;
        Transform childTransform = parentTransform.Find(childName);
        if (childTransform != null)
        {
            return childTransform.gameObject;
        }
        else
        {
            return null;
        }
    }
}
