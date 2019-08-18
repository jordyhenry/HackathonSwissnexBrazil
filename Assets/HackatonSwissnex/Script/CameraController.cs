using UnityEngine;
using UnityEngine.XR;

public class CameraController : MonoBehaviour
{
    Vector2 rotation = new Vector2(0, 0);

    public float speed = 3;
    public GameObject handPrefab;
    public string collisionTag = "ground";
    public Vector2 handScaleBoundaries;
    public float rayDistance = 10f;
    public Transform rigTransform;
    public Transform handTransform;
    public LineRenderer handLine;

    private void Awake()
    {

    }
    private void Start()
    {
        handLine = handTransform.GetComponent<LineRenderer>();
    }

    bool triggerPressed = false;
    bool lastTriggerPressed = false;
    void Update()
    {
        //rotation.y += Input.GetAxis("Mouse X");
        //rotation.x += -Input.GetAxis("Mouse Y");
        //transform.eulerAngles = (Vector2)rotation * speed;

        if (Input.GetKey(KeyCode.Joystick1Button9))
            transform.position += transform.forward * speed * Time.deltaTime;

        float triggerAxis = Input.GetAxis("RightTriggerAxis");
        if (!triggerPressed)
        {
            if (triggerAxis >= .9)
            {
                triggerPressed = true;
                lastTriggerPressed = true;
            }
        }
        else
        {
            if (triggerAxis <= .1)
            {
                triggerPressed = false;
                lastTriggerPressed = false;
            }
        }

        handLine.SetPosition(0, handTransform.position);
        handLine.SetPosition(1, handTransform.forward * rayDistance);

        if (triggerPressed)
        {
            Ray ray = new Ray(handTransform.position, handTransform.forward);
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(ray, out hitInfo, rayDistance))
            {
                if (hitInfo.collider.CompareTag(collisionTag))
                {
                    CreateHand(hitInfo.point);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button9))
        {
            Ray ray = new Ray(handTransform.position, handTransform.forward);
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(ray, out hitInfo, rayDistance))
            {
                if (hitInfo.collider.CompareTag(collisionTag))
                {
                    rigTransform.position = hitInfo.point;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(handTransform.position, handTransform.forward);
    }

    void CreateHand(Vector3 pos)
    {
        GameObject hand = Instantiate(handPrefab, pos, Quaternion.identity);
        hand.transform.localScale = Vector3.one * (Random.Range(handScaleBoundaries.x, handScaleBoundaries.y));
        Vector3 rot = Vector3.one;
        rot.y = Random.Range(0f, 360f);
        hand.transform.rotation = Quaternion.Euler(rot);
    }
}
