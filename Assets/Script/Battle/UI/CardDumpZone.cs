using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDumpZone : MonoBehaviour
{
    Image image;

    RectTransform rectTransform;
    Rect rect;

    CardUI dumpCard;
    RectTransform cardRectTransform;
    Rect cardRect;
    private void Awake()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        rect = new Rect(rectTransform.position,rectTransform.rect.size);
        image.color = new Color(1, 1, 1, 0.5f);
    }

    void Update()
    {
        if(cardRectTransform != null)
            cardRect = new Rect(cardRectTransform.position, cardRectTransform.rect.size);
        if (rect.Overlaps(cardRect))
        {
            dumpCard.isDumping = true;
            image.color = new Color(0.85f, 0.1f, 0.1f,0.5f);
        }
        else if(cardRectTransform != null)
        {
            dumpCard.isDumping = false;
            image.color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            image.color = new Color(1, 1, 1, 0.5f);
        }
    }

    public void SetCardRect(GameObject card = null)
    {
        if (card == null)
            cardRectTransform = null;
        else
        {
            cardRectTransform = card.GetComponent<RectTransform>();
            dumpCard = card.GetComponent<CardUI>();
        }
            
    }
}
