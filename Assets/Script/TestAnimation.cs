using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimation : MonoBehaviour
{
    [Header("Models")]
    [SerializeField] private Transform characterModel;
    [SerializeField] private Transform fighter;
    [SerializeField] private Transform cleric;
    [SerializeField] private Transform wizard;

    [Header("Controller")]
    [SerializeField][Range(1, 2)] private int sexType;
    [SerializeField][Range(1, 3)] private int raceType;
    [SerializeField][Range(1, 3)] private int classType;

    [Header("Others")]
    [SerializeField] private Animator animator2;

    private Animator animator;
    private SkinnedMeshRenderer raceTarget;
    private List<SkinnedMeshRenderer> sexTarget;

    private void Start()
    {
        animator = GetComponent<Animator>();

        raceTarget = characterModel.GetComponent<SkinnedMeshRenderer>();

        sexTarget = new List<SkinnedMeshRenderer>();
        SkinnedMeshRenderer fighterMeshRenderer = fighter.GetComponent<SkinnedMeshRenderer>();
        SkinnedMeshRenderer clericMeshRenderer = cleric.GetComponent<SkinnedMeshRenderer>();
        SkinnedMeshRenderer wizardMeshRenderer = wizard.GetComponent<SkinnedMeshRenderer>();
        sexTarget.Add(raceTarget);
        sexTarget.Add(fighterMeshRenderer);
        sexTarget.Add(clericMeshRenderer);
        sexTarget.Add(wizardMeshRenderer);

        fighter.gameObject.SetActive(true);
        cleric.gameObject.SetActive(false);
        wizard.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("attack");
            animator2.SetTrigger("attack");
        }

        if (sexType == 1)
        {
            foreach (SkinnedMeshRenderer target in sexTarget)
            {
                target.SetBlendShapeWeight(0, 0f);
            }
        }

        if (sexType == 2)
        {
            foreach (SkinnedMeshRenderer target in sexTarget)
            {
                target.SetBlendShapeWeight(0, 100f);
            }
        }

        if (raceType == 1)
        {
            raceTarget.SetBlendShapeWeight(1, 0f);
            raceTarget.SetBlendShapeWeight(2, 0f);
        }
        else if (raceType == 2)
        {
            raceTarget.SetBlendShapeWeight(1, 100f);
            raceTarget.SetBlendShapeWeight(2, 0f);
        }
        else if (raceType == 3)
        {
            raceTarget.SetBlendShapeWeight(1, 0f);
            raceTarget.SetBlendShapeWeight(2, 100f);
        }

        if (classType == 1)
        {
            animator.SetInteger("fighter", 1);
            animator2.SetInteger("fighter", 1);
            fighter.gameObject.SetActive(true);
            cleric.gameObject.SetActive(false);
            wizard.gameObject.SetActive(false);
        }
        else if (classType == 2)
        {
            animator.SetInteger("cleric", 2);
            animator2.SetInteger("cleric", 2);
            fighter.gameObject.SetActive(false);
            cleric.gameObject.SetActive(true);
            wizard.gameObject.SetActive(false);
        }
        else if (classType == 3)
        {
            animator.SetInteger("wizard", 3);
            animator2.SetInteger("wizard", 3);
            fighter.gameObject.SetActive(false);
            cleric.gameObject.SetActive(false);
            wizard.gameObject.SetActive(true);
        }
    }
}
