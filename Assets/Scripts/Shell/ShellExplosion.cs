using UnityEngine;

public class ShellExplosion : MonoBehaviour {
    public LayerMask m_TankMask;
    public ParticleSystem m_ExplosionParticles;
    public AudioSource m_ExplosionAudio;
    public float m_MaxDamage = 100f;
    public float m_Damage = 20f;
    public float m_ExplosionForce = 1000f;
    public float m_MaxLifeTime = 2f;
    public float m_ExplosionRadius = 5f;
    public int m_ReflectionNum = 1; //1回反射して2回目の衝突で爆発

    private int currentReflectionNum = 0;

    private void Start() {
        Destroy(gameObject, m_MaxLifeTime);
    }


    private void OnTriggerEnter(Collider collider) {

        if (collider.GetComponent<Rigidbody>()) {
            //tankとの衝突
            Explosion(collider);
        } else {
            //壁との衝突
            if (currentReflectionNum < m_ReflectionNum) {
                Reflection(collider);
            } else {
                Explosion(collider);
            }
        }

        /*
        // Find all the tanks in an area around the shell and damage them.
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_TankMask);

        for (int i = 0; i < colliders.Length; i++) {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();
            if (!targetRigidbody)
                continue;

            TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();

            if (!targetHealth)
                continue;

            float damage = CalculateDamage(targetRigidbody.position);

            targetHealth.TakeDamage(damage);
        }

        m_ExplosionParticles.transform.parent = null;

        m_ExplosionParticles.Play();

        m_ExplosionAudio.Play();

        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);
        Destroy(gameObject);
        */
    }

    private void Reflection(Collider collider) {
        currentReflectionNum++;
    }

    private void Explosion(Collider collider) {
        m_ExplosionParticles.transform.parent = null;

        m_ExplosionParticles.Play();

        m_ExplosionAudio.Play();

        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);
        Destroy(gameObject);

        Rigidbody targetRigidbody = collider.GetComponent<Rigidbody>();
        TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();
        if (targetRigidbody && targetHealth) {
            //タンクの場合HPを減らす
            targetHealth.TakeDamage(m_Damage);
        }

    }


    private float CalculateDamage(Vector3 targetPosition) {
        // Calculate the amount of damage a target should take based on it's position.
        Vector3 explosionToTarget = targetPosition - transform.position;

        float explosionDistance = explosionToTarget.magnitude;

        float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;

        float damage = relativeDistance * m_MaxDamage;

        damage = Mathf.Max(0f, damage);

        return damage;
    }
}
