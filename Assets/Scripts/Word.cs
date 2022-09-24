using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Word : MazeObject
{
    public string word;

    protected override void Start()
    {
        base.Start();
        GetComponentInChildren<TMP_Text>().text = word;
    }
    
    public override int Interact()
    {
        GameManager.Instance.AddToSentence(word);
        return -2;
    }
}
