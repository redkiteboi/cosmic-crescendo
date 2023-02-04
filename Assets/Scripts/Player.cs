using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    private PlayerInput playerInput;
    private Camera cam;

    private SpaceObject[] selectedObjects = new SpaceObject[2];

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        cam = playerInput.camera;
    }

    public void Click(CallbackContext ctx)
    {
        SpaceObject s = DetectSpaceObject();
        switch (ctx.phase)
        {
            case InputActionPhase.Performed:
                selectedObjects[0] = s;
                if (s == null) return;
                s.Select();
                break;
            case InputActionPhase.Canceled:
                selectedObjects[1] = s;
                selectedObjects[0].Deselect();
                if (s == null) return;
                GameMaster.MergePlanets(selectedObjects);
                break;
            default:
                break;
        }
    }

    private SpaceObject DetectSpaceObject()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(ray, out hit);
        if (hit.collider == null) return null;
        return hit.collider.gameObject.GetComponent<SpaceObject>();
    }
}
