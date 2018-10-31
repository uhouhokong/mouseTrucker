using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGCar : MonoBehaviour {

    public Scene scene;

    public int directionX{
		set{transform.localScale = new Vector3(Mathf.Sign(value)*Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);}
		get {return (int)Mathf.Sign(transform.localScale.x);}
	}

    public int line = 0;
    public float timeRatio{get;set;}//0-1

    public Rigidbody rb{get{return GetComponent<Rigidbody>();}}

	// Use this for initialization
	void Start () {
		rb.velocity = new Vector2(directionX * 10f, 0);
        // if(scene!=null)scene.bgcars.Add(this);
	}

    //Sceneから呼び出す(instantiateと同時に)
    public void init(Scene _s, int _line, int direction){
        scene = _s;
        line = _line;
        directionX = direction;
    }
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter(Collider other){
        if(other.tag == "End"){
            scene.carTerminated(this);
            Destroy(gameObject);
        }
    }
}
