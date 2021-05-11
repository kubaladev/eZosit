using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Galeria : MonoBehaviour
{
    // Start is called before the first frame update
    public Texture2D[] zvierata;
    public Texture2D[] enviroment;
    public Texture2D[] priroda;
    public Texture2D[] zvierataBack;
    public Texture2D[] hmyz;
    public Texture2D[] voda;
    public Texture2D[] selectedArray;
    public ObjectT objectT;
    public CanvasGroup galeriaPanel;
    public GaleriaBtn[] galBtns;
    public GeneratedObject generatedObject;
    int counter = 0;
    public void SetupGalery(ObjectT objectT,GeneratedObject go)
    {
        this.objectT = objectT;
        this.generatedObject = go;
        selectedArray = zvierata;
    }
    public void OpenGalery()
    {
        galeriaPanel.DOFade(1, 0.35f);
        SetupBtns();
    }
    public void CloseGalery()
    {
        galeriaPanel.DOFade(0, 0.35f);
        Invoke("Deactivate", 0.35f);
    }
    void Deactivate()
    {
        galeriaPanel.gameObject.SetActive(false);
        counter = 0;
    }
    public void SetupBtns()
    {
        ResetBtns();
        for (int i=counter;i<counter+10; i++)
        {
            if (i < selectedArray.Length)
            {
                galBtns[i%10].gameObject.SetActive(true);
                galBtns[i % 10].LoadTexture(selectedArray[i]);
            }
        }
    }
    public void ResetBtns()
    {
        foreach(GaleriaBtn btn in galBtns)
        {
            btn.gameObject.SetActive(false);
        }
    }
    public void LoadNext()
    {
        if (counter+10 >= selectedArray.Length)
        {
            return;
        }
        else
        {
            counter += 10;
            SetupBtns();
        }
    }
    public void LoadPrev()
    {
        if (counter-10 <0)
        {
            return;
        }
        else
        {
            counter -= 10;
            SetupBtns();
        }
    }
    public void ChangeSelectedPanel(string panelName)
    {
        switch (panelName)
        {
            case "zvierata": selectedArray = zvierata; break;
            case "enviroment": selectedArray = enviroment; break;
            case "priroda": selectedArray = priroda; break;
            case "back": selectedArray = zvierataBack; break;
            case "hmyz": selectedArray = hmyz; break;
            case "voda": selectedArray = voda; break;
        }
        counter = 0;
        SetupBtns();
    }

}
