using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;
    private int currentSelectedItem;
    private int selectedItemCost;
    private Player player;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
        {
            player = other.GetComponent<Player>();
            UIManger.Instance.ShopGemCount(player.diamonds);
            shopPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player")
        {
            shopPanel.SetActive(false);
        }
    }

    public void SelectionImage(int item)
    {
        switch (item)
        {
            case 0:
                UIManger.Instance.UpdateSelectionImage(75);
                currentSelectedItem = 0;
                selectedItemCost = 200;
                break;
            case 1:
                UIManger.Instance.UpdateSelectionImage(-35);
                currentSelectedItem = 1;
                selectedItemCost = 400;
                break;
            case 2:
                UIManger.Instance.UpdateSelectionImage(-130);
                currentSelectedItem = 2;
                selectedItemCost = 100;
                break;
        }
    }

    public void BuyItem()
    {
        if (player.diamonds >= selectedItemCost)
        {
            //award
            if (currentSelectedItem == 2)
            {
                GameManager.Instance.HasKey = true;
            }
            player.diamonds -= selectedItemCost;
            shopPanel.SetActive(false);
        }
        else
        {
            Debug.Log("Dont have enough creds. Come back later");
            shopPanel.SetActive(false);
        }
    }
}
