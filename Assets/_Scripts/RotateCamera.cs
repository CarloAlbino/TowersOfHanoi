using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour {

    public bool autoRotate = true;
    public float speed = 1;

	void Update ()
    {
		if(autoRotate)
        {
            transform.Rotate(Vector3.up, speed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up, speed * 3 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, -speed * 3 * Time.deltaTime);
        }

	}

    public void StartGame()
    {
        autoRotate = false;
        transform.rotation = Quaternion.identity;
    }
}
