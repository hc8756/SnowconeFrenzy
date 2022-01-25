using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class DragDropper : MonoBehaviour
{
    [SerializeField]
    private InputAction mouseClick;
 
    private Camera mainCamera;
    private Vector3 velocity = Vector3.zero;
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    private float mouseDragSpeed = 0.0f;

    private void Awake() {
        mainCamera = Camera.main;
    }
    private void OnEnable() {
        mouseClick.Enable();
        mouseClick.performed += MousePressed;
    }
    private void OnDisable() {
        mouseClick.performed -= MousePressed;
        mouseClick.Disable();
    }
    private void MousePressed(InputAction.CallbackContext context) {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);
        if (hit2D.collider != null && hit2D.collider.gameObject.CompareTag("Draggable")) {
            StartCoroutine(DragUpdate(hit2D.collider.gameObject));
        }
    }

    private IEnumerator DragUpdate(GameObject clickedObject) {
        float initialDistance = Vector3.Distance(clickedObject.transform.position, mainCamera.transform.position);
        clickedObject.TryGetComponent<Rigidbody>(out var rb);
        while (mouseClick.ReadValue<float>() != 0) {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position, ray.GetPoint(initialDistance), ref velocity, mouseDragSpeed);
            yield return null;
        }
    }
}
