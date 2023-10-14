using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideKey : MonoBehaviour, inventoryManager.Interact
{
    public Collider otherObject;

    // Start is called before the first frame update

    public  void interaction()
    {
        StartCoroutine(SmoothMove());
        otherObject.enabled = true;
        gameObject.tag = "Untagged";
    }

    IEnumerator SmoothMove()
    {
        Vector3 targetPosition = transform.position + new Vector3(1f, 0f, 0f);
        float timeToMove = 3f;
        float elapsedTime = 0f;

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }

}
