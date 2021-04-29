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
        generalPanel.SetupPanel(go.rectTransform);
        if (OT.prefabTyp.Equals(PrefabType.Tvar))
        {
            tvarPanel.gameObject.SetActive(true);
            tvarPanel.SetupPanel(go.img, go.rectTransform,OT);
        }

    }
}
