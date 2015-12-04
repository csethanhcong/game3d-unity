using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    public class BounceBulletItem : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }


        void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                other.GetComponentInChildren<PlayerShooting>().BounceTimer = 0;
                Destroy(gameObject, 1);
            }
        }
    }
}
