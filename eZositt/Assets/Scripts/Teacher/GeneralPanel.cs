using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class GeneralPanel : MonoBehaviour
{
    public TMP_InputField vyskaInp;
    public TMP_InputField sirkaInp;
    private RectTransform selectedRT;
    public TMP_InputField percentInp;
    public TMP_InputField rotationInp;
    public RawImage rawImage;
    bool holdingPercentBtn = false;
    bool upscale = false;
    public void SetupPanel(RectTransform rect, RawImage ri,ObjectT objT)
    {
        this.objT = objT;
        selectedRT = rect;
        sirkaInp.text = "";
        vyskaInp.text = "";
        sirkaInp.text = Mathf.RoundToInt(rect.rect.width).ToString();
        vyskaInp.text = Mathf.RoundToInt(rect.rect.height).ToString();
        percentInp.text=ScaleToPercent(selectedRT.localScale.x).ToString();
        rotationInp.text = Mathf.RoundToInt(selectedRT.localEulerAngles.z).ToString();
        rawImage = ri;

    }
    private ObjectT objT;
    public void Delete()
    {
        objT.FadeOut();
        HidePanel();
    }
    public void Duplicate()
    {
        objT.Duplicate();
    }
    public void HidePanel()
    {
        ObjectModificator.Instance.DeactivatePanels();
    }
    public void UpdateRectSizeFromText()
    {
        if(vyskaInp.text.Length>0 && sirkaInp.text.Length > 0)
        {
            selectedRT.sizeDelta= new Vector2(float.Parse(sirkaInp.text), float.Parse(vyskaInp.text));
        }
    }
    public void UpdateScaleFromText()
    {
        if (percentInp.text.Length > 0)
        {
            float scale = PercentToScale(int.Parse(percentInp.text));
            selectedRT.localScale = new Vector3(scale, scale, 1);
        }


    }
    public void UpdateRotationFromText()
    {
        if(rotationInp.text.Length > 0)
        {
            selectedRT.localEulerAngles=new Vector3(selectedRT.localEulerAngles.x, selectedRT.localEulerAngles.y, int.Parse(rotationInp.text));
        }

    }
    private int ScaleToPercent(float scale)
    {
        int percent=Mathf.RoundToInt(scale * 100);
        return percent;
    }
    private void FixedUpdate()
    {
        if (holdingPercentBtn)
        {
            
            if (percentInp.text.Length > 0)
            {
                int value = Int32.Parse(percentInp.text);
                if (upscale)
                {
                    if (value < 999)
                    {
                        value++;
                    }       
                }
                else
                {
                    if (value > 0)
                        value--;
                }
                percentInp.text = value.ToString();
            }
            else
            {
                percentInp.text = "0";
            }
        }
    }
    private float PercentToScale(int percent)
    {
        float scale= (float)percent/100;
        return scale;
    }
    public void Upscale(bool isOn)
    {
        upscale = true;
        holdingPercentBtn = isOn;
    }
    public void Downscale(bool isOn)
    {
        upscale = false;
        holdingPercentBtn = isOn;
    }
    public void FlipHorizontal()
    {
        if (rawImage.uvRect.x == 0)
        {
            Debug.Log(rawImage.uvRect);
            rawImage.uvRect = new Rect(1, rawImage.uvRect.y, -1, rawImage.uvRect.height);
        }
        else
        {
            rawImage.uvRect=new Rect(0, rawImage.uvRect.y, 1, rawImage.uvRect.height);
        }
        Debug.Log(rawImage.uvRect);
    }
    public void FlipVertical()
    {
        if (rawImage.uvRect.y == 0)
        {
            Debug.Log(rawImage.uvRect);
            rawImage.uvRect = new Rect(rawImage.uvRect.x,1, rawImage.uvRect.width,-1);
        }
        else
        {
            rawImage.uvRect = new Rect(rawImage.uvRect.x,0 , rawImage.uvRect.width, 1);
        }
    }
    public void ZeroRotation()
    {
        if (rotationInp.text.Length == 0)
        {
            rotationInp.text = "0";
        }
    }

}
