using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Character : MonoBehaviour {

    [SerializeField] private float maxHP;
    [SerializeField] private float HP;
    [SerializeField] private Slider HPbar;


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

}
