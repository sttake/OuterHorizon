using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    GameObject player;
    private float moveSpeed;
	private Vector3 targetPosition;
	private Vector3 moveVector;
	private Rigidbody myRigidbody;

	public void SetStatus(float speed) {
		moveSpeed = speed;
	}

	void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
		myRigidbody = GetComponent<Rigidbody>();
		targetPosition = player.transform.position;
		moveVector = (targetPosition - transform.position).normalized;
	}

	void FixedUpdate() {
		myRigidbody.velocity = moveSpeed * moveVector;
	}
}
