using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Text;

public class GeneratedObject : MonoBehaviour
{
    [HideInInspector] public RectTransform rectTransform;
    protected CanvasGroup canvasGroup;
    protected Vector3 startingPos;
    public RawImage img;
    [SerializeField]
    public Outline outline;
    [SerializeField] protected Canvas canvas;
    public string type;
    public string objectName = "";
    public GameObject pair;
    public int imageID = 0;
    public bool original = true;


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

    public void FadeOut()
    {
        if (!original)
        {
            canvasGroup.DOFade(0, 0.4f);
            rectTransform.DOScale(Vector3.zero, 0.4f);
            Destroy(this.gameObject, 0.41f);
        }

    }
    public void LoadTexture(byte[] data)
    {
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(data);
        img.texture = tex;
        img.SetNativeSize();
        while (rectTransform.rect.width > 256|| rectTransform.rect.height > 256)
        {
            rectTransform.sizeDelta=new Vector2(rectTransform.rect.width / 2, rectTransform.rect.height / 2);
        }
        ObjectModificator.Instance.UnselectObject();
        ObjectModificator.Instance.SelectObject(this, this.GetComponent<ObjectT>());
    }
}
