using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    public Transform objectToRotate;
    public float duration = 1f;

    private Quaternion startRotation;
    private Quaternion endRotation;

    private bool isRotating = false;
    // Start is called before the first frame update
    public void startRotate()
    {
        if (isRotating)
            return;

        isRotating = true;

        startRotation = objectToRotate.rotation;
        endRotation = Quaternion.Euler(0f, -90f, 0f) * startRotation;

        StartCoroutine(RotateGate());
    }

    // Update is called once per frame
    IEnumerator RotateGate()
    {
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            objectToRotate.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        objectToRotate.rotation = endRotation;
        isRotating = false;
    }
}
