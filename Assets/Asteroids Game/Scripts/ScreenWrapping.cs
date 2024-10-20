using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScreenWrapping : MonoBehaviour
{
    [Header("Screen Space")]
    [SerializeField] private float screenTop = 10.5f;
    [SerializeField] private float screenBottom = -10.5f;
    [SerializeField] private float screenLeft = -21.6f;
    [SerializeField] private float screenRight = 21.6f;


    private void Update()
    {
        ScreenWrap();
    }

    private void ScreenWrap()
    {
        // Save Player Transform
        Vector2 newPos = transform.position;

        // screenwrapping stops you going off screen
        if (transform.position.y > screenTop)
        {
            newPos.y = screenBottom;
        }
        if (transform.position.y < screenBottom)
        {
            newPos.y = screenTop;
        }
        if (transform.position.x > screenRight)
        {
            newPos.x = screenLeft;
        }
        if (transform.position.x < screenLeft)
        {
            newPos.x = screenRight;
        }
        // Update Player Transform based on screen pos
        transform.position = newPos;
    }

}
