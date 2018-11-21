using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TenMajorControl : MonoBehaviour {

    static public int root_rate;
    static public bool isSetTenMajor = false;
    Button next;

    private void Start()
    {
        root_rate = -1;
        next = GameObject.Find("Next").GetComponent<Button>();
        next.interactable = false;
        isSetTenMajor = false;
    }

    private void Update()
    {
        if(isSetTenMajor)
        {
            next.interactable = true;
        }
        else
        {
            next.interactable = false;
        }
    }

    public void OnClickMajor()
    {
        isSetTenMajor = true;
        if (transform.name == "zero")
        {
            root_rate = 0;
        }
        else if(transform.name == "one")
        {
            root_rate = 1;
        }
        else if (transform.name == "two")
        {
            root_rate = 2;
        }
        else if (transform.name == "three")
        {
            root_rate = 3;
        }
        else if (transform.name == "four")
        {
            root_rate = 4;
        }
        else if (transform.name == "five")
        {
            root_rate = 5;
        }
        else if (transform.name == "six")
        {
            root_rate = 6;
        }
        else if (transform.name == "seven")
        {
            root_rate = 7;
        }
        else if (transform.name == "eight")
        {
            root_rate = 8;
        }
        else if (transform.name == "nine")
        {
            root_rate = 9;
        }
        else if (transform.name == "ten")
        {
            root_rate = 10;
        }
    }

}
