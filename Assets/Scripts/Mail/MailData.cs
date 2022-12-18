using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Mail Data", menuName = "Introma/Create Mail Infos", order = 1)]

public class MailData : ScriptableObject
{

    [SerializeField] private string sender;
    [SerializeField] private string subject;
    [TextArea][SerializeField] private string content;
    [SerializeField] private string date;

    [SerializeField] private MailData previousMail;

    [SerializeField] private MailData nextMailChoice1;
    [SerializeField] private MailData nextMailChoice2;

    [SerializeField] private Sprite girlFace;

    [SerializeField] private bool isGlitched;
    [SerializeField] private bool canAnswer;
    [SerializeField] private bool screamer;

    [SerializeField] private float timeToWaitForAnswer;

    public string Sender
    {
        get { return sender; }
        set { sender = value; }
    }
    public string Subject
    {
        get { return subject; }
        set { subject = value; }
    }
    public string Content
    {
        get { return content; }
        set { content = value; }
    }

    public string Date
    {
        get { return date; }
        set { date = value; }
    }

    public MailData PreviousMail
    {
        get { return previousMail; }
        set { previousMail = value; }
    }
    public MailData NextMailChoice1
    {
        get { return nextMailChoice1; }
        set { nextMailChoice1 = value; }
    }
    public MailData NextMailChoice2
    {
        get { return nextMailChoice2; }
        set { nextMailChoice2 = value; }
    }

    public Sprite GirlFace
    {
        get { return girlFace; }
        set { girlFace = value; }
    }

    public bool IsGlitched
    {
        get { return isGlitched; }
        set { isGlitched = value; }
    }

    public bool CanAnswer
    {
        get { return canAnswer; }
        set { canAnswer = value; }
    }
    public bool Screamer
    {
        get { return screamer; }
        set { screamer = value; }
    }

    public float TimeToWaitForAnswer
    {
        get { return timeToWaitForAnswer; }
        set { timeToWaitForAnswer = value; }
    }
}

