using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance => instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public enum Day {
        First = 0,
        Second = 1,
        Third = 2};

    public Day day = Day.First;
    private MailBoxManager mailBoxManager;

    [SerializeField] private Animator FadeImage;
    [SerializeField] private TMP_Text WinText;
    [SerializeField] private GameObject WinButton;

    void Start()
    {
        //mailBoxManager = GameObject.FindObjectOfType<MailBoxManager>();
        //mailBoxManager.GetDayMail();
        Win();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void NextDay()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        day = (Day)(GetNewDay((int)day,3));
        FadeImage.SetTrigger("FadeOut");
        StartCoroutine(DayStart());
    }

    IEnumerator DayStart()
    {
        yield return new WaitForSeconds(1.5f);
        Debug.Log("New day : "+(int)day);
        SceneManager.LoadScene((int)day);
        FadeImage.SetTrigger("FadeIn");
    }
    public void Win()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        FadeImage.gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        FadeImage.SetTrigger("FadeOut");
        StartCoroutine(WinCinematic());
    }
    IEnumerator WinCinematic()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("EndScene");
        //FadeImage.gameObject.GetComponent<UnityEngine.UI.Image>().enabled = false;
        //FadeImage.gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.black;
        WinText.gameObject.SetActive(true);
        yield return StartCoroutine(ScrollText.Instance.ScrollSentence("You're free.", WinText));
        yield return new WaitForSeconds(1f);
        WinButton.SetActive(true);
    }
    public void RemoveWinScreen()
    {
        WinButton.SetActive(false);
        FadeImage.gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.clear;
        WinText.text = "";
        WinText.gameObject.SetActive(false);
    }
    private int GetNewDay(int currentDay, int numberOfDays)
    {
        currentDay++;
        if (currentDay > numberOfDays - 1)
        {
            return 0;
        }
        else
            return currentDay;
    }

}
