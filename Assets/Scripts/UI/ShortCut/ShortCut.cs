using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShortCut : MonoBehaviour {

    [SerializeField] private string itemName = null;
    [SerializeField] private Image imageObject = null;

    private Animator _animator;
    private Sprite itemSprite = null;

    void Start() {
        _animator = GetComponent<Animator>();
        if (itemName == null || !(itemSprite = Resources.Load<Sprite>("UI/" + itemName))) return;
        imageObject.sprite = itemSprite;
        imageObject.enabled = true;
    }

    public void SetActive(bool isActive) {
        _animator.SetBool("isActive", isActive);
    }

    public void Submit() {
        _animator.SetTrigger("Submit");
    }

}
