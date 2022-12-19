using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class GetComputerNumber : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<TMP_Text>().text = GetNumber(System.Int32.Parse(transform.parent.parent.parent.parent.name), transform.parent.GetSiblingIndex()).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private int GetNumber(int rowNumber, int computerNumber)
    {
        return rowNumber*20 + computerNumber;
    }
}
