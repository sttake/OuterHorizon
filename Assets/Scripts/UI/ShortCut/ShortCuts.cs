using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortCuts : MonoBehaviour {

    [SerializeField] private int currentItem = 0;
    [SerializeField] private Animator[] shortcutItems = null;
    private int prevItem;
    private const int shortcutMax = 10;
    private float prevInput = 0;
    private float currentInput = 0;

    void Start() {
        shortcutItems[currentItem].SetBool("isActive", true);
    }

    void Update() {
        GetInput();
        ChangeActiveItem();
    }

    void GetInput() {
        currentInput = 0;
        var input = Input.GetAxisRaw("ShortCutLR");
        if (prevInput != input) currentInput = input;
        prevInput = input;
    }

    void ChangeActiveItem() {
        if(currentInput == 0) return;
        else if(currentInput == 1) currentItem++;
        else currentItem += shortcutMax - 1;
        currentItem %= shortcutMax;
        shortcutItems[prevItem].SetBool("isActive", false);
        shortcutItems[currentItem].SetBool("isActive", true);
        prevItem = currentItem;
    }

}
