using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeaderController : MonoBehaviour
{

    public Text HeaderText;

    private SessionData.MenuScreens currentScreen;

    private void Start()
    {
        HeaderText.text = SessionData.Instance.CurrentMenuScreen.ToString();
        currentScreen = SessionData.Instance.CurrentMenuScreen;
    }

    private void Update()
    {
        if (currentScreen != SessionData.Instance.CurrentMenuScreen)
        {
            HeaderText.text = SessionData.Instance.CurrentMenuScreen.ToString();
            currentScreen = SessionData.Instance.CurrentMenuScreen;
        }
    }
}
