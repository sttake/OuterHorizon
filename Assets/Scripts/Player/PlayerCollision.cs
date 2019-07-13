using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class PlayerCollision : MonoBehaviour {
	[SerializeField] private float moveSpeed;
	[SerializeField] private float moveTorque;
	[SerializeField] private float jumpPower;

	public Transform cameraObject;
	private Rigidbody myRigidbody;

	///<summary>
	///    ジャンプ入力が一度でもあったらtrue、着地したらfalse
	///</summary>
	private bool isJumpInput = false;

	///<summary>
	///    ジャンプ処理が開始されたらtrue、着地したらfalse
	///</summary>
	private bool isJumping = false;

	///<summary>
	///    連続で接地しているフレーム数
	///</summary>
	private int flameOnGround = 0;

	///<summary>
	///    連続で接地していないフレーム数
	///</summary>
	private int flameNotOnGround = 0;

	///<summary>
	///    何フレーム同じ状態が続いたら接地/非接地状態を切り替えるか
	///</summary>
	private const int flameGroundStateChange = 5;

	///<summary>
	///    現在のプレイヤーと地面の間の距離
	///</summary>
	[SerializeField] private float groundDistance = 0f;

	///<summary>
	///    groundDistanceがこの値以下の場合接地していると判定する
	///</summary>
	private const float groundDistanceLimit = 0.01f;

	///<summary>
	///    接地判定用レイの発射位置オフセット
	///</summary>
	private Vector3 raycastOffset  = new Vector3(0f, 0.005f - 1f, 0f);

	///<summary>
	///    接地判定用レイの最大長
	///</summary>
	private const float raycastLimitDistance = 100f;

	void Start() {
		myRigidbody = GetComponent<Rigidbody>();
	}

	///<summary>
	///    Update()
	///</summary>
	private void Update() {
		CheckGroundDistance(() => {
			isJumpInput = false;
			isJumping = false;
		});

		// 既にジャンプ入力が行われていたら、ジャンプ入力チェックを飛ばす
		if (isJumpInput || CheckJumpInput()) isJumpInput = true;
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
		Vector3 moveVector = (cameraForward.normalized * Input.GetAxis("MoveZ") + cameraRight.normalized * Input.GetAxis("MoveX")) * moveSpeed;
		Vector3 torque = myRigidbody.velocity;
		torque.y = 0;

		// if (moveForward != Vector3.zero) transform.rotation = Quaternion.LookRotation(moveForward);
		myRigidbody.AddForce(moveTorque * (moveVector - torque));
	}

	void Jump() {
		myRigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
	}

	///<summary>
	///    ジャンプ入力チェック
	///</summary>
	private bool CheckJumpInput() {
		if (Input.GetButton("Jump")) return true;
		return false;
	}

	///<summary>
	///    接地判定
	///</summary>
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
		} else
		if (flameGroundStateChange == flameNotOnGround && flameOnGround == 0) {
			if (takeOffAction != null) takeOffAction();
		}
	}
}

