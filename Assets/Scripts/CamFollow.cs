using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CamFollow : MonoBehaviour {
    public GameObject target;
    public GameObject lockOnTarget;
    public Text scoreText;
    public float rotateSpeed = 5;
    public float zoomSpeed = 5;
    public float rotationDampening = 5;
    public float distanceOffset;
    public float autoCamSpeedSubtraction = 0.5f;

    public Vector3 offsetFactor = Vector3.one;
    public Vector3 zoomInVector = new Vector3(0, 1, -1);
    public Vector3 zoomOutVector = new Vector3(0, -1, 1);

    public float maxZoomDistance = 10;
    Vector3 offset;

    float vertical = 0;

    void Start() {
        offset = new Vector3   (target.transform.position.x - transform.position.x * offsetFactor.x,
                                target.transform.position.y - transform.position.y * offsetFactor.y,
                                target.transform.position.z - transform.position.z * offsetFactor.z);
        if (lockOnTarget == null)
            lockOnTarget = target;
    }
    void LateUpdate() {
        if (lockOnTarget == null)
            lockOnTarget = target;

        Vector3 fromPlayerToTarget = target.transform.position - lockOnTarget.transform.position;
        fromPlayerToTarget.Normalize();
        float horizontal  = Input.GetAxis("Horizontal" + target.GetComponent<Player>().inputID) * rotateSpeed;
        //float lhorizontal = ;
        float lhorizontal = Input.GetAxis("Mouse X" + target.GetComponent<Player>().inputID) * (rotateSpeed / autoCamSpeedSubtraction);
        //if (lockOnTarget == target)

        horizontal += lhorizontal;

        target.transform.Rotate(0, horizontal, 0);

        offset += new Vector3(0, -Input.GetAxis("Mouse Y" + target.GetComponent<Player>().inputID) * (rotateSpeed * 10), 1 * (int)distanceOffset/*-Input.GetAxis("VerticalB")*/) * Time.deltaTime * zoomSpeed;

        float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = target.transform.position - (rotation * Vector3.ClampMagnitude(offset, maxZoomDistance));

        Vector3 lookAtVector = new Vector3(target.transform.position.x, Mathf.Lerp(target.transform.position.y, target.transform.position.y + vertical, Time.deltaTime * rotationDampening), target.transform.position.z);
        transform.LookAt(lookAtVector);

        RaycastHit hit;
        Vector3 direction = transform.TransformDirection(Vector3.forward);
        Ray ray = new Ray(transform.position - (direction / 2), direction);
        Debug.DrawRay(transform.position - (direction / 2), direction);
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.gameObject.tag != "Player" && hit.collider.gameObject.tag != "shield") {
            }
            else {
            }
        }
    }
}
