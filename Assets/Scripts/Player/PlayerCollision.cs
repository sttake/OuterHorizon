using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Character))]
public class PlayerCollision : MonoBehaviour {

	#region プロパティ
	[SerializeField] private float moveSpeed;
	[SerializeField] private float moveDashSpeed;
	[SerializeField] private float moveTorque;
	[SerializeField] private float jumpPower;

	[SerializeField] private Transform cameraObject;
	private Rigidbody myRigidbody;
    private Character character;

    // ジャンプ入力が一度でもあったらtrue、着地したらfalse
    private bool isJumpInput = false;

	// ジャンプ処理が開始されたらtrue、着地したらfalse
	private bool isJumping = false;

	// 接地していたらtrue、空中にいたらfalse
	private bool isGround = true;

	// 連続で接地しているフレーム数
	private int flameOnGround = 0;

	// 連続で接地していないフレーム数
	private int flameNotOnGround = 0;

	// 何フレーム同じ状態が続いたら接地/非接地状態を切り替えるか
	private const int flameGroundStateChange = 5;

	// 現在のプレイヤーと地面の間の距離
	[SerializeField] private float groundDistance = 0f;

	// groundDistanceがこの値以下の場合接地していると判定する
	private const float groundDistanceLimit = 0.02f;

	// 接地判定用レイの発射位置オフセット
	private Vector3 raycastOffset  = new Vector3(0f, 0.015f - 1f, 0f);

	// 接地判定用レイの最大長
	private const float raycastLimitDistance = 100f;

	#endregion

	void Start() {
		myRigidbody = GetComponent<Rigidbody>();
        character = GetComponent<Character>();
    }

	private void Update() {
		CheckGroundDistance(() => {
			isJumpInput = false;
			isJumping = false;
		});
		if (isJumpInput || CheckJumpInput()) isJumpInput = true;

        Attack();
    }

	void FixedUpdate() {
		Run();
		if (isJumpInput && !isJumping) {
			isJumping = true;
			Jump();
		}
	}

	void Run() {
		Vector3 cameraForward = Camera.main.transform.forward;
		cameraForward.y = 0;
		Vector3 cameraRight = Camera.main.transform.right;
		cameraRight.y = 0;
		Vector3 moveVector = (cameraForward.normalized * Input.GetAxis("MoveZ") + cameraRight.normalized * Input.GetAxis("MoveX"))
			* ((Input.GetAxisRaw("Dash") == 1) ? moveDashSpeed : moveSpeed);
		Vector3 torque = myRigidbody.velocity;
		torque.y = 0;

		if (!isGround) {
			moveVector *= 0.2f;
			torque *= 0.2f;
		}
		myRigidbody.AddForce(moveTorque * (moveVector - torque));
	}

	void Jump() {
		myRigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
	}

	/* ============ ジャンプ入力チェック ============ */
	private bool CheckJumpInput() {
		if (Input.GetButton("Jump")) return true;
		return false;
	}

	/* ============ 接地判定 ============ */
	private void CheckGroundDistance(UnityAction landingAction = null, UnityAction takeOffAction = null) {
		
		RaycastHit hit;
		var layerMask = LayerMask.GetMask("Ground");
		
		// 地面にGroundレイヤを付与しておき、真下にレイを生成して地面を判定する
		var isGroundHit = Physics.Raycast(
				transform.position + raycastOffset,
				transform.TransformDirection(Vector3.down),
				out hit,
				raycastLimitDistance,
				layerMask
			);

		if (isGroundHit) {
			groundDistance = hit.distance;
		} else {
			// ヒットしなかった場合はキャラの下方に地面が存在しないものとして扱う
			groundDistance = float.MaxValue;
		}

		// 接地カウント実行
		if (groundDistance < groundDistanceLimit) {
			// 接地しているとき
			if (flameOnGround <= flameGroundStateChange) {
				flameOnGround++;
				flameNotOnGround = 0;
			}
		} else {
			// 接地していないとき
			if (flameNotOnGround <= flameGroundStateChange) {
				flameOnGround = 0;
				flameNotOnGround++;
			}
		}

		// 接地後またはジャンプ後、特定フレーム分状態の変化が無ければ、
		// 状態が安定したものとして接地処理またはジャンプ処理を行う
		if (flameGroundStateChange == flameOnGround && flameNotOnGround == 0) {
			if (landingAction != null) landingAction();
			isGround = true;
		} else if (flameGroundStateChange == flameNotOnGround && flameOnGround == 0) {
			if (takeOffAction != null) takeOffAction();
			isGround = false;
		}
	}

	void OnTriggerEnter(Collider col) {
        character.Damage(5f);
    }

	void Attack() {
		if(Input.GetAxisRaw("Attack") == 1) {
			
		}
	}

}

