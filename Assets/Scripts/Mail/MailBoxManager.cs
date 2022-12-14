using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;
using UnityEngine.UI;

public class MailBoxManager : MonoBehaviour
{


    [Header("\nInitial Mail Boxes\n")]
    [SerializeField] private MailData[] mailListDay1;
    [SerializeField] private MailData[] mailListDay2;
    [SerializeField] private MailData[] mailListDay3;

    [Header("\nGirl Window Objects\n")]
    [SerializeField] private GameObject girlWindow;
    [SerializeField] private GameObject girlFace;

    [Header("\nDisplayed Mail Objects\n")]
    [SerializeField] private TMP_Text mailSubject;
    [SerializeField] private TMP_Text mailSender;
    [SerializeField] private TMP_Text mailDate;
    [SerializeField] private TMP_Text previousMail;
    [SerializeField] private TMP_Text mailContent;
    [SerializeField] private GameObject AnswerButton;

    [Header("\nAnswer Window Game Objects\n")]
    [SerializeField] private GameObject answer1Box;
    [SerializeField] private TMP_Text answer1;
    [SerializeField] private GameObject answer2Box;
    [SerializeField] private TMP_Text answer2;
    [SerializeField] private TMP_Text chosenAnswer;

    [Header("\nMail Box Objects\n")]
    [SerializeField] private MailCell mailPrefab;
    [SerializeField] private GameObject mailGrid;
    [SerializeField] private GameObject screamer;

    [Header("\nOther\n")]
    [SerializeField] private TriggerComputer triggerComputer;

    private MailData activeDisplayedMail;
    private MailData chosenAnswerData;
    private bool canSendAnswer;

    [SerializeField] GameObject _enemy;
    [SerializeField] GameObject _cle;


    void Start()
    {
        if(_enemy != null)
            _enemy.SetActive(false);

        if (_cle != null)
            _cle.SetActive(false);
        GetDayMail();
    }

    void AddMail(MailData mailData)
    {
        GameObject mail = Instantiate(mailPrefab, mailGrid.transform).Init(mailData);
        mail.transform.SetAsFirstSibling();
    }

    void DisplayMail(MailData mailData)
    {
        mailSubject.text = mailData.Subject;
        mailSender.text = "From : " + mailData.Sender;
        mailDate.text = mailData.Date;
        if (mailData.PreviousMail != null)
            previousMail.text = "In response to : " + mailData.PreviousMail.Sender + " : " + mailData.PreviousMail.Content;
        else
            previousMail.text = "";
        
        AnswerButton.SetActive(mailData.NextMailChoice1 != null && mailData.CanAnswer); //Disable Answer Button if no Answers or !canAnswer
        mailContent.text = mailData.Content;
        activeDisplayedMail = mailData;
        if (mailData.Screamer)
            StartCoroutine(PlayScreamer());
        if(mailData.GirlFace != null)
        {
            girlWindow.SetActive(true);
            girlFace.GetComponent<Image>().sprite = mailData.GirlFace;
        }
        else
            girlWindow.SetActive(false);
    }
    IEnumerator PlayScreamer()
    {
        this.GetComponent<ScreenSoundsManager>().ScreamerSound();
        screamer.SetActive(true);
        _enemy.SetActive(true);
        _cle.SetActive(true);
        yield return new WaitForSeconds(2f);
        triggerComputer.GetOffComputer();
    }
    public void DisplayAnswers()
    {
        canSendAnswer = false;
        answer1Box.SetActive(true);
        answer2Box.SetActive(true);
        chosenAnswer.gameObject.SetActive(false);
        answer1.text = activeDisplayedMail.NextMailChoice1.Content;
        answer2.text = activeDisplayedMail.NextMailChoice2.Content;
    }

    public void SetChosenAnswer(Button button)
    {
        canSendAnswer = true;
        if (button.gameObject.name == "Answer1")
            chosenAnswerData = activeDisplayedMail.NextMailChoice1;
        else
            chosenAnswerData = activeDisplayedMail.NextMailChoice2;
        answer1Box.SetActive(false);
        answer2Box.SetActive(false);
        chosenAnswer.gameObject.SetActive(true);
        chosenAnswer.text = chosenAnswerData.Content;
    }
    public void SendAnswer()
    {
        if (canSendAnswer)
        {
            activeDisplayedMail.CanAnswer = false;
            AnswerButton.SetActive(false);
            StartCoroutine(WaitForAnswer());
        }
    }
    private IEnumerator WaitForAnswer()
    {
        if (canSendAnswer)
        {
            yield return new WaitForSeconds(activeDisplayedMail.TimeToWaitForAnswer);
            AddMail(chosenAnswerData.NextMailChoice1);
        }
    }
    public void GetDayMail()
    {
        switch (GameManager.Instance.day)
        {
            case GameManager.Day.First:
                InitMailBox(mailListDay1);
                break;
            case GameManager.Day.Second:
                InitMailBox(mailListDay2);
                break;
            case GameManager.Day.Third:
                InitMailBox(mailListDay3);
                break;
        }
    }

    private void InitMailBox(MailData[] mailList)
    {
        //StartCoroutine(gameObject.GetComponent<TextDisplay>().ScrollSentence(customer.Data.RequestStartText, _requestText));

        // Destroy all mails in mailbox
        foreach (Transform child in mailGrid.transform)
            Destroy(child.gameObject);

        // Instantiate all mails from List
        foreach (MailData mail in mailList)
        {
            AddMail(mail);
        }
    }
}
