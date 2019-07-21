using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour {

    public Animator _animator;
    private Text _text;

    void OnEnable() {
        AnimatorClipInfo[] clipinfo = _animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipinfo[0].clip.length);
        _text = _animator.GetComponent<Text>();
    }

    public void setText(string text) {
        _text.text = text;
    }

}
