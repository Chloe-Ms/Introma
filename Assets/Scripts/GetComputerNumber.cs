using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GetComputerNumber : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetNumber(System.Int32.Parse(transform.parent.parent.parent.parent.name), transform.GetSiblingIndex()+1);
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
