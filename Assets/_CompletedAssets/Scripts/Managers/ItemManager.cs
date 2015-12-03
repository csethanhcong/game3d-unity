using UnityEngine;

namespace CompleteProject
{
    public class ItemManager : MonoBehaviour
    {
        public PlayerHealth playerHP;
        public GameObject item;
        public float spawnItemTime = 15f;            // How long between each spawn.
        public int maxHealthItemOnScreen = 5;
        public Transform[] spawnItemPoints;         // An array of the spawn points this item can spawn from.

        private int nbrOfHealthItemsOnScreen = 0;

        void Start ()
        {
            // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
            InvokeRepeating("Spawn", spawnItemTime, spawnItemTime);

            for (int i = 0; i < spawnItemPoints.Length; i++)
                spawnItemPoints[i].position = new Vector3(spawnItemPoints[i].localPosition.x, -1.23f, spawnItemPoints[i].localPosition.z);
        }


        void Spawn ()
        {
            // If the number of health item appeared on game is exceeded the limitation , stop spawning it
            if (nbrOfHealthItemsOnScreen >= maxHealthItemOnScreen) 
                return;

            // Find a random index between zero and one less than the number of spawn points.
            int spawnPointIndex = Random.Range(0, spawnItemPoints.Length);

            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            Instantiate(item, spawnItemPoints[spawnPointIndex].position, spawnItemPoints[spawnPointIndex].rotation);

            nbrOfHealthItemsOnScreen++;
        }
    }
}