using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

public class MailBoxManager : MonoBehaviour
{
    [Header("\nInitial Mail Boxes\n")]
    [SerializeField] private MailData[] mailListDay1;
    [SerializeField] private MailData[] mailListDay2;
    [SerializeField] private MailData[] mailListDay3;

    [Header("\nGirl Window Objects\n")]
    [SerializeField] private GameObject girlWindow;
    [SerializeField] private Sprite girlFace;

    [Header("\nDisplayed Mail Objects\n")]
    [SerializeField] private TMP_Text mailSubject;
    [SerializeField] private TMP_Text mailSender;
    [SerializeField] private TMP_Text mailDate;
    [SerializeField] private TMP_Text previousMail;
    [SerializeField] private TMP_Text mailContent;

    [Header("\nMail Box Objects\n")]
    [SerializeField] private MailCell mailPrefab;
    [SerializeField] private GameObject mailGrid;

    private MailData activeDisplayedMail;

    // Start is called before the first frame update
    void Start()
    {
        InitMailBox(mailListDay1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void AddMail(MailData mailData)
    {
        Instantiate(mailPrefab, mailGrid.transform).Display(mailData);
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