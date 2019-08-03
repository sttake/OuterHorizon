using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject Player { get; private set; }

    void OnEnable() {
        Player = GameObject.FindGameObjectWithTag("Player");
        DamageTextController.Initialize();
    }

}
