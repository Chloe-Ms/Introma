using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScreenSoundsManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip clickSound;
    [SerializeField] AudioClip errorSound;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClickSound()
    {
        audioSource.PlayOneShot(clickSound, 0.6f);
    }
    public void ErrorSound()
    {
        audioSource.PlayOneShot(errorSound, 1);
    }
}