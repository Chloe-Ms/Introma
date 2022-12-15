
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MailCell : MonoBehaviour
{
    [SerializeField] private TMP_Text mailSubject;
    [SerializeField] private TMP_Text mailSender;
    [SerializeField] private TMP_Text mailDate;
    private MailData mailData;

    private MailBoxManager mailBoxManager;

    private void Start()
    {
        //mailBoxManager = GameObject.Find("Screen").Get;
    }
    public GameObject Init(MailData _mailData)
    {
        mailSubject.text = _mailData.Subject;
        mailSender.text = "From : " + _mailData.Sender;
        mailDate.text = _mailData.Date;
        mailData = _mailData;
        return this.gameObject;
    }
    public void Display()
    {
        MailBoxManager.Instance.SendMessage("DisplayMail",mailData);
    }
}
