using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Character : MonoBehaviour {

    [SerializeField] private float maxHP = 0;
    [SerializeField] public float HP = 0;
    [SerializeField] private Slider HPbar = null;


    void Start() {
        
    }

    void Update() {
        if(HPbar != null) UpdateHPbar();
    }

    public void Damage(float damage, UnityAction action = null) {
        HP -= damage;
    }

    void UpdateHPbar() {
        HPbar.maxValue = maxHP;
        HPbar.SetValueWithoutNotify(HP);
    }

    public void Dead(UnityAction deadAction = null) {
        UpdateHPbar();
        if(deadAction != null) deadAction();
        Destroy(gameObject);
    }

}
