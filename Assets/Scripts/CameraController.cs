
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private bool move = true;
    public float panSpeed = 30f;
    public float panBorder = 10f;
    public float scrollSpeed = 4f;
    public float minY = 10f;
    public float maxY = 100f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            move = !move; // when escape is pressed, disable screen moving

        if (!move)
            return;

        // moving
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorder ) {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World); // when w is pressed, moves forward, (0, 0, 1)
                                                                                           // * Time.deltaTime becasue we want independency from frame rate
        }

        if (Input.GetKey("s") || Input.mousePosition.y <= panBorder ) {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World); // when w is pressed, moves forward, (0, 0, 1)
                                                                                           // * Time.deltaTime becasue we want independency from frame rate
        }

        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorder ) {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World); // when w is pressed, moves forward, (0, 0, 1)
                                                                                           // * Time.deltaTime becasue we want independency from frame rate
        }

        if (Input.GetKey("a") || Input.mousePosition.x <= panBorder ) {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World); // when w is pressed, moves forward, (0, 0, 1)
                                                                                           // * Time.deltaTime becasue we want independency from frame rate
        }

        // scrolling
        float scroll = Input.GetAxis("Mouse ScrollWheel"); // stroe unity's mouse scrollwheel data in a float
                                                           // scroll up -> positive num; scroll down -> negative num
        Vector3 pos = transform.position;

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY); // clamps the value within min and max
        transform.position = pos;

    }
}
