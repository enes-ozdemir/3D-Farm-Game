using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryObject;
    [SerializeField] private GameObject shopPanel;
    public bool _isBagActive=false;
    public bool _isShopActive=false;
    [SerializeField] public Text goldText;
    [SerializeField] private PlayerManager playerManager;

    public void DisplayInventory()
    {
        if (_isBagActive)
        {
            inventoryObject.SetActive(true);
            _isBagActive = false;
        }
        else
        {
            inventoryObject.SetActive(false);
            _isBagActive = true;
        }
    }

    public void DisplayShop()
    {
        if (_isShopActive)
        {
            shopPanel.SetActive(false);
            _isShopActive = false;
            playerManager.isPlayerNotBlocked = true;
        }
        else
        {
            shopPanel.SetActive(true);
            _isShopActive = true;
            playerManager.isPlayerNotBlocked = false;
        }
    }

    private void Update()
    {
        //DisplayShop();
    }
}
