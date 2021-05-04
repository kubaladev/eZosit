using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActivePanel : MonoBehaviour
{
    public TMP_Text friendTxt;
    public GeneratedObject GO;
    public void SetupPanel(GeneratedObject GO)
    {
        this.GO = GO;
        UpdateFriendTxt();
    }
    public void UpdateFriendTxt()
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
            friendTxt.text = "vytvorený";
            friendTxt.color = c;
        }
    }
}
