using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector2 rotation = new Vector2(0, 0);
    public float speed = 3;
    public GameObject handPrefab;
    public string collisionTag = "ground";
    public Vector2 handScaleBoundaries;
    public float rayDistance = 10f;

    private void Start()
    {
        
    }

    void Update()
    {
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        transform.eulerAngles = (Vector2)rotation * speed;

        if (Input.GetKey(KeyCode.W))
            transform.position += transform.forward * speed * Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(ray, out hitInfo, rayDistance))
            {
                if (hitInfo.collider.CompareTag(collisionTag))
                {
                    CreateHand(hitInfo.point);
                }
            }
        }
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
