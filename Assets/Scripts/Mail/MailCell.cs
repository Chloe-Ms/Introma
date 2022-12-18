
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MailCell : MonoBehaviour
{
    [SerializeField] private TMP_Text mailSubject;
    [SerializeField] private TMP_Text mailSender;
    [SerializeField] private TMP_Text mailDate;
    private MailData mailData;
    public bool canAnswer;

    private void Start()
    {
    }
    public GameObject Init(MailData _mailData)
    {
        Debug.Log(_mailData.Subject);
        mailSubject.text = _mailData.Subject;
        mailSender.text = "From : " + _mailData.Sender;
        mailDate.text = _mailData.Date;
        mailData = _mailData;
        canAnswer = _mailData.CanAnswer;
        return this.gameObject;
    }
    public void Display()
    {
        MailBoxManager.Instance.SendMessage("DisplayMail",this.mailData);
    }
    void OnApplicationQuit()
    {
        mailData.CanAnswer = canAnswer;  //Reset CanAnswer bool in all mail data scriptable objects
    }
}
