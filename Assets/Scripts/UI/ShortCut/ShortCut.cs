using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class ShortCut : MonoBehaviour {

    [SerializeField] private ActionBase action;
    [SerializeField] private Image imageObject = null;

    private Character player;
    private Animator _animator;
    private Sprite itemSprite = null;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        _animator = GetComponent<Animator>();
        if (!(itemSprite = Resources.Load<Sprite>("UI/" + action.GetType().Name))) return;
        imageObject.sprite = itemSprite;
        imageObject.enabled = true;
    }

    public void SetActive(bool isActive) {
        _animator.SetBool("isActive", isActive);
    }

    public void Submit() {
        _animator.SetTrigger("Submit");
        action.Action(player);
    }

}
