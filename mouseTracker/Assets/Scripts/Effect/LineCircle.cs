using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCircle : MonoBehaviour {
    Vector2 posit{get{return transform.position;}}
    public float r{
        get{return (circle.localScale.x + width) / 2f;}
        set{circle.localScale = new Vector3(value*2f-width, value*2f-width,circle.localScale.z);}
    }
    public float angle = 1;
    public LineRenderer l{get{return GetComponent<LineRenderer>();}}

    public Transform circle;
    public float width{
        set{l.startWidth = value; l.endWidth = value;}
        get{return l.startWidth;}
    }

    public Color color{
        set{l.startColor = value; l.endColor = value;}
        get{return l.startColor;}
    }

    int fineness = 50;
    public AnimationCurve finenessCurve;

	// Use this for initialization
	void Start () {
		l.positionCount=fineness;
	}
	
	// Update is called once per frame
	void Update () {
        fineness = (int)finenessCurve.Evaluate(r);
        l.positionCount=fineness;
		for(int i=0; i<l.positionCount; i++){
            float rad = 2f* Mathf.PI* angle * (float)i/(l.positionCount-1) + Mathf.PI/2f;
            l.SetPosition(i, r * new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)));
        }
	}
}
