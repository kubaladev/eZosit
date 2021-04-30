using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectModificator : Singleton<ObjectModificator>
{

    public TvarPanel tvarPanel;
    public GeneralPanel generalPanel;
    public GeneratedObject go;
    public ObjectT OT;
    public void SelectObject(GeneratedObject go, ObjectT OT)
    {
        this.go = go;
        this.OT = OT;
        generalPanel.gameObject.SetActive(true);
        generalPanel.SetupPanel(go.rectTransform, OT.img);
        if (OT.prefabTyp.Equals(PrefabType.Tvar))
        {
            tvarPanel.gameObject.SetActive(true);
            tvarPanel.SetupPanel(OT.img, go.rectTransform,OT);
        }

    }
}
