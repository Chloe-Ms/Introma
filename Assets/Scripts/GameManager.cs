using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameManager>();
            return instance;
        }
    }
    public enum Day {
        First = 1,
        Second = 2,
        Third = 3};

    public Day day = Day.Second; //A MODIFIER

    [SerializeField] private Animator FadeImage;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextDay()
    {
        day = (Day)(((int)day + 1) % 3);
        FadeImage.SetTrigger("FadeOut");
    }

}
