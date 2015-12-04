using UnityEngine;

namespace CompleteProject
{
    public class EnemyManager : MonoBehaviour
    {
        public PlayerHealth playerHealth;       // Reference to the player's heatlh.
        public GameObject enemy;                // The enemy prefab to be spawned.
        public float spawnTime = 100f;            // How long between each spawn.
        public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
		private int level;


        void Start ()
        {
            // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
			level = 1;
            InvokeRepeating ("Spawn", 0f, spawnTime);
        }


        void Spawn ()
        {
            // If the player has no health left...
            if(playerHealth.currentHealth <= 0f)
            {
                // ... exit the function.
                return;
            }

            // Find a random index between zero and one less than the number of spawn points.
            int spawnPointIndex = Random.Range (0, spawnPoints.Length);

            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }

		void Update() {
			if (LevelManager.level > level && spawnTime > 5f) {
				if (spawnTime > 20f)
					spawnTime -= 5f;
				else spawnTime -= 2f;

				if (spawnTime < 5f) 
					spawnTime = 5f;
				//spawnTime -= 5f;
				level = LevelManager.level;

				CancelInvoke("Spawn");
				InvokeRepeating ("Spawn", spawnTime, spawnTime);

			}
		}
    }
}