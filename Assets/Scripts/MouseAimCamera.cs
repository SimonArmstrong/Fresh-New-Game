using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MouseAimCamera : MonoBehaviour {
	public GameObject target;
    public Text scoreText;
	public float rotateSpeed = 5;
	Vector3 offset;
	
	void Start() {
		offset = target.transform.position - transform.position;
	}
	
	void LateUpdate() {
		float horizontal = Input.GetAxis("Mouse X" + target.GetComponent<Player>().id) * rotateSpeed;
		target.transform.Rotate(0, horizontal, 0);

		float desiredAngle = target.transform.eulerAngles.y;
		Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
		transform.position = target.transform.position - (rotation * offset);
		
		transform.LookAt(target.transform);

        scoreText.text = target.GetComponent<Player>().score.ToString();
	}
}