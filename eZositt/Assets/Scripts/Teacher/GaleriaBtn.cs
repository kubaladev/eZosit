using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaleriaBtn : MonoBehaviour
{
    public Galeria galeria;
    public RawImage textura;
    public void SetTexture()
    {
        galeria.objectT.img.texture = textura.texture;
        if (galeria.objectT.typ.Equals(GenObjectType.Click))
        {
            ClickableObject co=(ClickableObject)galeria.generatedObject;
            co.imgFace[co.currentId] = (Texture2D)textura.texture;
        }
        galeria.CloseGalery();
    }
    public void LoadTexture(Texture2D tex)
    {
        textura.texture = tex;
    }
}
