using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {

    public int nodeLength = 10;

    public GameObject head, tail;

    public GameObject nodeInstance;
	public List<GameObject> vertices = new List<GameObject>();
    LineRenderer line;

    void Start () 
    {	
        makeNodes();
        line = GetComponent<LineRenderer>();
        // line.material =  new Material(Shader.Find("Unlit/Color"));
        line.positionCount = vertices.Count;

        foreach (GameObject v in vertices)
        {            
            // v.GetComponent<MeshRenderer> ().enabled = false;
        }
    }
	
    void Update () 
    {
        int idx = 0;
        foreach (GameObject v in vertices)
        {
            line.SetPosition(idx, v.transform.position);
            idx++;
        }
    }

    void makeNodes(){
        vertices.Clear();
        Vector3 dist = (tail.transform.position - head.transform.position);
        vertices.Add(tail);

        GameObject pre = null;
        for(int i=0; i<nodeLength+2; i++){
            var cur = Instantiate(nodeInstance, tail.transform.position - dist * (i)/(nodeLength+1), Quaternion.Euler(0,0,0));
            cur.transform.parent = transform;
            vertices.Add(cur);

            if(i == 0)tail.GetComponent<HingeJoint>().connectedBody = cur.GetComponent<Rigidbody>();
            else pre.GetComponent<HingeJoint>().connectedBody = cur.GetComponent<Rigidbody>();

            pre = cur;
        }
        pre.GetComponent<HingeJoint>().connectedBody = head.GetComponent<Rigidbody>();
        vertices.Add(head);
    }

    public AnimationCurve tenseRateCurve;
    public void ropeTense(float r){
        foreach(var g in vertices){
            var j = g.GetComponent<HingeJoint>();
            if(j==null)continue;
            var l = j.limits;
            l.min = -180 * (1f-tenseRateCurve.Evaluate(r));
            l.max =  180 * (1f-tenseRateCurve.Evaluate(r));
            j.limits = l;
            // Debug.Log(r);
        }
    }
}
