using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultControl : MonoBehaviour {

    Text Q1;
    Text Q2;
    Text Q3;
    Text Q4;
    Text Q5;
    ConnectServer connectServer;
    GameObject result;

    // Use this for initialization
    void Start () {
        Q1 = GameObject.Find("Q1").GetComponent<Text>();
        Q2 = GameObject.Find("Q2").GetComponent<Text>();
        Q3 = GameObject.Find("Q3").GetComponent<Text>();
        Q4 = GameObject.Find("Q4").GetComponent<Text>();
        Q5 = GameObject.Find("Q5").GetComponent<Text>();
        result = GameObject.Find("result");
        connectServer = GameObject.Find("SceneController").GetComponent<ConnectServer>();
    }
	
	// Update is called once per frame
	void Update () {
        Q1.text = "1. 最後（step:C）に動かした箱の重さ: " + TenMajorControl.root_rate;
        if(SexSelectControl.root_sex == 0)
        {
            Q2.text = "2. あなたの性別: 男";
        }
        else if(SexSelectControl.root_sex == 1)
        {
            Q2.text = "2. あなたの性別: 女";
        }
        if(DexteritySelectControl.root_decterity == 0)
        {
            Q3.text = "3. あなたの利き手: 左";
        }
        else if(DexteritySelectControl.root_decterity == 1)
        {
            Q3.text = "3. あなたの利き手: 右";
        }
        Q4.text = "4. あなたの年齢: " + AgeSelectControl.root_age;
        string group = "";
        if (Scene.group_num == 0)
        {
            group = "A";
        }
        else if(Scene.group_num == 1)
        {
            group = "B";
        }
        Q5.text = "5. あなたの実験グループ: " + group;

    }

    public void OnClickFinish()
    {
        Debug.Log("server_connection_start");
        connectServer.set_rate(TenMajorControl.root_rate, Scene.group_num, SexSelectControl.root_sex, AgeSelectControl.root_age, DexteritySelectControl.root_decterity);
        result.SetActive(false);
    }
}
