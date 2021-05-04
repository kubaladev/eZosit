using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSPawner : MonoBehaviour
{
    public GameObject basic;
    public GameObject spawnArea;
    public GameObject tvar;
    public GameObject text;
    public Texture2D rectangleText;
    public GameObject selected;
    public GenObjectType type;
    public Sprite[] icons;
    public PrefabType prefabType;
    public ActivePanel activePanel;
    public int objectContextID = 0;
    public int staticContextID = 0;
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
        OT.prefabTyp = prefabType;
        switch (type)
        {
            case GenObjectType.Drag:
                DragDrop dg = go.AddComponent<DragDrop>();
                dg.DisableMovement();
                OT.SetTyp(GenObjectType.Drag, icons[0]);
                if (OT.prefabTyp.Equals(PrefabType.Tvar))
                {
                    dg.objectName = "Tvar "+objectContextID;
                    dg.contextID = objectContextID;
                    objectContextID++;
                    dg.type = "Drag";
                }
                if (OT.prefabTyp.Equals(PrefabType.Basic))
                {
                    dg.contextID = objectContextID;
                    dg.objectName = "Obr. " + objectContextID;
                    objectContextID++;
                }
                break;
            case GenObjectType.Click:
                ClickableObject co = go.AddComponent<ClickableObject>();
                OT.SetTyp(GenObjectType.Click, icons[1]);
                break;
            case GenObjectType.Static:
                ItemSlot itemslot = go.AddComponent<ItemSlot>();
                OT.SetTyp(GenObjectType.Static, icons[2]);
                if (prefabType.Equals(PrefabType.Tvar))
                {
                    itemslot.img.color = Color.gray;
                    itemslot.img.texture = rectangleText;
                    itemslot.objectName = "Static tvar " + staticContextID;
                    staticContextID++;
                }
                if (prefabType.Equals(PrefabType.Basic))
                {

                }
                break;
        }
        ObjectModificator.Instance.SelectObject(OT.GetComponent<GeneratedObject>(), OT);
        
    }
    public void GenerateFriend()
    {
        ObjectT mainOT = ObjectModificator.Instance.OT;
        DragDrop mainGO = ObjectModificator.Instance.go.GetComponent<DragDrop>();
        if (mainGO.pair != null)
        {
            mainGO.pair.GetComponent<ObjectT>().FadeOut();
        }
        GameObject go=Instantiate(mainGO.gameObject, spawnArea.transform);
        go.transform.SetAsFirstSibling();
        Destroy(go.GetComponent<DragDrop>());
        ItemSlot itemslot = go.AddComponent<ItemSlot>();
        go.GetComponent<ObjectT>().SetTyp(GenObjectType.Static, icons[2]);
        itemslot.contextID= mainGO.contextID;
        itemslot.objectName = "Miesto: " + mainGO.objectName;
        itemslot.type = "Stat";
        itemslot.img.color = Color.gray;
        mainGO.pair = itemslot.gameObject;
        itemslot.imageID = mainGO.imageID;
        itemslot.pair = mainGO.gameObject;
        ObjectModificator.Instance.activePanel.UpdateFriendTxt();
        mainOT.UpdatePair();
    }

}
