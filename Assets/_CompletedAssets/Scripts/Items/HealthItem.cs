using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace CompleteProject
{
    public class HealthItem : MonoBehaviour
    {
        public Slider healthSlider;                 // Reference to the UI's health bar.
        GameObject player;                          // Reference to the player GameObject.
        PlayerHealth playerHealth;                  // Reference to the player's health.
        float timer;                                // Timer for counting up to the next attack.
        int increaseHp = 100;                       // Full regen

        void Awake()
        {
            // Setting up the references.
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<PlayerHealth>();
        }


        void OnControllerColliderHit(ControllerColliderHit other)
        {
            // If the entering collider is the player...
            if (other.gameObject == player)
            {
                playerHealth.currentHealth = increaseHp;

                // Set the health bar's value to the current health.
                healthSlider.value = increaseHp;

                Destroy(gameObject, 1f);
            }
        }
    }
}
