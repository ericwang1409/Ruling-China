using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

// This script used to manage dialogue ...
// 

public class DialogueManager : MonoBehaviour
{
    // These are all inputs can be changed in Unity editor...
    // presumably can be passed into functions ...
    // to display different texts

    // which dialogue box (always same)
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private ChoiceBox choiceBox;
    // what text to display (always different, hopefully fn arg?)
    [SerializeField] private Text dialogueText;
    // speed of text display (default 45)
    [SerializeField] int lettersPerSecond;

    bool isTyping;
    bool wantChoices = false;
    public bool IsShowing { get; private set; }

    // No idea what this does, I think it talks to interactable.cs ...
    // which is an 'interface'
    public static DialogueManager Instance {get; private set;}

    public event Action OnShowDialogue;
    public event Action OnCloseDialogue;
    int currentLine = 0;

    Dialogue dialogue;
    Action onDialogFinished;

    private void Awake() {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isTyping) {
            currentLine++;
            if (currentLine < dialogue.Lines.Count - 1) {
                StartCoroutine(TypeDialogue(dialogue.Lines[currentLine]));
            } else if (currentLine == dialogue.Lines.Count - 1) {
                StartCoroutine(TypeDialogue(dialogue.Lines[currentLine]));
                if (wantChoices) {
                    StartCoroutine(RunChoices());
                }
            } 
            else {
                currentLine = 0;
                IsShowing = false;
                dialogueBox.SetActive(false);
                onDialogFinished?.Invoke();
                OnCloseDialogue?.Invoke();
            }
        }
    }
    // Turns on dialogue box, then starts to show text
    public IEnumerator ShowDialogue(Dialogue dialogue, Action onFinished=null, bool localWantChoices=false, Action<int> onChoiceSelected=null) {
        yield return new WaitForEndOfFrame();
        OnShowDialogue?.Invoke();
        IsShowing = true;
        this.dialogue = dialogue;
        onDialogFinished = onFinished;
        wantChoices = localWantChoices;
        
        dialogueBox.SetActive(true);
        StartCoroutine(TypeDialogue(dialogue.Lines[0]));
        // if (choices != null && choices.Count > 1) {
        //     yield return choiceBox.ShowChoices(choices, onChoiceSelected);
        // }
    }

    public IEnumerator RunChoices() {
        yield return choiceBox.ShowChoices(new List<string>() { "Mozi", "Kongzi", "Han Feizi", "Zhuangzi", "Exit" }, (choice) => { 
            if (choice <= 3) 
                DayTracker.Instance.choose_counter[choice]++; 
        });
    }

    // Prints dialogue line by line instead of just displaying
    public IEnumerator TypeDialogue(string line) {
        isTyping = true;
        dialogueText.text = "";
        foreach (var letter in line.ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);

        }
        isTyping = false;
    }
}
