using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    [SerializeField] int lettersPerSecond;
    public static DialogManager Instance { get; private set; }
    public event Action OnShowDialog;
    public event Action OnCloseDialog;
    private void Awake()
    {
        Instance = this;
    }
    Dialog dialog;
    private int currentLine = 0;
    
    bool isTyping;
    public IEnumerator ShowDialog(Dialog dialog)
    {
        yield return new WaitForEndOfFrame();
        OnShowDialog?.Invoke();
        this.dialog = dialog;
        dialogBox.SetActive(true);  
        currentLine = 0;
        StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
    }
    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.X) && !isTyping)
        {
            ++currentLine;
            if (currentLine < dialog.Lines.Count)
            {
                dialogText.text = "";
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
            }
            else
            {
                dialogBox.SetActive(false);
                currentLine = 0;
                OnCloseDialog?.Invoke();
            }
        }
    }
    public IEnumerator TypeDialog(string line)
    {
        isTyping = true;
        dialogText.text = "";
        foreach (var letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f/ lettersPerSecond);
        }
        isTyping = false;
    }
}
