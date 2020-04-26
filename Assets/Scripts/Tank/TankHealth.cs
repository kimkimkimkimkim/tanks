using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour {
    public int maxHealth;
    public GameObject explosionPrefab;

    private ParticleSystem explosionParticles;
    [HideInInspector] public int currentHealth;
    private bool isDead;

    private void Awake() {
        explosionParticles = Instantiate(explosionPrefab).GetComponent<ParticleSystem>();

        explosionParticles.gameObject.SetActive(false);
    }

    private void OnEnable() {
        currentHealth = maxHealth;
        isDead = false;
    }

    public virtual void TakeDamage(int amount) {
        currentHealth -= amount;

        if (currentHealth <= 0 && !isDead) {
            OnDeath();
        }
    }

    private void OnDeath() {
        isDead = true;

        explosionParticles.transform.position = transform.position;
        explosionParticles.gameObject.SetActive(true);

        explosionParticles.Play();
        gameObject.SetActive(false);
    }
}
