using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PopUps : MonoBehaviour
{
    [SerializeField] ScreenSoundsManager soundsManager;
    [SerializeField] GameObject firstPopUp;
    [SerializeField] GameObject[] popUpsRound1;
    [SerializeField] GameObject[] popUpsRound2;
    List<GameObject> allWindows = new();
    bool hasStarted;
    // Start is called before the first frame update
    void Start()
    {
        GetAllWindows();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted)
            CheckIfWindowsClosed();
    }
    void GetAllWindows()
    {
        allWindows.Add(firstPopUp);
        foreach(GameObject popUp in popUpsRound1)
            allWindows.Add(popUp);
        foreach (GameObject popUp in popUpsRound2)
            allWindows.Add(popUp);
    }
    void CheckIfWindowsClosed()
    {
        foreach (GameObject popUp in allWindows) 
        {
            if (popUp.activeSelf == true)
                return;
        }
        GameManager.Instance.NextDay();
        hasStarted = false;
    }
    public void SecondRound()
    {
        StartCoroutine(PopUpRound(popUpsRound2));
    }
    public IEnumerator StartPopUp()
    {
        soundsManager.ErrorSound();
        firstPopUp.SetActive(true);
        yield return new WaitForSeconds(1);
        hasStarted = true;
        StartCoroutine(PopUpRound(popUpsRound1));
    }
    IEnumerator PopUpRound(GameObject[] popUpsLists) {
        foreach (GameObject popUp in popUpsLists) {
            soundsManager.ErrorSound();
            popUp.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
