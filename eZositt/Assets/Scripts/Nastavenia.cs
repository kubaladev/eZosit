using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Nastavenia : MonoBehaviour
{
    public Toggle[] toggles;
    public CanvasGroup cg;
    public void Awake()
    {
        cg.alpha = 0;
        cg.DOFade(1, 0.35f);
    }
    public void SetBoolValue(int index)
    {
        switch (index)
        {
            case 0: ImageSerializer.Instance.rot = toggles[0].isOn; break;
            case 1: ImageSerializer.Instance.vel = toggles[1].isOn; break;
            case 2: ImageSerializer.Instance.klon = toggles[2].isOn; break;
            case 3: ImageSerializer.Instance.shuffle = toggles[3].isOn; break;
            case 4: ImageSerializer.Instance.random = toggles[4].isOn; break;
            case 5: ImageSerializer.Instance.control = toggles[5].isOn; break;
        }
    }
    public void Offline()
    {
        cg.DOFade(0, 0.35f);
        Invoke("OFF",0.35f);
    }
    public void OFF()
    {
        cg.gameObject.SetActive(false);
    }
}
