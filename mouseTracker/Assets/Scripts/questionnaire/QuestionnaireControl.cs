using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionnaireControl : MonoBehaviour {

    int num_of_question = 5;
    int question_state;
    string[] question;
    Text question_draw;
    public GameObject[] question_object;
    Button next;
    Button back;

	// Use this for initialization
	void Start () 
    {
        question_state = 0;
        question = new string[num_of_question];
        question[0] = "最後（step:C）に動かした箱の重さ";
        question[1] = "性別";
        question[2] = "利き手";
        question[3] = "年齢";
        question[4] = "あなたの入力した内容";
        question_draw = GameObject.Find("Question").GetComponent<Text>();
        for (int i = 0; i < num_of_question; i++)
        {
            question_object[i].SetActive(false);
        }
        question_object[question_state].SetActive(true);
        question_draw.text = question[question_state];
        next = GameObject.Find("Next").GetComponent<Button>();
        back = GameObject.Find("Back").GetComponent<Button>();
    }
	
	// Update is called once per frame
	void Update () {
        if(question_state == 0)
        {
            back.gameObject.SetActive(false);
        }
        else if(question_state == num_of_question-1)
        {
            next.gameObject.SetActive(false);
        }
        else
        {
            back.gameObject.SetActive(true);
            next.gameObject.SetActive(true);
        }
	}

    public void OnClickedNext()
    {
        if(num_of_question-1 > question_state) question_state++;
        for (int i = 0; i < num_of_question; i++)
        {
            question_object[i].SetActive(false);
        }
        question_object[question_state].SetActive(true);
        question_draw.text = question[question_state];
    }

    public void OnClickBack()
    {
        if(0 < question_state) question_state--;
        for (int i = 0; i < num_of_question; i++)
        {
            question_object[i].SetActive(false);
        }
        question_object[question_state].SetActive(true);
        question_draw.text = question[question_state];
    }

}
