using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTroller : MonoBehaviour
{
    public GameObject hider;
    public void Awake()
    {
        hider.GetComponent<CanvasGroup>().DOFade(0, 0.75f);
        Invoke("Disablee", 0.75f);
    }
    public void RealodScene()
    {
        hider.gameObject.SetActive(true);
        hider.GetComponent<CanvasGroup>().DOFade(1, 0.65f);
        Invoke("Restart", 0.75f);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Skonci()
    {
        hider.gameObject.SetActive(true);
        hider.GetComponent<CanvasGroup>().DOFade(1, 0.65f);
        Invoke("End", 0.7f);
    }
    public void Disablee()
    {
        hider.SetActive(false);
    }
    public void End()
    {
        Application.Quit();
    }
}
