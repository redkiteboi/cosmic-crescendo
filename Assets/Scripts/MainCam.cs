using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCam : MonoBehaviour
{
    [SerializeField][ContextMenuItem("Look at Vector3.zero", "OnValidate")] public Vector3 goalPos;
    [SerializeField][Range(0f, 1f)] private float camSpeed = 0.025f;

    private void Start()
    {
        StartCoroutine(Animate());
    }

    private void OnValidate()
    {
        transform.LookAt(Vector3.zero);
    }

    private IEnumerator Animate()
    {
        while (true)
        {
            transform.position = Vector3.Lerp(transform.position, goalPos, camSpeed);
            transform.LookAt(Vector3.zero);
            yield return new WaitForFixedUpdate();
        }
    }

}
