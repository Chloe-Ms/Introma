using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScreenSoundsManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip clickSound;
    [SerializeField] AudioClip errorSound;
    [SerializeField] AudioClip screamerSound;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("MainCamera").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ScreamerSound()
    {
        audioSource.PlayOneShot(screamerSound, 1f);
    }
    public void ClickSound()
    {
        audioSource.PlayOneShot(clickSound, 0.4f);
    }
    public void ErrorSound()
    {
        audioSource.PlayOneShot(errorSound, 1);
    }
}
