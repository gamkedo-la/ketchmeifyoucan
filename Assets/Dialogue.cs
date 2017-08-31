using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {

    public Text dialogueText;

    public void RunDialogue(string dialogue) {
        StartCoroutine(RunDialogueCR(dialogue));
    }

    IEnumerator RunDialogueCR (string d) {
        int letter = 0;
        dialogueText.text = "";
        dialogueText.transform.parent.gameObject.SetActive(true);

        while (letter < d.Length) {
            dialogueText.text += d[letter];
            letter++;
            yield return new WaitForSeconds(0.01f);
        }

        dialogueText.text = d;
    }

    public void EndDialogue() {
        dialogueText.text = "";
        dialogueText.transform.parent.gameObject.SetActive(false);
    }
}
