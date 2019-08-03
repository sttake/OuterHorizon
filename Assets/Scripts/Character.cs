using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Character : MonoBehaviour {

    [SerializeField] private float maxHP = 0;
    [SerializeField] private float defaultHP = 0;
    public float HP { get; private set; }
    [SerializeField] private Slider HPbar = null;


    void Start() {
        HP = defaultHP;
    }

    void Update() {
        if(HPbar != null) UpdateHPbar();
    }

    private void normalizeHP() {
        HP = normalizedHP(HP);
    }

    private float normalizedHP(float hp) {
        return (maxHP < hp) ? maxHP : (hp < 0) ? 0 : hp;
    }

    public void AddHP(float value) {
        HP = normalizedHP(HP + value);
    }

    public void Damage(float damage, UnityAction action = null) {
        AddHP(-damage);
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
