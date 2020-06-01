using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellManager : MonoBehaviour {
    public ParticleSystem explosionParticles;
    public int damage;
    public float maxLifeTime;
    public int reflectionNum; //1回反射して2回目の衝突で爆発
    [HideInInspector] public TankType ownerTankType;

    private Rigidbody myRigidbody;
    private int currentReflectionNum = 0;
    private Vector3 rotateAngle;
    private Vector3 normal;

    private void Start() {
        Destroy(gameObject, maxLifeTime);
        myRigidbody = GetComponent<Rigidbody>();
    }

    private void Update() {
        RaycastHit hit;
        if (Physics.Linecast(transform.position, transform.position + transform.forward * 3f, out hit)) {
            rotateAngle = Quaternion.FromToRotation(-transform.forward, hit.normal).eulerAngles;
            normal = hit.normal;
        }
    }

    private void OnTriggerEnter(Collider collider) {
        if (collider.GetComponent<Rigidbody>()) {
            if (collider.GetComponent<ShellManager>() == null) {
                //tankとの衝突
                HitWithTank(collider);
            } else {
                //shellとの衝突
                HitWithShell(collider);
            }

        } else {
            //壁との衝突
            if (currentReflectionNum < reflectionNum) {
                Reflection(collider);
            } else {
                Explosion(collider);
            }
        }
    }

    private void Reflection(Collider collider) {
        currentReflectionNum++;
        Vector3 reflect = Vector3.Reflect(transform.forward, normal);
        transform.LookAt(transform.position + reflect);
        myRigidbody.velocity = myRigidbody.velocity.magnitude * reflect;
    }

    private void HitWithTank(Collider collider) {
        bool isPlayer = collider.GetComponent<PlayerTankMovement>() != null;

        if (isPlayer && ownerTankType != TankType.Player) {
            Explosion(collider);
        } else if (!isPlayer && ownerTankType == TankType.Player) {
            Explosion(collider);
        }
    }

    private void HitWithShell(Collider collider) {
        bool isPlayer = collider.GetComponent<ShellManager>().ownerTankType == TankType.Player;

        if (isPlayer && ownerTankType != TankType.Player) {
            Explosion(collider);
        } else if (!isPlayer && ownerTankType == TankType.Player) {
            Explosion(collider);
        }
    }

    private void Explosion(Collider collider) {
        explosionParticles.transform.parent = null;

        explosionParticles.Play();

        Destroy(explosionParticles.gameObject, explosionParticles.main.duration);
        Destroy(gameObject);

        Rigidbody targetRigidbody = collider.GetComponent<Rigidbody>();
        if (!targetRigidbody) return;

        TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();
        if (targetRigidbody && targetHealth) {
            //タンクの場合HPを減らす
            targetHealth.TakeDamage(damage);
        }

    }
}
