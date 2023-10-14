using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berta_FirstConvo : MonoBehaviour, inventoryManager.Interact, dialogueManager.Dialogue
{
    public TextAsset text;
    public GameObject spkr1;
    public GameObject spkr2;

    public float fadeTime = 3f; // How long the fade should take in seconds
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    private dialogueManager dialogue;

    private void Start()
    {
        dialogue = GameObject.FindWithTag("UI").GetComponent<dialogueManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void interaction()
    {
        //Debug.Log("Here is where I lore dump then open the door");
        dialogue.startDialogue(text, spkr1, spkr2);
        GameManager.Progression++;
        StartCoroutine(FadeOut());
    }

    public TextAsset getDialogue()
    {
        return text;
    }
    public GameObject getSpeaker1()
    {
        return spkr1;
    }

    public GameObject getSpeaker2()
    {
        return spkr2;
    }
    IEnumerator FadeOut()
    {
        // Get the initial color of the sprite
        Color spriteColor = spriteRenderer.color;

        // Calculate the amount to decrease the alpha by for each frame of the fade
        float alphaChangePerFrame = spriteColor.a / (fadeTime / Time.deltaTime);

        // Loop until the alpha of the sprite is 0
        while (spriteColor.a > 0)
        {
            // Decrease the alpha of the sprite by the calculated amount for this frame
            spriteColor.a -= alphaChangePerFrame;

            // Set the color of the sprite to the new color with the decreased alpha
            spriteRenderer.color = spriteColor;

            // Wait for the next frame
            yield return null;
        }

        // Once the alpha is 0, destroy the game object
        Destroy(gameObject);
    }
}
