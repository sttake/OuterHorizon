using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class Enemy : MonoBehaviour {

	[SerializeField] private GameObject bulletObject;
    private Character _character;

    IEnumerator Start() {
        _character = GetComponent<Character>();
        while (true) {
			GenerateBullet();
			yield return new WaitForSeconds(2f);
		}
	}

	void GenerateBullet() {
		Instantiate(bulletObject, transform.position, transform.rotation).GetComponent<Bullet>().SetStatus(14f);
	}

	void Update() {
		
	}

	void OnTriggerEnter(Collider col) {
		if(col.tag == "PlayerAttack") _character.Damage(3f);
    }

}
