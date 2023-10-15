using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueManager : MonoBehaviour
{
    public UIManager ui;
    public playerMovement player;

    public TextAsset completeText;
    public string dialogueString;
    public string[] speakerSections;
    public string[] sentenceList;
    private int currentSpeaker = 0;
    private GameObject _speaker1;
    private GameObject _speaker2;

    public interface Dialogue
    {
        TextAsset getDialogue();
        GameObject getSpeaker1();
        GameObject getSpeaker2();
    }

    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.FindWithTag("UI").GetComponent<UIManager>();
        player = GameObject.FindWithTag("Player").GetComponent<playerMovement>();
    }

    //CJ causing chaos
    public void easyDialogue(TextAsset text)
    {
        dialogueString = text.text;

        GameManager.inCinematic = true;
        ui.calculateUI(ui.dialogueBox, ui.dialogueDistance, 0.2f);

        speakerSections = textSplit(dialogueString, '^');
        cjDisplaySection();
    }

    public void easyDialogue(string text)
    {
        dialogueString = text;

        GameManager.inCinematic = true;
        ui.calculateUI(ui.dialogueBox, ui.dialogueDistance, 0.2f);

        speakerSections = textSplit(dialogueString, '^');
        cjDisplaySection();
    }
    private void cjDisplaySection()
    {
        if (currentSpeaker == speakerSections.Length)
        {
            player.endDialogue();
            ui.endDialogue();
            currentSpeaker = 0;
        }
        else
        {
            sentenceList = textSplit(speakerSections[currentSpeaker], '*');
            player.ZoomIn();

            ui.displayPassage(sentenceList);
        }
    }

    public void getDialogue(GameObject target)
    {
        Dialogue text = target.GetComponent<Dialogue>();
        completeText = text.getDialogue();
        _speaker1 = text.getSpeaker1();
        _speaker2 = text.getSpeaker2();
        startDialogue(completeText, _speaker1, _speaker2);
    }
    public void startDialogue(TextAsset text, GameObject speak1, GameObject speak2)
    {
        dialogueString = text.text;
        _speaker1 = speak1;
        _speaker2 = speak2;

        player.playerAnim.SetFloat("Speed", 0f);
        player.playerAnim.SetFloat("Direction", -1f);
        player.playerAnim.SetBool("Playing", false);

        GameManager.inCinematic = true;
        ui.calculateUI(ui.dialogueBox, ui.dialogueDistance, 0.2f);

        speakerSections = textSplit(dialogueString, '^');
        displaySection();
    }

    private string[] textSplit(string text, char symbol)
    {
        return text.Split(symbol);
    }

    private void displaySection()
    {
        if (currentSpeaker == speakerSections.Length)
        {
            player.endDialogue();
            ui.endDialogue();
            currentSpeaker = 0;
        }
        else
        {
            sentenceList = textSplit(speakerSections[currentSpeaker], '*');

            if (currentSpeaker % 2 == 0)
            {
                player.ZoomIn(_speaker1);
            }
            else
            {
                player.ZoomIn(_speaker2);
            }

            ui.displayPassage(sentenceList);
        }
    }

    public void nextSection()
    {
        currentSpeaker++;
        displaySection();
    }
}
