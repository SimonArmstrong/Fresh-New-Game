using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MouseAimCamera : MonoBehaviour {
	public GameObject target;
    public Text scoreText;
	public float rotateSpeed = 5;
	public Vector3 offset;
	public float height = 1;

	void Start() {
		offset = target.transform.position - transform.position;
	}
	
	void LateUpdate() {
		float horizontal = Input.GetAxis("Mouse X" + target.GetComponent<Player>().inputID) * rotateSpeed;
		target.transform.Rotate(0, horizontal, 0);

		float desiredAngle = target.transform.eulerAngles.y;
		Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
		transform.position = target.transform.position - (rotation * offset);
        Vector3 targetLookAt = new Vector3(0, height, 0);

        transform.LookAt(targetLookAt + target.transform.position);

        //scoreText.text = target.GetComponent<Player>().score.ToString();
	}
}