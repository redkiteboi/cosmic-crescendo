using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCam : MonoBehaviour
{
    [SerializeField] public Vector3 goalPos;
    [SerializeField][Range(0f, 1f)] private float camSpeed = 0.025f;

    private void Start()
    {
        goalPos = transform.position;
        StartCoroutine(Animate());
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
