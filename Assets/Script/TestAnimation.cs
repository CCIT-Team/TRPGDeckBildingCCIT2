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
    [SerializeField] private bool changeRaceType;
    [SerializeField][Range(1, 3)] private int classType;
    [SerializeField] private bool changeEyeColor;
    [SerializeField] private bool changeSkinColor;

    [Header("Others")]
    [SerializeField] private Animator animator2;

    private Animator animator;
    private SkinnedMeshRenderer raceTarget;
    private List<SkinnedMeshRenderer> sexTarget;
    private int raceType;
    private Material eyeMaterial;
    private Material skinMaterial;
    private const float offset = -0.015625f;

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

        eyeMaterial = characterModel.GetComponent<SkinnedMeshRenderer>().materials[2];
        skinMaterial = characterModel.GetComponent<SkinnedMeshRenderer>().materials[1];

        raceType = 1;
    }

    private void Update()
    {
        transform.eulerAngles = new Vector3 (0f, -65f + Mathf.PingPong(Time.time * 15f, 130f), 0f);

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
        if (changeRaceType)
        {
            raceType++;
            if (raceType == 5)
            {
                raceType = 1;
            }

            if (raceType == 1)
            {
                raceTarget.SetBlendShapeWeight(1, 0f);
                raceTarget.SetBlendShapeWeight(2, 0f);
                skinMaterial.mainTextureOffset = new Vector2(0, offset * 0f);
            }
            else if (raceType == 2)
            {
                raceTarget.SetBlendShapeWeight(1, 100f);
                raceTarget.SetBlendShapeWeight(2, 0f);
                skinMaterial.mainTextureOffset = new Vector2(0, offset * 4f);
            }
            else if (raceType == 3)
            {
                raceTarget.SetBlendShapeWeight(1, 100f);
                raceTarget.SetBlendShapeWeight(2, 0f);
                skinMaterial.mainTextureOffset = new Vector2(0, offset * 7f);
            }
            else if (raceType == 4)
            {
                raceTarget.SetBlendShapeWeight(1, 0f);
                raceTarget.SetBlendShapeWeight(2, 100f);
                skinMaterial.mainTextureOffset = new Vector2(0, offset * 11f);
            }

            changeRaceType = false;
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

        if (changeEyeColor)
        {
            eyeMaterial.mainTextureOffset += new Vector2(0, offset);
            if (eyeMaterial.mainTextureOffset.y <= -0.1875f)
            {
                eyeMaterial.mainTextureOffset = new Vector2(0, 0);
            }
            changeEyeColor = false;
        }

        if (changeSkinColor)
        {
            skinMaterial.mainTextureOffset += new Vector2(0, offset);
            if (raceType == 1 && skinMaterial.mainTextureOffset.y < -0.046875f)
            {
                skinMaterial.mainTextureOffset = new Vector2(0, 0);
            }
            if (raceType == 2 && skinMaterial.mainTextureOffset.y < -0.09375f)
            {
                skinMaterial.mainTextureOffset = new Vector2(0, 0);
            }
            if (raceType == 3 && skinMaterial.mainTextureOffset.y < -0.140625f)
            {
                skinMaterial.mainTextureOffset = new Vector2(0, 0);
            }
            if (raceType == 4 && skinMaterial.mainTextureOffset.y < -0.21875f)
            {
                skinMaterial.mainTextureOffset = new Vector2(0, 0);
            }
            changeSkinColor = false;
        }
    }
}
