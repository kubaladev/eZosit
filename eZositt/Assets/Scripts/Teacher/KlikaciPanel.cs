using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KlikaciPanel : MonoBehaviour
{
    char[] pismena = new char[] { 'A', 'B', 'C', 'D','E','F','G','H' };
    public TMP_Text nameTxt;
    public Toggle toggle;
    public TMP_Dropdown dropdown;
    private ClickableObject clickableObject;
    private ObjectT objectT;
    public Texture2D basicTexture;
    public TMP_Text correctText;
    public List<string> GetListOfLengthX(int x)
    {
        List<string> options = new List<string>();
        for(int i = 0; i < x; i++)
        {
            options.Add("Strana " + pismena[i]);
        }
        return options;
    }
    public void SetupPanel(ClickableObject clickableObject, ObjectT objectT)
    {
        this.objectT = objectT;
        this.clickableObject = clickableObject;
        UpdatePanel();
    }
    public void SaveTexture()
    {
        clickableObject.imgFace[dropdown.value]=(Texture2D)clickableObject.img.texture;
    }
    public void ChangeTexture()
    {
        clickableObject.img.texture = clickableObject.imgFace[dropdown.value];
        clickableObject.currentId = dropdown.value;
        UpdateText();
    }
    public void UpdatePanel()
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(GetListOfLengthX(clickableObject.imgFace.Count));
        dropdown.SetValueWithoutNotify(clickableObject.currentId);
        UpdateText();

    }

    public void AddTexture()
    {
        if (dropdown.options.Count < 8)
        {
            clickableObject.AddImage(basicTexture);
            dropdown.ClearOptions();
            dropdown.AddOptions(GetListOfLengthX(clickableObject.imgFace.Count));
            dropdown.value = dropdown.options.Count - 1;
            clickableObject.currentId = dropdown.value;
            UpdateText();
        }

    }
    public void RemoveTexture()
    {
        if (dropdown.options.Count > 1)
        {
            if (clickableObject.correctId > dropdown.value)
            {
                clickableObject.correctId--;
            }
            else if (clickableObject.correctId == dropdown.value)
            {
                clickableObject.correctId=999;
            }
            clickableObject.RemoveImage(dropdown.value);
            dropdown.ClearOptions();
            dropdown.AddOptions(GetListOfLengthX(clickableObject.imgFace.Count));
            dropdown.value = dropdown.options.Count - 1;
            clickableObject.currentId = dropdown.value;
            UpdateText();
        }

    }
    public void UpdateText()
    {
        if (clickableObject.correctId == dropdown.value)
        {
            Color c;
            ColorUtility.TryParseHtmlString("#9CF175", out c);
            correctText.text = "-správna-";
            correctText.color = c;
        }
        else
        {
            Color c;
            ColorUtility.TryParseHtmlString("#C3C3C3", out c);
            correctText.text = "-nesprávna-";
            correctText.color = c;
        }
    }
    public void SetCorrect()
    {
        clickableObject.correctId = dropdown.value;
        UpdateText();
    }


}
