using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedDoor : MonoBehaviour
{
    public int progressAfter;
    public Transform objectToRotate;
    public float duration = 1f;

    private Quaternion startRotation;
    private Quaternion endRotation;
    private bool hasRotated = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Progression >= progressAfter && !hasRotated)
        {
            //open the door but I'm just deleting it for now
            startRotate();
        }
    }

    private bool isRotating = false;
    // Start is called before the first frame update
    public void startRotate()
    {
        if (isRotating)
            return;

        isRotating = true;
        hasRotated = true;

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
