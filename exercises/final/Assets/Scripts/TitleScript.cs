﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Continue()
    {
        SceneManager.LoadScene("Fight1Screen");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
