using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace CompleteProject
{
    public class HealthItem : MonoBehaviour
    {
        //public Slider healthSlider;                 // Reference to the UI's health bar.
        //GameObject player;                          // Reference to the player GameObject.
        PlayerHealth playerHealth;                  // Reference to the player's health.
        //float timer;                                // Timer for counting up to the next attack.
        public int HpRegenAmount = 100;                       // Full regen

        void Awake()
        {
            // Setting up the references.
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        }


        void OnTriggerStay(Collider other)
        {
            if( other.gameObject.tag == "Player" )
            {
                playerHealth.RecoverHp(HpRegenAmount);

                Debug.Log(playerHealth.currentHealth);

                // Destroy health item
                Destroy(gameObject);
            }
        }
    }
}
