using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GeneratedObject : MonoBehaviour
{
    [HideInInspector] public RectTransform rectTransform;
    protected CanvasGroup canvasGroup;
    protected Vector3 startingPos;
    public Image img;
    [SerializeField]
    protected Outline outline;
    [SerializeField] protected Canvas canvas;

    SerializeTexture data;

    // Start is called before the first frame update
    virtual protected void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        img = GetComponent<Image>();
        outline = GetComponent<Outline>();
    }
    public void Initialize()
    {
            string text = File.ReadAllText(@"d:\test.json");
            data = JsonUtility.FromJson<SerializeTexture>(text);
            Texture2D tex = new Texture2D(data.texX, data.texY);
            ImageConversion.LoadImage(tex, data.texbytes);
            Sprite mySprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), Vector2.one);
            img.sprite = mySprite;
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
