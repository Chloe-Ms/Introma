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
        Debug.Log((int)day);
    }
    public void NextDay()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        day = (Day)(((int)day + 1) % 3);
        FadeImage.SetTrigger("FadeOut");
        StartCoroutine(DayStart());
    }

    IEnumerator DayStart()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene((int)day);
        FadeImage.SetTrigger("FadeIn");
        //mailBoxManager = GameObject.FindObjectOfType<MailBoxManager>();
        //mailBoxManager.GetDayMail();
    }

}
