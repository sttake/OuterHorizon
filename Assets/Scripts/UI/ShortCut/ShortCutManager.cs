using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortCutManager : MonoBehaviour {

    [SerializeField] private int currentCursor = 0;
    [SerializeField] private ShortCut[] shortcutIcons = null;
    private int prevCursor;
    private const int shortcutMax = 10;
    private float prevInput = 0;
    private float currentInput = 0;

    void Start() {
        shortcutIcons[currentCursor].SetActive(true);
    }

    void Update() {
        GetInput();
        ChangeActiveItem();
        SubmitItem();
    }

    void GetInput() {
        currentInput = 0;
        var input = Input.GetAxisRaw("ShortCutLR");
        if (prevInput != input) currentInput = input;
        prevInput = input;
    }

    void ChangeActiveItem() {
        if(currentInput == 0) return;
        else if(currentInput == 1) currentCursor++;
        else currentCursor += shortcutMax - 1;
        currentCursor %= shortcutMax;
        shortcutIcons[prevCursor].SetActive(false);
        shortcutIcons[currentCursor].SetActive(true);
        prevCursor = currentCursor;
    }

    void SubmitItem() {
        if(Input.GetButtonDown("ShortcutSubmit")) {
            shortcutIcons[currentCursor].Submit();
        }
    }

}
