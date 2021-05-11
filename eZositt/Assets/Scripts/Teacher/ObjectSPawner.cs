using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSPawner : Singleton<ObjectSPawner>
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
    public Texture2D basicTexture;
    public List<Texture2D> basicTextures;
    public void SetSelected(int id)
    {
        switch (id)
        {
            case 0: selected = tvar; prefabType = PrefabType.Tvar; break;
            case 1: selected = basic; prefabType = PrefabType.Basic; break;
            case 2: selected = text; prefabType = PrefabType.Text; break;
        }
    }
    public void AssignId(GeneratedObject generatedObject,ObjectT objectT)
    {
        if (objectT.typ.Equals(GenObjectType.Drag))
        {
            if (objectT.prefabTyp.Equals(PrefabType.Tvar))
            {
                generatedObject.objectName = "Tvar " + objectContextID;
                generatedObject.GetComponent<DragDrop>().contextID = objectContextID;
                objectContextID++;
            }
            if (objectT.prefabTyp.Equals(PrefabType.Basic))
            {
                generatedObject.objectName = "Obraz " + objectContextID;
                generatedObject.GetComponent<DragDrop>().contextID = objectContextID;
                objectContextID++;
            }

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
                    AssignId(dg, OT);
                    dg.type = "Drag";
                }
                if (OT.prefabTyp.Equals(PrefabType.Basic))
                {
                    AssignId(dg, OT);
                    dg.type = "Drag";
                    dg.imageID = 900;
                }
                break;
            case GenObjectType.Click:
                ClickableObject co = go.AddComponent<ClickableObject>();
                OT.SetTyp(GenObjectType.Click, icons[1]);
                co.imgFace = new List<Texture2D>(basicTextures);
                co.img.texture = basicTextures[0];
                OT.prefabTyp = PrefabType.Swap;
                co.editor = true;
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
            case GenObjectType.Afk:
                OT.SetTyp(GenObjectType.Afk, icons[2]);
                ItemSlot itemSlot = go.AddComponent<ItemSlot>();
                itemSlot.contextID = -999;
                itemSlot.imageID = -999;
                itemSlot.objectName = "Statický obj";
                itemSlot.type = "Stat";
                itemSlot.editor = true;
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
            Debug.Log(Vector3.Distance(mainGO.pair.transform.position, mainGO.transform.position));
            if(Vector3.Distance(mainGO.pair.transform.position,mainGO.transform.position)<0.5f)
                mainGO.pair.GetComponent<ObjectT>().FadeOut();
        }
        GameObject go=Instantiate(mainGO.gameObject, spawnArea.transform);
        go.transform.SetAsFirstSibling();
        Destroy(go.GetComponent<DragDrop>());
        ItemSlot itemslot = go.AddComponent<ItemSlot>();
        go.GetComponent<ObjectT>().SetTyp(GenObjectType.Static, icons[2]);
        itemslot.outline.enabled = false;
        itemslot.contextID= mainGO.contextID;
        if (mainOT.prefabTyp.Equals(PrefabType.Tvar))
        {

        }
        if (mainOT.prefabTyp.Equals(PrefabType.Basic))
        {
            itemslot.img.texture = basicTexture;
        }
        itemslot.objectName = "Miesto: " + mainGO.objectName;
        itemslot.type = "Stat";
        itemslot.img.color = Color.gray;
        mainGO.pair = itemslot.gameObject;
        itemslot.imageID = mainGO.imageID;
        itemslot.pair = mainGO.gameObject;
        ObjectModificator.Instance.activePanel.UpdateFriendTxt();
        mainOT.UpdatePair();
    }
    public void FindAndKillAllFriends(int contextId)
    {
        ItemSlot[] friends = spawnArea.GetComponentsInChildren<ItemSlot>();
        if (friends != null)
        {
            foreach(ItemSlot itemSlot in friends)
            {
                if (itemSlot.contextID == contextId)
                {
                    itemSlot.FadeOut();
                }
            }
        }
    }

}
