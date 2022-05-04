using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	float panSpeed = 15f;
	public Transform camTransform;

    // Update is called once per frame
    void Update()
    {
      	if(Input.GetKey(KeyCode.UpArrow))
			camTransform.transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
		else if(Input.GetKey(KeyCode.RightArrow))
			camTransform.transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
		else if(Input.GetKey(KeyCode.DownArrow))
			camTransform.transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
		else if(Input.GetKey(KeyCode.LeftArrow))
			camTransform.transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
    }
}
