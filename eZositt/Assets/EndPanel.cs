using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class EndPanel : MonoBehaviour
{
    public TMP_Text points;
    public CanvasGroup cg;
    private void Awake()
    {
        this.gameObject.SetActive(false);
        cg.alpha = 0;

    }
    public void SetupPanel(int contextPoints, int maxPoints)
    {
        this.gameObject.SetActive(true);
        cg.DOFade(1, 0.65f);
        points.text = contextPoints + "/" + maxPoints;
    }

}
