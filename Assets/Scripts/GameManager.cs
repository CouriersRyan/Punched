using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }

            _instance = FindObjectOfType<GameManager>();
            return _instance;
        }
    }

    private void Awake()
    {
        sentence = String.Empty;
        inputBox.text = sentence;
    }

    private void Start()
    {
        textBox.text = initialStatement;
    }

    private static GameManager _instance;
    
    [SerializeField] private string sentence;
    [SerializeField] private string correctSentence;
    [SerializeField] private string incorrectSentence;

    [SerializeField] private TMP_Text textBox;
    [SerializeField] private TMP_Text inputBox;

    [Header("Responses")]
    [SerializeField] private string initialStatement;
    [SerializeField] private string incorrectStatement;
    [SerializeField] private string invalidStatement;
    
    public int ValidateSentence()
    {
        if (sentence.Equals(correctSentence))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            return 1;
        }

        if (sentence.Equals(incorrectSentence))
        {
            textBox.text = incorrectStatement;
            return -1;
        }

        textBox.text = invalidStatement;
        return 0;
    }

    public void AddToSentence(string s)
    {
        if (sentence.Equals(String.Empty))
        {
            sentence = s;
        }
        else
        {
            sentence += " " + s;
        }

        inputBox.text = "\"" + sentence + "\"";
    }

    public void ResetSentence()
    {
        sentence = String.Empty;
    }
}
