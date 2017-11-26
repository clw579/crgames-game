using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInteraction : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		Debug.Log(gameObject.name);
	}

	void OnMouseEnter() {
        gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
		gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
    
    void OnMouseExit() {
        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
