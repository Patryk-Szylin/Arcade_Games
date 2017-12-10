using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraScript : NetworkBehaviour
{

    #region SINGLETON
    private static CameraScript _instance;
    public static CameraScript Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<CameraScript>();

                // If the GameObject with GameManager wasn't found, create one.
                if (_instance == null)
                    _instance = new GameObject("Camera").AddComponent<CameraScript>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this.gameObject);
    }
    #endregion

    public Transform target;

    [Header("Camera Restriction")]
    public float xPosRestriction; // Left/Right
    public float zPosRestriction; // Up/Down
    private float camaraHeight = 28; // TODO
    private bool mouseToogle = false;

    [Header("Mouse Cursor")]
    public Texture2D cursorTexture;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    [Header("Camera Shake")]
    //public bool cameraShake;
    public float duration;
    public float magnitude;
    private bool running = false;

    // Use this for initialization
    public void Setup (Transform playerTarget) {
        target = playerTarget;
        transform.parent = null;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

    // Update is called once per frame
    void Update() {

        if (target == null)
            return;

        // Toogle Camara Follow
        if (Input.GetKeyDown(KeyCode.LeftAlt))
            mouseToogle = !mouseToogle;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            CameraShake();

        if (mouseToogle)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Mouse Position
            Vector3 mousePosition;
            if (Physics.Raycast(ray, out hit, 100)) { }
            mousePosition = hit.point;

            Vector3 midPoint = (mousePosition + target.transform.position) / 2;

            // Camera clamping
            Vector3 clampedPos = new Vector3(Mathf.Clamp(midPoint.x, (target.position.x - xPosRestriction), (target.position.x + xPosRestriction)),
                                            camaraHeight,
                                            Mathf.Clamp(midPoint.z, (target.position.z - zPosRestriction), (target.position.z + zPosRestriction)));

            transform.position = clampedPos;
        }
        else
        {
            transform.position = new Vector3 (target.transform.position.x, camaraHeight, target.transform.position.z - 4);
        }
    }

    public void CameraShake() {
        StartCoroutine(Shake());
    }

    // Camera Shake
    public IEnumerator Shake()
    {
        //if (cameraShake == true)
        //{
            if (running == false)
            {
                running = true;

                float elapsed = 0.0f;

                Vector3 originalCamPos = Camera.main.transform.position;

                while (elapsed < duration)
                {
                    elapsed += Time.deltaTime;

                    float percentComplete = elapsed / duration;
                    float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

                    // map value to [-1, 1]
                    float x = Random.value * 2.0f - 1.0f;
                    float y = Random.value * 2.0f - 1.0f;
                    x *= magnitude * damper;
                    y *= magnitude * damper;
                    x += originalCamPos.x;
                    y += originalCamPos.y;

                    Camera.main.transform.position = new Vector3(x, y, originalCamPos.z);

                    yield return null;
                }

                Camera.main.transform.position = originalCamPos;
                running = false;
            }
        //}
    }
}
