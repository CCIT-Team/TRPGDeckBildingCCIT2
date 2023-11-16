using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardDisplay : MonoBehaviour
{
    public Image image;
    public Text rewardName;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GettReward()
    {
        Debug.Log("를 받았다");
    }

    void DumpReward()
    {
        gameObject.SetActive(false);
    }
}
