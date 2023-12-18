using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class QuestionAndAnswer
{
    public bool isDone = false; 
    public string question;
    public string[] answers;
    public int correctAnswer;  
    public string explanation; 
}