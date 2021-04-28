using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ShowUiText : MonoBehaviour
{
    public TMP_Text help;
    // Start is called before the first frame update
    private void Awake()
    {
        help.DOFade(0, 0);
    }
    public void Show()
    {
        help.DOFade(1, 0.25f);
    }
    public void Hide()
    {
        help.DOFade(0, 0.25f);
    }
}
