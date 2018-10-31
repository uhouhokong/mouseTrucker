using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverButton : MonoBehaviour {

    public LineCircle lc {get{return GetComponent<LineCircle>();}}
    public float r{
        get{return (lc.circle.localScale.x + lc.width) / 2f;}
        set{transform.localScale = new Vector3(value*2f-lc.width, value*2f-lc.width,lc.circle.localScale.z);
            }
    }
    Collider2D col {get{return GetComponent<Collider2D>();}}

    float timeLimit = 10f;
    float time = 0f;

    public TextMesh text;
    public AnimationCurve curve;

    OverButtonEvent ob = null;

	// Use this for initialization
	void Start () {
        camera = GameObject.Find("Main Camera").transform;
        
	}
    public void init(OverButtonEvent ob_ = null, string text_ = "OK?", float timeLimit_ = 2f){
        timeLimit = timeLimit_;
        ob = ob_;
        text.text = text_;
    }
	
    bool dead = false;
	// Update is called once per frame
	void Update () {
        // Debug.Log(mousePoint()+", "+ Input.mousePosition);
        if(col.OverlapPoint(mousePoint())){
            lc.angle = curve.Evaluate(1f-(time/timeLimit));

            if(time > timeLimit){
                if(!dead)
                if(ob!=null)ob.buttonPressed();

                dead = true;
                Destroy(gameObject);
            }
            time+=Time.deltaTime;
        }
        else{
            time = 0;
            lc.angle += (1f-lc.angle) * 0.62f;
        }
		
	}

    public Transform camera;
    protected Vector3 mousePoint(){
        Vector3 posit = Input.mousePosition;
        posit.z = transform.position.z - camera.position.z;
        return Camera.main.ScreenToWorldPoint(posit);
    }
}
public interface OverButtonEvent{
    void buttonPressed();
}
