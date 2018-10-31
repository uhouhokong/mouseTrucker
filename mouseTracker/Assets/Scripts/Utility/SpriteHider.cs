using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHider : MonoBehaviour {
	void Start () {
		gameObject.GetComponent<SpriteRenderer>().enabled = false;
	}
}
