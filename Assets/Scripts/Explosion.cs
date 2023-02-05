using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Explosion : MonoBehaviour
{

    [SerializeField] private int pointsCount;
    [SerializeField] private float maxRadius;
    [SerializeField] private float speed;
    [SerializeField] private float startWidth;
    [SerializeField] private Image fadeScreen; 
    

    private LineRenderer lineRenderer;
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = pointsCount + 1;
    }

    /*public void Explode()
    {
        gameObject.SetActive(true);
        StartCoroutine(Blast());
    }*/

    private IEnumerator Blast()
    {
        float currentRadius = 0f;

        while (currentRadius < maxRadius)
        {
            currentRadius += Time.deltaTime * speed;
           Draw(currentRadius);
           yield return null;
        }
    }

    private void Draw( float currentRadius)
    {
        float angleBetweenPoints = 360f / pointsCount;

        for (int i = 0; i <= pointsCount; i++)
        {
            float angle = i * angleBetweenPoints * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f);
            Vector3 position = direction * currentRadius;
            
            lineRenderer.SetPosition(i,position);
        }

        lineRenderer.widthMultiplier = Mathf.Lerp(0f, startWidth, 1f - currentRadius / maxRadius);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(Blast());

            //put Screen Fade here 
        }
        
       
    }
}
