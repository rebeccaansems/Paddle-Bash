using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditLevelController : MonoBehaviour
{
    private GameObject overallController;

    private void Start()
    {
        overallController = GameObject.FindGameObjectWithTag("Overall Controller");
    }
}
