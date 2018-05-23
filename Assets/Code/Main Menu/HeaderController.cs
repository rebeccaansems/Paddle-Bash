using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeaderController : MonoBehaviour
{

    public Text HeaderText;

    private GameData.MenuScreens currentScreen;

    private void Start()
    {
        HeaderText.text = GameData.k_CurrentMenuScreen.ToString();
        currentScreen = GameData.k_CurrentMenuScreen;
    }

    private void Update()
    {
        if (currentScreen != GameData.k_CurrentMenuScreen)
        {
            HeaderText.text = GameData.k_CurrentMenuScreen.ToString();
            currentScreen = GameData.k_CurrentMenuScreen;
        }
    }
}
