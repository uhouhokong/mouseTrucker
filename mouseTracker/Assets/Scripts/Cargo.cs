using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo : MonoBehaviour {

    private Rigidbody rb{get{return GetComponent<Rigidbody>();}}
    public AudioSource audio{get {return GetComponent<AudioSource>();}}

    public AnimationCurve frecRate;

    public int weightType = 0;
    public bool slowable = false;
    List<float> tenseRate = new List<float>(){25.5f, 2.5f, 8.8f};

	// Use this for initialization
	void Start () {
        audio.Play();
        audio.volume = 0.38f;
	}
	
	// Update is called once per frame
	void Update () {
        if(slowable)Time.timeScale += (0.34f - Time.timeScale) * 0.6f;
        if(Input.GetKeyDown(KeyCode.Z))slowable = !slowable;
        audio.pitch = 0.65f + 0.35f * Time.timeScale;
	}

    void FixedUpdate(){
        rb.velocity -= rb.velocity * 0.18f;
    }

    public void getTense(float tense, float xTense, Vector3 dist){
        if(tense > 0.98 && xTense > 0){
            //if(slowable)Time.timeScale += (0.34f - Time.timeScale) * 0.6f;
            rb.AddForce(Vector3.left * tense * tenseRate[weightType] ,ForceMode.Force);
        }
        else ;//Time.timeScale += (1f - Time.timeScale) * 0.6f;
        
    }
}
