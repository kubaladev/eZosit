using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StaticPanel : MonoBehaviour
{
    public TMP_Text friendTxt;
    public void SetupPanel(ItemSlot go)
    {
        UpdateFriendTxt(go);
    }
    public void UpdateFriendTxt(ItemSlot GO)
    {
        if (GO.pair == null)
        {
            Color c;
            ColorUtility.TryParseHtmlString("#C3C3C3", out c);
            friendTxt.text = "nepriradené";
            friendTxt.color = c;
        }
        else
        {
            Color c;
            ColorUtility.TryParseHtmlString("#9CF175", out c);
            friendTxt.text = GO.objectName.Replace("Miesto: ","");
            friendTxt.color = c;
        }
    }
}
