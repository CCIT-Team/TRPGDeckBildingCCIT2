using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAnimation : MonoBehaviour
{
    public GameObject backSide;

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
        yield return new WaitUntil(() => transform.parent.name == "DrawnCard");
        transform.position = BattleUI.instance.cardStartTransform.position;
        Debug.Log("T:"+BattleUI.instance.cardStartTransform.position);
        Debug.Log("C:" + transform.position);
        transform.localRotation = Quaternion.Euler(0, 180, 0);
        transform.localScale *= 0.3f;
        backSide.SetActive(true);

        while(transform.rotation.y >= 0.65f)
        {
            yield return new WaitForSeconds(0.05f);
            transform.position = Vector3.Lerp(transform.position, BattleUI.instance.cardHighlightPosition.position, drawSpeed);
            transform.localRotation = Quaternion.Euler(0, Mathf.Lerp(180,0,Mathf.InverseLerp(Vector3.Distance(BattleUI.instance.cardStartTransform.position, BattleUI.instance.cardHighlightPosition.position), 0, Vector3.Distance(transform.position, BattleUI.instance.cardHighlightPosition.position))), 0);
            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f) + Vector3.one * Mathf.Lerp(0, 1f, Mathf.InverseLerp(Vector3.Distance(BattleUI.instance.cardStartTransform.position, BattleUI.instance.cardHighlightPosition.position), 0, Vector3.Distance(transform.position, BattleUI.instance.cardHighlightPosition.position)));
        }
        backSide.SetActive(false);

        while (Vector3.Distance(transform.position, BattleUI.instance.cardHighlightPosition.position) >= distance)
        {
            yield return new WaitForSeconds(0.05f);
            transform.position = Vector3.Lerp(transform.position, BattleUI.instance.cardHighlightPosition.position, drawSpeed);
            transform.localRotation = Quaternion.Euler(0, Mathf.Lerp(180, 0, Mathf.InverseLerp(Vector3.Distance(BattleUI.instance.cardStartTransform.position, BattleUI.instance.cardHighlightPosition.position), 0, Vector3.Distance(transform.position, BattleUI.instance.cardHighlightPosition.position))), 0);
            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f) + Vector3.one * Mathf.Lerp(0, 1f, Mathf.InverseLerp(Vector3.Distance(BattleUI.instance.cardStartTransform.position, BattleUI.instance.cardHighlightPosition.position), 0, Vector3.Distance(transform.position, BattleUI.instance.cardHighlightPosition.position)));
        }
        transform.localRotation = Quaternion.Euler(0, 0, 0);
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
        yield return new WaitForSeconds(0.1f);
        N_BattleManager.instance.IsAction = false;
    }
}
