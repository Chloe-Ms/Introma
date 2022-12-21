using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollText : MonoBehaviour
{
    private static ScrollText instance;
    public static ScrollText Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<ScrollText>();
            return instance;
        }
    }

    [SerializeField] private float _charDisplayTime;
    AudioSource audioSource;
    [Header("Audio"), SerializeField] private AudioClip textAudioClip;

    private void Reset()
    {
        _charDisplayTime = 0.1f;
    }

    private void Start()
    {
        audioSource = GameObject.Find("MainCamera").GetComponent<AudioSource>();
        //StartCoroutine(ScrollSentence(TextToDisplay, textPlaceHolder));
    }
    /// <summary>
    /// Display each letter in sentence with delay
    /// </summary>
    public IEnumerator ScrollSentence(string sentence, TMP_Text text, float DisplayTextTime)
    {
        float charDisplayTime = DisplayTextTime / sentence.Length;
        text.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            text.text += letter;
            //AudioManager.PlaySound(textAudioClip);
            yield return new WaitForSeconds(charDisplayTime);
        }
    }
    public IEnumerator ScrollSentence(string sentence, TMP_Text text)
    {
        text.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            text.text += letter;
            //AudioManager.PlaySound(textAudioClip);
            yield return new WaitForSeconds(_charDisplayTime);
        }
    }
}
