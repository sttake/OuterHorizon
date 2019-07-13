using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Character : MonoBehaviour {

    [SerializeField] private float HP;
    [SerializeField] private Slider HPbar;


    void Start() {
		
	}

	void Update() {
		
	}

    void Damage(float damage, UnityAction action = null) {
        HP -= damage;
    }

}
