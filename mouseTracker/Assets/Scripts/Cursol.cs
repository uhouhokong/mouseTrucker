using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursol : MonoBehaviour, OverButtonEvent {

    public Collider2D mouseableRange;

    public GameObject tail;
    public Rope rope;
    public Transform cursolNode;
    public Cargo cargo;

    public float tense;
    private float stLength;
    private Rigidbody rb{get{return GetComponent<Rigidbody>();}}
    [SerializeField]
    private bool moveable =false;

    public List<SpriteRenderer> blinds;
    public float blindAlphaDefault;
    public float blindAlpha{
        get{return blinds[0].color.a/blindAlphaDefault;}
        set{foreach(var s in blinds)s.color= new Color(s.color.r, s.color.g, s.color.b, blindAlphaDefault*value);}
    }

	// Use this for initialization
    float camera2cargoDist;
    float cameraReach;

    public Transform targetPoint;

    public OverButton buttonPrefab;
    float target2tailDist;
    float targetReach;

    public int state=0;
    void changeState(int next){
        state = next;
        stateWakeup();
        Time.timeScale=1f;
    }

    public int section = 0;

    void stateWakeup(){
        OverButton b;
        switch(state){
            case 0://カーソル合わせ
            setNewCameraPoint();
            b =  Instantiate(buttonPrefab, tail.transform.position + new Vector3(-0.8f, 0,0), Quaternion.identity);
            b.init(this, "here", 2.0f);
            break;
            case 1://動かし中
            break;
            case 2://完了後のディレイ
            Destroy(pullReminder);
            StartCoroutine(audioFeedOut());

            answerReminder = Instantiate(answerReminderPrefab);
            answerReminder.transform.parent = camera;
            answerReminder.transform.localPosition = new Vector3(0, 0.82f, 0);
            answerReminder.transform.position = new Vector3(answerReminder.transform.position.x, answerReminder.transform.position.y, 0);
            if(section == 2){
                answerReminder.GetComponent<TextMesh>().text = "you complited tasks.\nPlease answer some questions.\n";
            }else{

                b =  Instantiate(buttonPrefab, tail.transform.position + new Vector3(0f, -1.05f,0), Quaternion.identity);
                b.init(this, "OK?", 2.0f);
                b.r = 0.27f;
                b.text.fontSize = (int)(b.text.fontSize * 0.6f);
                b.transform.parent = camera;
                b.transform.localPosition = new Vector3(1.25f, -1.15f,0);
                b.transform.position = new Vector3(b.transform.position.x, b.transform.position.y, 0);
            }

            
            break;
        }
    }

    public IEnumerator audioFeedOut(){
        float time=0;
        float timeLimit = 4.0f;
        for(time=0; time < timeLimit; time+=Time.deltaTime){
            cargo.audio.volume = 0.45f * (1f - time/timeLimit);
            yield return null;
        }
        cargo.audio.volume = 0;

    }

    public GameObject answerReminderPrefab;
    GameObject answerReminder;

    public GameObject pullReminderPrefab;
    GameObject pullReminder;
    public void buttonPressed(){
        switch(state){
            case 0://カーソル合わせ
            changeState(1);
            break;
            case 1://動かし中
            break;
            case 2://完了後のディレイ
            Destroy(leftNumber.gameObject);
            Destroy(sectionCount.gameObject);
            Destroy(answerReminder);
            GameObject.Find("Environment").GetComponent<Scene>().finishedTasks();
            break;
        }
    }

	void Start () {
        camera = GameObject.Find("Main Camera").transform;

        camera2cargoDist = camera.position.x - cargo.transform.position.x;
        target2tailDist = targetPoint.position.x - tail.transform.position.x;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

		stLength = (transform.position - tail.transform.position).magnitude + 0.3f;

        blindAlphaDefault = blinds[0].color.a;
        blindAlpha = 0;

        changeState(0);
        pullReminder = Instantiate(pullReminderPrefab);
        pullReminder.transform.parent = camera;
        pullReminder.transform.localPosition = new Vector3(0, 1.02f, 0);
        pullReminder.transform.position = new Vector3(pullReminder.transform.position.x, pullReminder.transform.position.y, 0);

        leftNumber.text = "left: "+taskNum+"times";
        if(section==0)      sectionCount.text = "step: "+"A, weight: 0";
        else if(section==1) sectionCount.text = "step: "+"B, weight: 10";
        else                sectionCount.text = "step: "+"C, weight: ?";
        leftNumber.transform.parent = camera;
        sectionCount.transform.parent = camera;
	}
    int taskNum = 2;

    int step=0;
    public TextMesh leftNumber;
    public TextMesh sectionCount;
    void clear(){
        setNewCameraPoint();
        taskNum--;
        leftNumber.text = "left: "+taskNum+"times";
        if(taskNum==0){
            changeState(2);
        }

    }

	void setNewCameraPoint(){
        cargo.GetComponent<Rigidbody>().velocity = Vector2.zero;
        cameraReach = cargo.transform.position.x + camera2cargoDist;
        targetReach = tail.transform.position.x + target2tailDist;
    }

	// Update is called once per frame
	void Update () {
        // if(Input.GetKeyDown(KeyCode.K))setNewCameraPoint();
        if(Mathf.Abs(camera.position.x - cameraReach) > 0.1f){
            camera.position = new Vector3(camera.position.x + (-camera.position.x + cameraReach)*0.17f , camera.position.y, camera.position.z);
        }
        if(Mathf.Abs(targetPoint.position.x - targetReach) > 0.1f){
            targetPoint.position = new Vector3(targetPoint.position.x + (-targetPoint.position.x + targetReach)*0.05f , targetPoint.position.y, targetPoint.position.z);
        }else{
            if(tail.transform.position.x - targetPoint.position.x < 0)clear();
        }

        
        Cursor.visible = false;
        Vector3 target = mousePoint();
        Vector3 dist = mousePoint() - tail.transform.position;
        if(dist.magnitude > stLength)target = tail.transform.position + dist * stLength/dist.magnitude;
        tense = (mousePoint() - tail.transform.position).magnitude / stLength;
        
        var xTense = (mousePoint() - tail.transform.position).x / stLength;
        
        moveable = mouseableRange.OverlapPoint(target) & Mathf.Abs(targetPoint.position.x - targetReach) <= 0.1f;

        switch(state){
            case 0://カーソル合わせ
            blindAlpha += (1-blindAlpha) * 0.8f;
            transform.position = mousePoint();
            if(moveable){
            rope.ropeTense(tense);
            // transform.position = target;
            if(tense < 1f)cursolNode.transform.position = transform.position;
            }
            break;
            case 1://動かし中
            if(moveable){
            blindAlpha += (0-blindAlpha) * 0.3f;
            rope.ropeTense(tense);
            cargo.getTense(tense, -xTense, dist);
            // rb.velocity = (target - transform.position)/Time.deltaTime;
            transform.position = target;
            cursolNode.transform.position = transform.position;
            }
            else changeState(0);
            break;
            case 2://カーソル合わせ
            blindAlpha += (1-blindAlpha) * 0.8f;
            transform.position = mousePoint();
            break;
        }
		
	}

    public Transform camera;
    protected Vector3 mousePoint(){
        Vector3 posit = Input.mousePosition;
        posit.z = transform.position.z - camera.position.z;
        return Camera.main.ScreenToWorldPoint(posit);
    }

    protected float getAimngRad(Vector3 dist){
        var direction = 1;
			float radBuf = Mathf.Atan2(dist.y,dist.x);
			radBuf = ((radBuf - Mathf.PI*(1-direction)/2) % (Mathf.PI*2));
			radBuf = radBuf + Mathf.PI*(1-direction)/2;
			return radBuf;
			// arm.rad = radBuf + (1-direction)*Mathf.PI/2;
		}
}
