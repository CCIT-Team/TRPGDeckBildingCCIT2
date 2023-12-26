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

    public bool isdrawn = false;

    private void OnEnable()
    {
        if(!isdrawn)
            StartCoroutine(Draw());
    }

    IEnumerator Draw()
    {
        N_BattleManager.instance.IsAction = true;
        Debug.Log("Draw");
        transform.position = BattleUI.instance.cardStartTransform.position - new Vector3(transform.root.GetComponent<RectTransform>().position.x,0,0) ;
        Debug.Log("T:"+BattleUI.instance.cardStartTransform.position);
        Debug.Log("C:" + transform.position);
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
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f) + Vector3.one * Mathf.Lerp(0, 0.6f, Mathf.InverseLerp(Vector3.Distance(BattleUI.instance.cardStartTransform.position, BattleUI.instance.cardHighlightPosition.position), 0, Vector3.Distance(transform.position, BattleUI.instance.cardHighlightPosition.position)));
        }
        backSide.SetActive(false);
        foreach (GameObject part in FrontParts)
            part.SetActive(true);
        while (Vector3.Distance(transform.position, BattleUI.instance.cardHighlightPosition.position) >= distance)
        {
            yield return new WaitForSeconds(0.05f);
            transform.position = Vector3.Lerp(transform.position, BattleUI.instance.cardHighlightPosition.position, drawSpeed);
            transform.rotation = Quaternion.Euler(0, Mathf.Lerp(180, 0, Mathf.InverseLerp(Vector3.Distance(BattleUI.instance.cardStartTransform.position, BattleUI.instance.cardHighlightPosition.position), 0, Vector3.Distance(transform.position, BattleUI.instance.cardHighlightPosition.position))), 0);
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f) + Vector3.one * Mathf.Lerp(0, 0.6f, Mathf.InverseLerp(Vector3.Distance(BattleUI.instance.cardStartTransform.position, BattleUI.instance.cardHighlightPosition.position), 0, Vector3.Distance(transform.position, BattleUI.instance.cardHighlightPosition.position)));
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.localScale = Vector3.one * 1.3f;
        StartCoroutine(Highlight());
    }

    IEnumerator Highlight()
    {
        yield return new WaitForSeconds(highlightTime);
        StartCoroutine(ToHand());
    }

    IEnumerator ToHand()
    {
        while (Vector3.Distance(transform.position, transform.parent.position) >= distance)
        {
            yield return new WaitForSeconds(0.05f);
            transform.position = Vector3.Lerp(transform.position, transform.parent.position, handSpeed);
            transform.localScale = Vector3.one * Mathf.Lerp(1.3f, 1, Mathf.InverseLerp(Vector3.Distance(BattleUI.instance.cardHighlightPosition.position, transform.parent.position), 0, Vector3.Distance(transform.localPosition, Vector3.zero)));
        }
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        isdrawn = true;
        N_BattleManager.instance.IsAction = false;
    }
}
