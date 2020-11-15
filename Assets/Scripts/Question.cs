using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    public string question;
    public int weight;
    public string[] options = new string[4];
    public int correctAnsIndex;
}
