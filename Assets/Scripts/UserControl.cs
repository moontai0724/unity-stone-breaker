using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserControl : MonoBehaviour
{
    void Update()
    {
        if (BallControl.started == true && Mathf.Abs(Input.mousePosition.x - Camera.main.scaledPixelWidth / 2) < Camera.main.scaledPixelWidth / 2)
        {
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = Vector2.Lerp(transform.position, new Vector3(targetPosition.x, transform.position.y, transform.position.z), 0.8f);
        }
    }
}