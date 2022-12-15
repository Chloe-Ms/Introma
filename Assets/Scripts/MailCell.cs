
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MailCell : MonoBehaviour
{
    [SerializeField] private TMP_Text mailSubject;
    [SerializeField] private TMP_Text mailSender;
    [SerializeField] private TMP_Text mailDate;

    public void Display(MailData mailData)
    {
        mailSubject.text = mailData.Subject;
        mailSender.text = "From : " + mailData.Sender;
        mailDate.text = mailData.Date;
    }
}
