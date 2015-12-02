using UnityEngine;

namespace CompleteProject
{
    public class ItemManager : MonoBehaviour
    {
        public PlayerHealth playerHP;
        public GameObject item;
        public float spawnItemTime = 15f;            // How long between each spawn.
        public Transform[] spawnItemPoints;         // An array of the spawn points this item can spawn from.

        void Start ()
        {
            // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
            InvokeRepeating("Spawn", spawnItemTime, spawnItemTime);
        }


        void Spawn ()
        {
            // Find a random index between zero and one less than the number of spawn points.
            int spawnPointIndex = Random.Range(0, spawnItemPoints.Length);

            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            Instantiate(item, spawnItemPoints[spawnPointIndex].position, spawnItemPoints[spawnPointIndex].rotation);
        }
    }
}