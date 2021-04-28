using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSPawner : MonoBehaviour
{
    public GameObject basic;
    public GameObject spawnArea;
    public GameObject tvar;
    public GameObject text;

    public GameObject selected;
    public GenObjectType type;
    public Sprite[] icons;
    public PrefabType prefabType;
    public void SetSelected(int id)
    {
        switch (id)
        {
            case 0: selected = tvar; prefabType = PrefabType.Tvar; break;
            case 1: selected = basic; prefabType = PrefabType.Basic; break;
            case 2: selected = text; prefabType = PrefabType.Text; break;
        }
    }

    public void CreateObj()
    {
        GameObject go=Instantiate(selected, spawnArea.transform);
        ObjectT OT = go.AddComponent<ObjectT>();
        OT.prefabTyp = PrefabType.Tvar;
        switch (type)
        {
            case GenObjectType.Drag:
                DragDrop dg = go.AddComponent<DragDrop>();
                dg.DisableMovement();
                OT.SetTyp(GenObjectType.Drag, icons[0]);              
                break;
            case GenObjectType.Click:
                ClickableObject co = go.AddComponent<ClickableObject>();
                OT.SetTyp(GenObjectType.Click, icons[1]);
                break;
            case GenObjectType.Static:
                ItemSlot itemslot = go.AddComponent<ItemSlot>();
                OT.SetTyp(GenObjectType.Static, icons[2]);
                break;
        }
        ObjectModificator.Instance.SelectObject(OT.GetComponent<GeneratedObject>(), OT);
        
    }

}
