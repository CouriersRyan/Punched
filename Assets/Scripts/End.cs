using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MazeObject
{
    public override int Interact()
    {
        return GameManager.Instance.ValidateSentence();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
