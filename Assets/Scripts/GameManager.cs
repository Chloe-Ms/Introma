using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    void Start()
    {
        //mailBoxManager = GameObject.FindObjectOfType<MailBoxManager>();
        //mailBoxManager.GetDayMail();
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
        //mailBoxManager = GameObject.FindObjectOfType<MailBoxManager>();
        //mailBoxManager.GetDayMail();
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
