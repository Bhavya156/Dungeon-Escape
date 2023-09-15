using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManger : MonoBehaviour
{
    private static UIManger _instance;

    public static UIManger Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("Instance is not found");
            }
            return _instance;
        }
    }

    public Text playerGemCountText;
    public Image selectionImage;
    public Text displayGemCount;
    public Image[] healthBars;

    public void ShopGemCount(int gemCount)
    {
        playerGemCountText.text = "" + gemCount + "G";
    }

    public void UpdateSelectionImage(int yPos)
    {
        selectionImage.rectTransform.anchoredPosition = new Vector2(selectionImage.rectTransform.anchoredPosition.x, yPos);
    }

    public void PlayerGemCount(int count) {
        displayGemCount.text = "" + count;
    }

    public void PlayerHealth(int livesRemaining)
    {
        for (int i = 0; i <= livesRemaining; i++)
        {   
            if (i == livesRemaining)
            {
                healthBars[i].enabled = false;
            }
        }
    }

    private void Awake()
    {
        _instance = this;
    }
}
