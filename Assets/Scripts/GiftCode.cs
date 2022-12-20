using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GiftCode : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private string code;
    private TMP_InputField inputField;

    [SerializeField] private PopUps popUps;

    private Animator player;

    void Start()
    {
        inputField = this.GetComponent<TMP_InputField>();
        player = GameObject.Find("CameraPC").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inputField.text.Length >=4)
            CheckCode(inputField.text);
    }

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if(GameManager.Instance.day == GameManager.Day.Second)
        {
            StartCoroutine(popUps.StartPopUp());
        }
    }
    void CheckCode(string text)
    {
        if(text == code)
        {
            GameManager.Instance.NextDay();
            inputField.text = "";
        }
        else
        {
            player.SetTrigger("ScreenShake");
            inputField.text = "";
        }
    }
}
