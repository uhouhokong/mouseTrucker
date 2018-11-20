using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour, OverButtonEvent {

    public BGCar carPrefab;
    public List<BGCar> bgcars = new List<BGCar>();//生成したBGCarを全て所有
    public List<float> responeTimer = new List<float>();

    public float respDefTime = 0.5f;
    public float respRange = 2.6f;
    public List<Transform> spones;

    public Transform camera;
    public Flont flontPrefab;
    
    

    private Flont curFlont;
    private int state=-1;
    void changeState(int next){
        Cursor.visible = true;
        stateTerminate();
        state = next;
        stateWakeup();
        Time.timeScale=1f;
    }
    public Group group = Group.A;
    public enum Group {A, B};
    void stateTerminate(){
        switch(state){
            case 0:
            Destroy(case0Canvas);
            break;
            case 1:
            Destroy(case1Canvas);
            break;
            case 2://軽い箱
            
            break;
            case 3://切り替え
            var c = blind.color;
            c.a = 0;
            blind.color = c;
            break;
            case 4://重い箱
            break;

            case 5://切り替え
            var c5 = blind.color;
            c5.a = 0;
            blind.color = c5;
            break;
            case 6://実験群
            Destroy(curFlont.gameObject);
            break;
        }
    }
    void stateWakeup(){
        switch(state){
            case 0:
            
            break;
            case 1:
            // Destroy(case0Canvas);
            case1Canvas = Instantiate(case1CanvasPrefab);
            var b =  Instantiate(buttonPrefab, new Vector3(0f, -1.25f,0), Quaternion.identity);
            b.init(this, "OK?", 3.0f);
            b.r = 0.27f;
            b.text.fontSize = (int)(b.text.fontSize * 0.6f);
            
            break;
            case 2://軽い箱
            curFlont = Instantiate(flontPrefab, new Vector2(camera.position.x + 1.2f, camera.position.y), Quaternion.identity);
            curFlont.cargo.weightType = 0;
            curFlont.cargo.slowable = false;
            break;
            case 3://切り替え
            var c = blind.color;
            c.a = 0;
            blind.color = c;
            
            time = 0;
            blindFlag = false;
            break;
            case 4://重い箱
            break;

            case 5://切り替え
            var c5 = blind.color;
            c5.a = 0;
            blind.color = c5;
            
            time = 0;
            blindFlag = false;
            break;
            case 6://実験群
            break;
        }
    }

    // case 0関係

    public GameObject case0Canvas;
    public void getButton(int i){
        switch(i){
            case 0:group = Group.A;break;
            case 1:group = Group.B;break;
        }
        changeState(1);
    }

    //case 1関係
    public GameObject case1CanvasPrefab;
    private GameObject case1Canvas;
    public OverButton buttonPrefab;
    public void buttonPressed(){
        changeState(2);
    }

    //case 3関係
    public SpriteRenderer blind;
    float time = 0;
    float timeLimit = 1.8f;
    public AnimationCurve blindCurve;

    //case 2,4,6関係
    public void finishedTasks(){
        if(state!=6)changeState(state+1);
        else{

        }
    }










	// Use this for initialization
	void Start () {
        changeState(0);
        audio.clip = musics[0];
        // audio.Play();

        for(int i=0; i<3; i++){
            responeTimer.Add(respDefTime + Random.Range(0f, 1f)*respRange);
        }
	}

    public bool blindFlag = false;
    void Update(){
        if(Input.GetKeyDown(KeyCode.L))Application.LoadLevel("Scenes/main");

        if(Input.GetKey(KeyCode.T) && Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.S))changeState(5);
        switch(state){
            case 3:case 5:
            
            var c = blind.color;
            c.a = blindCurve.Evaluate(time/timeLimit);
            blind.color = c;
            
            if(time>timeLimit * 0.7f && !blindFlag){
                if(curFlont!=null)
                Destroy(curFlont.gameObject);
                blindFlag = true;
                curFlont = Instantiate(flontPrefab, new Vector2(camera.position.x + 1.2f, camera.position.y), Quaternion.identity);
                curFlont.cargo.weightType = (state-1)/2;
                if(group == Group.A){curFlont.cargo.slowable = false;}
                if(group == Group.B && (state-1)/2 == 2){curFlont.cargo.slowable = true;}
                curFlont.cursol.section = (state-1)/2;

        
            }
            if(time>timeLimit)changeState(state+1);

            time += Time.deltaTime;

            break;
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {

		for(int i=0; i<responeTimer.Count; i++){
            responeTimer[i] -= Time.deltaTime;// * timeRate 注意 スローモーション時のここの処理に注意 スローにしたぶんだけここの時間も遅らせる

            if(responeTimer[i] < 0){
                var newCar = Instantiate(carPrefab, spones[i].position, Quaternion.Euler(0,0,0));
                bgcars.Add(newCar);
                newCar.init(this, i, (int)Mathf.Sign(spones[i].localScale.x));

                responeTimer[i] += respDefTime + Random.Range(0f, 1f)*respRange;
            }
        }
	}
    public void carTerminated(BGCar car){
        bgcars.Remove(car);
    }


    private AudioSource audio{get{return GetComponent<AudioSource>();}}
    public List<AudioClip> musics;


}
