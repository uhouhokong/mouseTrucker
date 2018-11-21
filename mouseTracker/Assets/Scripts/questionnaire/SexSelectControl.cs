using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SexSelectControl : MonoBehaviour
{
    static public int root_sex;
    static public bool isSetSex = false;
    Button next;

    // Use this for initialization
    void Start()
    {
        next = GameObject.Find("Next").GetComponent<Button>();
        next.interactable = false;
    }

    private void Update()
    {
        if (isSetSex)
        {
            next.interactable = true;
        }
        else
        {
            next.interactable = false;
        }
    }

    public void OnClickSexSelect()
    {
        for (int i = 0; i <= 10; i++)
        {
            if (transform.name == "male")
            {
                root_sex = 0;
                isSetSex = true;
            }
            else if(transform.name == "female")
            {
                root_sex = 1;
                isSetSex = true;
            }
        }
    }
}
