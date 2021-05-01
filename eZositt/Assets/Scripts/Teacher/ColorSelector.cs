using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions.ColorPicker;

public class ColorSelector : MonoBehaviour
{
    public InputField colorInput;
    RawImage img;
    Color baseColor;
    public void SetupPanel(RawImage img)
    {
        this.img = img;
        baseColor = img.color;
        colorInput.text = "#" + ColorUtility.ToHtmlStringRGB(img.color);
        colorInput.GetComponent<HexColorField>().UpdateColor(colorInput.text);
    }
    public void UpdateImgColor()
    {
        Color c;
        ColorUtility.TryParseHtmlString(colorInput.text, out c);
        img.color = c;
    }
    public void RedoColor()
    {
        img.color = baseColor;
    }
}
