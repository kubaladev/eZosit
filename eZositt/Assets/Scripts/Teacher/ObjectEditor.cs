using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[SerializeField]
public enum GenObjectType
{
    Drag,
    Click,
    Static,
}
public enum PrefabType
{
    Tvar,
    Text,
    Basic,
}
public class ObjectEditor : Singleton<ObjectEditor>
{

    public CanvasGroup SelectionPanel;
    public Image iconTyp;
    public Sprite[] icons;
    public ObjectSPawner objectSpawner;
    GenObjectType typ;
    // Start is called before the first frame update
    private void Awake()
    {
        objectSpawner.icons = icons;
    }
    public void SetType(string type)
    {
        switch (type)
        {
            case "Drag": this.typ = GenObjectType.Drag; iconTyp.sprite=icons[0]; break;
            case "Click": this.typ = GenObjectType.Click; iconTyp.sprite = icons[1]; break;
            case "Static": this.typ = GenObjectType.Static; iconTyp.sprite = icons[2]; break;
        }
        objectSpawner.type = this.typ;
        ShowSelection();
    }
    public void HideSelection()
    {
        SelectionPanel.DOFade(0, 0.3f);
        Invoke("Deactivate", 0.3f);
    }
    public void SetPrefab(int id)
    {
        objectSpawner.SetSelected(id);
    }
    public void ShowSelection()
    {
        SelectionPanel.gameObject.SetActive(true);
        SelectionPanel.DOFade(1, 0.3f);
    }
    private void Deactivate()
    {
        SelectionPanel.gameObject.SetActive(false);
    }
}
