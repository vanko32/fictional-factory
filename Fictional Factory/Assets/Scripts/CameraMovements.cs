using UnityEngine;

public class CameraMovements : MonoBehaviour
{
    Camera mainCamera;
    private static float zoomingSpeed = 20f;
    private static float minZoom = 3f;
    private static float maxZoom = 90f;

    private static float panningSpeed = 100f; 
    private static float minX = -250f;
    private static float maxX = 80f;
    private static float minZ = -350f;
    private static float maxZ = 0f;

    private Vector3 mouseClickPosition;


    void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseClickPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Panning(Input.mousePosition);
        }

        float scrolling = Input.GetAxis("Mouse ScrollWheel");
        Zooming(scrolling);
    }

   
    // zoom nefunguje na touchpade, ale iba cez kurzor mysi 
    void Zooming(float scrolling)
    {
        if (scrolling == 0)
        {
            return;
        }
        else
        {
            mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView - (scrolling * zoomingSpeed), minZoom, maxZoom);
        }
    }
    

    void Panning(Vector3 newMousePosition)
    {
        Vector3 direction = mainCamera.ScreenToViewportPoint(mouseClickPosition - newMousePosition);
        Vector3 movement = new Vector3(direction.x * panningSpeed, 0, direction.y * panningSpeed);
        mainCamera.transform.Translate(movement, Space.World);

        Vector3 checkPosition = mainCamera.transform.position;
        checkPosition.x = Mathf.Clamp(mainCamera.transform.position.x, minX, maxX);
        checkPosition.z = Mathf.Clamp(mainCamera.transform.position.z, minZ, maxZ);
        transform.position = checkPosition;

        mouseClickPosition = newMousePosition;
    }
}
