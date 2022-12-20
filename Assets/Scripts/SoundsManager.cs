using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [SerializeField] AudioClip[] _audios;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] float _delay = 5f;

    [SerializeField] float _percentageSound = 0.2f;

    private void Start()
    {
        StartCoroutine(PlaySound());
    }

    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(20f);
        while (true)
        {
            yield return new WaitForSeconds(_delay);
            bool playSound = Random.Range(0f, 1f) < _percentageSound;
            if (playSound)
            {
                Debug.Log("PLAY");
                int index = Random.Range(0, _audios.Length);
                _audioSource.PlayOneShot(_audios[index],0.28f);
            }else
            {
                Debug.Log("NO SOUND");
            }
        }

    }
}
