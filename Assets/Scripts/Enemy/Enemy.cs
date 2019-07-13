using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class Enemy : MonoBehaviour {

	[SerializeField] private GameObject bulletObject;

	IEnumerator Start() {
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

}
