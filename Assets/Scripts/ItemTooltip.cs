using System.Net.NetworkInformation;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
    [SerializeField] private Text itemNameText;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Text itemValueText;

    public void ShowTooltip(Item item)
    {
        itemNameText.text = item.itemName;

        itemValueText.text = item.value.ToString();
        itemIcon.sprite = item.icon;

        gameObject.SetActive(true);

    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
  
}