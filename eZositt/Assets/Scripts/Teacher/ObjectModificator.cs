using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectModificator : Singleton<ObjectModificator>
{

    public TvarPanel tvarPanel;
    public GeneralPanel generalPanel;
    public GeneratedObject go;
    public StaticPanel staticPanel;
    public ActivePanel activePanel;
    public ObjectT OT;
    bool editMode = false;
    public void SelectObject(GeneratedObject go, ObjectT OT)
    {
        if (this.OT != null)
        {
            if (!OT.Equals(this.OT))
            {
                this.OT.OnUnselectObject();

            }
            else
            {
                return;
            }
        }
        this.go = go;
        this.OT = OT;
        OT.OnSelectObject();
        generalPanel.gameObject.SetActive(true);
        generalPanel.SetupPanel(go.rectTransform, OT.img, OT);
        

        if (OT.prefabTyp.Equals(PrefabType.Tvar))
        {
            tvarPanel.gameObject.SetActive(true);
            tvarPanel.SetupPanel(OT.img, go.rectTransform, OT, go);
            if (OT.typ.Equals(GenObjectType.Static))
            {
                staticPanel.gameObject.SetActive(true);
                staticPanel.SetupPanel((ItemSlot)go);
            }
            else
            {
                staticPanel.gameObject.SetActive(false);
            }
            if (OT.typ.Equals(GenObjectType.Drag))
            {
                activePanel.gameObject.SetActive(true);
                activePanel.SetupPanel((GeneratedObject)go);
            }
            else
            {
                activePanel.gameObject.SetActive(false);
            }
        }
        else
        {
            activePanel.gameObject.SetActive(true);
            staticPanel.gameObject.SetActive(false);
            tvarPanel.gameObject.SetActive(false);
        }

       
            
    }          
    
    public void UnselectObject()
    {
        OT.OnUnselectObject();
    }
}
