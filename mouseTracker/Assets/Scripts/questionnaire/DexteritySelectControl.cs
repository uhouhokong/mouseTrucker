using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DexteritySelectControl : MonoBehaviour {
    static public int root_decterity;
    static public bool isSetDexterity = false;
    Button next;

    // Use this for initialization
    void Start()
    {
        next = GameObject.Find("Next").GetComponent<Button>();
        next.interactable = false;
    }

    private void Update()
    {
        if(isSetDexterity)
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
            if (transform.name == "left")
            {
                root_decterity = 0;
                isSetDexterity = true;
            }
            else if (transform.name == "right")
            {
                root_decterity = 1;
                isSetDexterity = true;
            }
        }
    }
}
