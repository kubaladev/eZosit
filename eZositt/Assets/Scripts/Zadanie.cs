using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Zadanie : MonoBehaviour
{
    public TMP_InputField input;
    public CanvasGroup cg;
    public void SetZadanie()
    {
        ImageSerializer.Instance.zadanie=input.text;
    }
    public void Fade(bool fadeIn)
    {
        if (fadeIn)
        {
            cg.DOFade(1, 0.35f);
        }
        else
        {
            cg.DOFade(0, 0.275f);
            Invoke("Deactivate", 0.275f);
        }
    }
    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
}
