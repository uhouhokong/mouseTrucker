using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgeSelectControl : MonoBehaviour {

    Dropdown age;
    static public int root_age;
    static public bool isSetAge;
    Button next;

    // Use this for initialization
    void Start () {
        age = GameObject.Find("age").GetComponent<Dropdown>();
        next = GameObject.Find("Next").GetComponent<Button>();
        next.interactable = false;
    }

    private void Update()
    {
        if (isSetAge)
        {
            next.interactable = true;
        }
        else
        {
            next.interactable = false;
        }
    }

    public void OnValueChanged()
    {
        isSetAge = true;
        root_age = age.value;
    }
}
