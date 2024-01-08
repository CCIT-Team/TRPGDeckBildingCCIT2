using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAnimationControl : UnitAnimationControl
{
    public float startHeight = 10;
    public float landSpeed = 0.4f;
    GameObject BreathParticle;
    void Start()
    {
        defaultPosition = new Vector3(0,0,0);
        transform.position += new Vector3(0, startHeight, 0);
        StartCoroutine(Landing());
        BreathParticle = transform.GetChild(transform.childCount -2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Landing()
    {
        while(transform.position.y >= 2f)
        {
            yield return new WaitForSeconds(0.02f);
            transform.Translate(Vector3.down * landSpeed);
        }
        transform.localPosition = new Vector3(transform.localPosition.x, 2, transform.localPosition.z);
        animator.SetTrigger("Landing");
        while (transform.localPosition.y >= 0)
        {
            yield return new WaitForSeconds(0.02f);
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0,0,0), 0.1f * landSpeed);
        }
        transform.localPosition = new Vector3(transform.localPosition.x, 0,transform.localPosition.z);
    }

    public new void SetAnimator()
    {
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/In Battle/Dragon_Battle");
        animator.avatar = Resources.Load<GameObject>("Prefabs/Monster/Dragon_Battle").GetComponent<Animator>().avatar;
        particleAndSound = Resources.Load<BattleParticleAndSound>("SFX & BGM/SFX/Battle/Combat_Dragon");

        if (TryGetComponent<AudioSource>(out audioSource))
            return;
        else
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.volume = 0.8f;
        }
    }

    public void DragonBreath(int i)
    {
        if(i != 0)
            BreathParticle.SetActive(true);
        else
            BreathParticle.SetActive(false);

    }
}
