using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectServer : MonoBehaviour
{

    public string ServerAddress = "http://localhost:8080/set_rate.php";
    int rate; //重さを表す 0~10段階評価
    int dexterity;
    int group; //A, Bどっちか0, 1
    int sex; //性別、0: male, 1: female
    int age; //年齢
    GameObject Message;
    GameObject Next;
    GameObject Back;
    GameObject question;

    private void Start()
    {
        Message = GameObject.Find("Message");
        Next = GameObject.Find("Next");
        Back = GameObject.Find("Back");
        question = GameObject.Find("Question");
        Message.SetActive(false);
    }

    // Use this for initialization
    public void set_rate(int _rate, int _group, int _sex, int _age, int _dexterity)
    {
        rate = _rate;
        dexterity = _dexterity;
        group = _group;
        sex = _sex;
        age = _age;
        StartCoroutine("Access");
    }

    private IEnumerator Access()
    {
        Next.SetActive(false);
        Back.SetActive(false);
        question.SetActive(false);
        Dictionary<string, int> dic = new Dictionary<string, int>();
        dic.Add("rate", rate);
        dic.Add("group", group);
        dic.Add("sex", sex);
        dic.Add("age", age);
        dic.Add("dexterity", dexterity);
        yield return StartCoroutine(Post(ServerAddress, dic));
    }

    private IEnumerator Post(string url, Dictionary<string, int> post)
    {
        WWWForm form = new WWWForm();
        foreach (KeyValuePair<string, int> post_arg in post)
        {
            form.AddField(post_arg.Key, post_arg.Value);
        }
        WWW www = new WWW(url, form);

        yield return StartCoroutine(CheckTimeOut(www, 3f));

        if (www.error != null)
        {
            Debug.Log("HttpPost NG:" + www.error);
        }
        else if (www.isDone)
        {
            Debug.Log("rate set complete");
            Message.SetActive(true);
        }
    }

    private IEnumerator CheckTimeOut(WWW www, float timeout)
    {
        float requestTime = Time.time;

        while (!www.isDone)
        {
            if (Time.time - requestTime < timeout)
                yield return null;
            else
            {
                Debug.Log("TimeOut");  //タイムアウト
                //タイムアウト処理
                //
                //
                break;
            }
        }
        yield return null;
    }
}

