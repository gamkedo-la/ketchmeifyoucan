using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotManager : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown(KeyCode.P)) {
            ScreenCapture.CaptureScreenshot("screenshot1.png");
            Debug.Log("shotted");
        }
	}
}
