using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] private GameObject loadingImageFront;
    [SerializeField] private GameObject loadingImageBack;
    [SerializeField] private List<Sprite> loadingImageList;
    [SerializeField] private float loadingImageBlendTime;
    [SerializeField] private float loadingImageShowingTime;

    private enum loadingImageState
    {
        showingImage,
        transitionImage,
    }

    private loadingImageState state;
    private float stateTimer;
    private bool isFront;

    private Image loadingImageFrontImage;
    private Image loadingImageBackImage;

    private int loadingImageCount;

    private void Start()
    {
        loadingImageCount = Random.Range(0, loadingImageList.Count);

        loadingImageFrontImage = loadingImageFront.GetComponent<Image>();
        loadingImageBackImage = loadingImageBack.GetComponent<Image>();
        loadingImageFrontImage.sprite = loadingImageList[loadingImageCount];
        loadingImageCount++;
        loadingImageBackImage.sprite = loadingImageList[loadingImageCount];
        loadingImageCount++;

        state = loadingImageState.showingImage;
        stateTimer = loadingImageShowingTime;
        isFront = true;
    }

    private void Update()
    {
        stateTimer -= Time.deltaTime;

        switch (state)
        {
            case loadingImageState.showingImage:
                break;
            case loadingImageState.transitionImage:
                if (isFront)
                {
                    loadingImageFrontImage.color =
                        new Color(loadingImageFrontImage.color.r, loadingImageFrontImage.color.g, loadingImageFrontImage.color.b, 1f - stateTimer / loadingImageBlendTime);
                    loadingImageBackImage.color =
                        new Color(loadingImageBackImage.color.r, loadingImageBackImage.color.g, loadingImageBackImage.color.b, stateTimer / loadingImageBlendTime);
                }
                
                if (!isFront)
                {
                    loadingImageFrontImage.color =
                        new Color(loadingImageFrontImage.color.r, loadingImageFrontImage.color.g, loadingImageFrontImage.color.b, stateTimer / loadingImageBlendTime);
                    loadingImageBackImage.color =
                        new Color(loadingImageBackImage.color.r, loadingImageBackImage.color.g, loadingImageBackImage.color.b, 1f - stateTimer / loadingImageBlendTime);
                }
                break;
        }

        if (stateTimer <= 0)
        {
            NextState();
        }
    }

    private void NextState()
    {
        switch (state)
        {
            case loadingImageState.showingImage:
                state = loadingImageState.transitionImage;
                isFront = !isFront;
                stateTimer = loadingImageBlendTime;
                break;
            case loadingImageState.transitionImage:
                state = loadingImageState.showingImage;
                ChangeLoadingImage();
                stateTimer = loadingImageShowingTime;
                break;
        }
    }

    private void ChangeLoadingImage()
    {
        if (loadingImageCount >= loadingImageList.Count)
        {
            loadingImageCount = 0;
        }

        if ( isFront )
        {
            loadingImageBackImage.sprite = loadingImageList[loadingImageCount];
            loadingImageCount++;
        }
        else
        {
            loadingImageFrontImage.sprite = loadingImageList[loadingImageCount];
            loadingImageCount++;
        }
    }
}
