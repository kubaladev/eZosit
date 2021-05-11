using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ObrPanel : MonoBehaviour
{
    public TMP_Text nameText;
    public GameObject imgLoader;
    public TMP_Text velkostTxt;
    public void SetupPanel(GeneratedObject generatedObject, ObjectT objectT)
    {
        nameText.text = generatedObject.objectName;
        if (objectT.velkost.Length > 4)
        {
            velkostTxt.text = objectT.velkost;
        }
        else
        {
            velkostTxt.text = "-obrázok nenahratý-";
        }
        if (objectT.typ.Equals(GenObjectType.Static))
        {
            imgLoader.SetActive(false);
        }
        else
        {
            imgLoader.SetActive(true);
        }
    }
}
