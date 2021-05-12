using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetupHomework : Singleton<SetupHomework>
{

    public Button[] buttons;
    public void Setup(DataFile df)
    {
        if (!df.vel)
        {
            buttons[0].gameObject.SetActive(false);
            buttons[1].gameObject.SetActive(false);
        }
        if (!df.rot)
        {
            buttons[2].gameObject.SetActive(false);
            buttons[3].gameObject.SetActive(false);
        }
        if (!df.klon)
        {
            buttons[4].gameObject.SetActive(false);
            buttons[5].gameObject.SetActive(false);
        }
    }
}
