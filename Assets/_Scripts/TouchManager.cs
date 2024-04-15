using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchManager : MonoBehaviour {

    public Text tCount;

	// Update is called once per frame
	void Update () {
        tCount.text = Input.touchCount.ToString();
	}
}
