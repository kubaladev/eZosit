using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectFactory : Singleton<ObjectFactory>
{
    public GameObject GenerationArea;
    public GameObject dragPref;
    public GameObject statPref;
    public GameObject clickPref;
    public GameObject textPref;
    public TMP_Text zadanie;
    public GameObject innerPanel;

    public void TestLoad()
    {
        GenerateScene(ImageSerializer.Instance.data.objects);
        zadanie.text = ImageSerializer.Instance.data.zadanie;
        innerPanel.SetActive(false);
    }
    public void GenerateScene(SerializedObject[] data)
    {
        Debug.Log("Loaded data " + data.Length);
        foreach(SerializedObject obj in data)
        {
            if (obj.type.Equals("Drag"))
            {
                GameObject go = Instantiate(dragPref, GenerationArea.transform);
                go.GetComponent<GeneratedObject>().Initialize(obj);
            }
            if (obj.type.Equals("Click"))
            {
                GameObject go = Instantiate(clickPref, GenerationArea.transform);
                go.GetComponent<GeneratedObject>().Initialize(obj);
            }
            if (obj.type.Equals("Stat"))
            {
                GameObject go = Instantiate(statPref, GenerationArea.transform);
                go.transform.SetAsFirstSibling();
                go.GetComponent<GeneratedObject>().Initialize(obj);
            }
            if (obj.type.Equals("Afk"))
            {
                if (obj.textItemSlot.Equals(""))
                {
                    GameObject go = Instantiate(statPref, GenerationArea.transform);
                    go.transform.SetAsFirstSibling();
                    go.GetComponent<GeneratedObject>().Initialize(obj);
                }
                else
                {
                    GameObject go = Instantiate(textPref, GenerationArea.transform);
                    go.transform.SetAsFirstSibling();
                    go.AddComponent<ItemSlot>();
                    go.GetComponent<GeneratedObject>().Initialize(obj);
                    go.AddComponent<TextBlocker>();
                }

            }
        }
    }
    public void GameOver()
    {
        Application.Quit();
    }
}
