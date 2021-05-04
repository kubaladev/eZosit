using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GeneratedObject : MonoBehaviour
{
    [HideInInspector] public RectTransform rectTransform;
    protected CanvasGroup canvasGroup;
    protected Vector3 startingPos;
    public RawImage img;
    [SerializeField]
    protected Outline outline;
    [SerializeField] protected Canvas canvas;
    public string type;
    public string objectName="";
    public GameObject pair;
    public int imageID=0;
    

    // Start is called before the first frame update
    virtual protected void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        img = GetComponent<RawImage>();
        outline = GetComponent<Outline>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    public virtual void Initialize(SerializedObject data)
    {
        //string text = File.ReadAllText(@"d:\test.json");
        //data = JsonUtility.FromJson<SerializeTexture>(text);
        Texture2D tex = new Texture2D(data.texX, data.texY);
        ImageConversion.LoadImage(tex, data.texbytes);
        img.texture = tex;
        rectTransform.position = data.position;
        rectTransform.localScale = data.scale;
        rectTransform.localRotation = data.rotation;
        rectTransform.sizeDelta = data.sizeDelta;
        img.uvRect = data.rawRect;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
