using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAnimation : MonoBehaviour
{
    public GameObject backSide;
    public GameObject[] FrontParts;

    public float distance = 1;
    public float drawSpeed = 0.25f;
    public float handSpeed = 0.3f;
    public float highlightTime = 1;

    private void OnEnable()
    {
        StartCoroutine(Draw());
    }

    IEnumerator Draw()
    {
        Debug.Log("Draw");
        transform.position = BattleUI.instance.cardStartTransform.position;
        transform.rotation = Quaternion.Euler(0, 180, 0);
        transform.localScale *= 0.7f;
        backSide.SetActive(true);
        foreach (GameObject part in FrontParts)
            part.SetActive(false);
        while(transform.rotation.y >= 0.65f)
        {
            yield return new WaitForSeconds(0.05f);
            transform.position = Vector3.Lerp(transform.position, BattleUI.instance.cardHighlightPosition.position, drawSpeed);
            transform.rotation = Quaternion.Euler(0, Mathf.Lerp(180,0,Mathf.InverseLerp(Vector3.Distance(BattleUI.instance.cardStartTransform.position, BattleUI.instance.cardHighlightPosition.position), 0, Vector3.Distance(transform.position, BattleUI.instance.cardHighlightPosition.position))), 0);
            transform.localScale = new Vector3(0.8f, 0.8f, 0.8f) + Vector3.one * Mathf.Lerp(0.2f, 0.6f, Mathf.InverseLerp(Vector3.Distance(BattleUI.instance.cardStartTransform.position, BattleUI.instance.cardHighlightPosition.position), 0, Vector3.Distance(transform.position, BattleUI.instance.cardHighlightPosition.position)));
        }
        backSide.SetActive(false);
        foreach (GameObject part in FrontParts)
            part.SetActive(true);
        while (Vector3.Distance(transform.position, BattleUI.instance.cardHighlightPosition.position) >= distance)
        {
            yield return new WaitForSeconds(0.05f);
            transform.position = Vector3.Lerp(transform.position, BattleUI.instance.cardHighlightPosition.position, drawSpeed);
            transform.rotation = Quaternion.Euler(0, Mathf.Lerp(180, 0, Mathf.InverseLerp(Vector3.Distance(BattleUI.instance.cardStartTransform.position, BattleUI.instance.cardHighlightPosition.position), 0, Vector3.Distance(transform.position, BattleUI.instance.cardHighlightPosition.position))), 0);
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f) + Vector3.one * Mathf.Lerp(0.2f, 0.6f, Mathf.InverseLerp(Vector3.Distance(BattleUI.instance.cardStartTransform.position, BattleUI.instance.cardHighlightPosition.position), 0, Vector3.Distance(transform.position, BattleUI.instance.cardHighlightPosition.position)));
            Debug.Log(Vector3.Distance(transform.position, BattleUI.instance.cardHighlightPosition.position));
        }
        Debug.Log("DrawEnd");
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.localScale = Vector3.one * 1.3f;
        StartCoroutine(Highlight());
    }

    IEnumerator Highlight()
    {
        Debug.Log("Highlight");
        yield return new WaitForSeconds(highlightTime);
        StartCoroutine(ToHand());
    }

    IEnumerator ToHand()
    {
        Debug.Log("GoHand");
        while (Vector3.Distance(transform.position, transform.parent.position) >= distance)
        {
            yield return new WaitForSeconds(0.05f);
            transform.position = Vector3.Lerp(transform.position, transform.parent.position, handSpeed);
            transform.localScale = Vector3.one * Mathf.Lerp(1.3f, 1, Mathf.InverseLerp(Vector3.Distance(BattleUI.instance.cardHighlightPosition.position, transform.parent.position), 0, Vector3.Distance(transform.localPosition, Vector3.zero)));
        }
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
    }
}
