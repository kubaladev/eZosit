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
    public ObrPanel obrPanel;
    public ObjectT OT;
    public KlikaciPanel klikaciPanel;
    public ObrPanel staticObrPanel;
    public Galeria galeria;
    public TextPanel textPanel;

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
        DeactivatePanels();
        OT.OnSelectObject();
        generalPanel.gameObject.SetActive(true);
        generalPanel.SetupPanel(go.rectTransform, OT.img, OT);
        galeria.SetupGalery(OT, go);

        if (OT.prefabTyp.Equals(PrefabType.Tvar))
        {
            tvarPanel.gameObject.SetActive(true);
            tvarPanel.SetupPanel(OT.img, go.rectTransform, OT, go);
            if (OT.typ.Equals(GenObjectType.Static))
            {
                staticPanel.gameObject.SetActive(true);
                staticPanel.SetupPanel((ItemSlot)go);
            }
            if (OT.typ.Equals(GenObjectType.Drag))
            {
                activePanel.gameObject.SetActive(true);
                activePanel.SetupPanel((GeneratedObject)go);
            }
        }
        if (OT.prefabTyp.Equals(PrefabType.Basic))
        {
            obrPanel.gameObject.SetActive(true);
            obrPanel.SetupPanel((GeneratedObject)go,OT);
            if (OT.typ.Equals(GenObjectType.Drag))
            {
                activePanel.gameObject.SetActive(true);
                activePanel.SetupPanel((GeneratedObject)go);


            }
            if (OT.typ.Equals(GenObjectType.Static))
            {
                staticPanel.gameObject.SetActive(true);
                staticPanel.SetupPanel((ItemSlot)go);
            }
            if (OT.typ.Equals(GenObjectType.Afk))
            {
                staticObrPanel.gameObject.SetActive(true);
                staticObrPanel.SetupPanel(go, OT);
            }
        }
        if (OT.prefabTyp.Equals(PrefabType.Swap))
        {
            klikaciPanel.gameObject.SetActive(true);
            klikaciPanel.SetupPanel((ClickableObject)go, OT);
        }
        if (OT.prefabTyp.Equals(PrefabType.Text))
        {
            textPanel.gameObject.SetActive(true);
            textPanel.SetupPanel(OT, (ItemSlot)go);
            tvarPanel.gameObject.SetActive(true);
            tvarPanel.SetupPanel(OT.img, go.rectTransform, OT, go);
        }

    }
    public void LoadTexture(byte[] data)
    {
        go.LoadTexture(data);
        OT.velkost = "Original: " + go.img.texture.width + " x " + go.img.texture.height;
        if (OT.typ.Equals(GenObjectType.Drag))
        {
            obrPanel.SetupPanel((GeneratedObject)go, OT);
        }
        if(OT.typ.Equals(GenObjectType.Click))
        {
            klikaciPanel.SaveTexture();
            klikaciPanel.SetupPanel((ClickableObject)go, OT);
        }

    }
    public void UnselectObject()
    {
        OT.OnUnselectObject();
        go = null;
        OT = null;
        DeactivatePanels();
    }
    public void DeactivatePanels()
    {
        generalPanel.gameObject.SetActive(false);
        activePanel.gameObject.SetActive(false);
        staticPanel.gameObject.SetActive(false);
        obrPanel.gameObject.SetActive(false);
        klikaciPanel.gameObject.SetActive(false);
        staticObrPanel.gameObject.SetActive(false);
        textPanel.gameObject.SetActive(false);
    }
}
