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
        HeaderText.text = SessionData.k_CurrentMenuScreen.ToString();
        currentScreen = SessionData.k_CurrentMenuScreen;
    }

    private void Update()
    {
        if (currentScreen != SessionData.k_CurrentMenuScreen)
        {
            HeaderText.text = SessionData.k_CurrentMenuScreen.ToString();
            currentScreen = SessionData.k_CurrentMenuScreen;
        }
    }
}
