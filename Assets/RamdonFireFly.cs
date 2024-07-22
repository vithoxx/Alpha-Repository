using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamdonFireFly : MonoBehaviour
{
    public float distance;
    public float speedMultiplier = 1;

    private void Start()
    {
        StartCoroutine(Brain());
    }

    private IEnumerator Brain()
    {
        float speedX, speedY;

        Vector2 angles = new Vector2(Random.Range(0, Mathf.PI *2f), Random.Range(0, Mathf.PI * 2f));

        while(true)
        {
            float waitTime = Random.Range(2, 4f);
            speedX = Mathf.PI * 2f;
            speedY = Random.Range(0, Mathf.PI * 2f);

            for (float i = 0; i < waitTime; i += Time.deltaTime)
            {
                Vector3 position;

                position.y = Mathf.Sin(angles.y) * distance;
                position.x = Mathf.Cos(angles.x) * Mathf.Cos(angles.y) * distance;
                position.z = Mathf.Sin(angles.x) * Mathf.Cos(angles.y) * distance;

                transform.localPosition = position;

                angles.x += speedX * Time.deltaTime * speedMultiplier;
                angles.y  += speedY * Time.deltaTime * speedMultiplier;

                yield return null;
            }

        }
    }
}
