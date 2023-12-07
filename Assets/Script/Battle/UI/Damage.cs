using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Damage : MonoBehaviour
{
    public float destroyTime = 1f;
    public float upSpeed = 2;
    TMP_Text damageText;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.Translate(0, upSpeed * Time.deltaTime, 0);
    }

    public void ShotDamage(int damage, GameObject targetObject)
    {
        //GameObject damageInstant = Instantiate(gameObject,new Vector3(0,2.3f,0),Quaternion.identity, targetObject.transform);
        GameObject damageInstant = Instantiate(gameObject,targetObject.transform);
        if (targetObject.CompareTag("Player"))
            damageInstant.transform.localPosition = new Vector3(0, 1f, 0);
        damageText = damageInstant.GetComponentInChildren<TMP_Text>();
        damageText.text = damage.ToString();
        Destroy(damageInstant, destroyTime);
    }
}
