using System.Collections;
using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI dialogText;

    public float typingSpeed = 0.05f;

    public void StartDialog(string message)
    {
        StopAllCoroutines();
        StartCoroutine(TypeText(message));
    }

    IEnumerator TypeText(string message)
    {
        dialogText.text = "";

        foreach(char letter in message.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
