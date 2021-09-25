using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continue : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
            SceneController.start();
    }
}
