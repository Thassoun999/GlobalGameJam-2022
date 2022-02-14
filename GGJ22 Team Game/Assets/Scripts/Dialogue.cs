using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dialogue : MonoBehaviour
{
    // Reference to dialogue box
    public TextMeshProUGUI textDisplay;

    // index to cycle through array of sentences
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public Animator textDisplayAnim;

    // Make sure that we can't continue the text before the first piece of text finishes
    public GameObject continueButton;
    public GameObject textBox;


    public void StartTaling()
    {
        // POTENTIAL SOUND HERE

        // This coroutine should start if a player goes up to a goes NPC, hits SPACE, and engages in a CONVERSATION (is the player in a conversation? Yes? No?)
        // Make the Text Box visible, set index to 0, and start the coroutine! Also make sure that player is inConversation!!!
        // ONLY DO THIS IF PLAYER IS NOT IN CONVERSATION SO WE DON'T LOOP THIS INFINITELY!!!
        if (!GameManager.instance.player.inCoversation){
            GameManager.instance.player.inCoversation = true;
            textBox.SetActive(true);
            continueButton.SetActive(false); // This can't be true yet, not until sentence finishes
            index = 0;
            StartCoroutine(Type());
        }

    }

    private void StopTalking()
    {
        GameManager.instance.player.inCoversation = false;
        textBox.SetActive(false);
    }


    private void Update()
    {
        if (GameManager.instance.player.inCoversation){
            // Ensures player can't spam the continue button before the sentence ends
            if(textDisplay.text == sentences[index])
            {
                continueButton.SetActive(true);
            }

            // You can click on the continue button or you can hit ENTER / SPACE
            if (continueButton.activeSelf == true)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                    NextSentence();
            }
        }
    }

    // Coroutines act as functions but all the code inside it doesn't need to be run immediately
    IEnumerator Type(){
        // So quick example of what was explained above
        // yield return new WaitForSeconds(2.0f); will literally wait that amount of time before continuing to run said coroutine!

        // This will ensure that characters in the dialogue box appear one at a time (RPG talking) -> This is where sound should be in the dialogue
        foreach(char letter in sentences[index].ToCharArray())
        {
            // TYPING SOUND HERE

            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence(){
        // BUTTON CLICK SOUND HERE

        // Ensures player can't spam the continue button before the sentence ends pt.2
        textDisplayAnim.SetTrigger("Change");
        continueButton.SetActive(false);

        if(index < sentences.Length - 1){
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        } else {
            // We reached the end!!!!! No more talking!!!
            textDisplay.text = "";
            continueButton.SetActive(false);
            StopTalking();

        }
    }
}
