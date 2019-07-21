using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class Enemy : MonoBehaviour {

    [SerializeField] private GameObject bulletObject = null;
    [SerializeField] private GameObject damageParticle = null;
    [SerializeField] private GameObject combatTarget = null;
    private Character _character;

    IEnumerator Start() {
        _character = GetComponent<Character>();
        while (true) {
            GenerateBullet();
            yield return new WaitForSeconds(2f);
        }
    }

    void GenerateBullet() {
        if(!combatTarget) return;
        Instantiate(bulletObject, transform.position, transform.rotation).GetComponent<Bullet>().SetStatus(14f);
    }

    void Update() {
        DeadCheck();
    }

    void OnTriggerEnter(Collider col) {
        if (col.tag == "PlayerAttack") {
            _character.Damage(3f);
            Instantiate(damageParticle, col.ClosestPointOnBounds(this.transform.position), Quaternion.LookRotation(Vector3.up));
            DamageTextController.CreateDamageText("3", transform);
        }
    }

    void DeadCheck() {
        if(_character.HP <= 0f) {
            _character.Dead();
        }
    }

}
