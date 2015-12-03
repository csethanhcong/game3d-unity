using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    public class EnemyMovement : MonoBehaviour
    {
        //Transform player;               // Reference to the player's position.
        //PlayerHealth playerHealth;      // Reference to the player's health.
        EnemyHealth enemyHealth;        // Reference to this enemy's health.
        //NavMeshAgent nav;               // Reference to the nav mesh agent.

        public float patrolSpeed = 2f;                          // The nav mesh agent's speed when patrolling.
        public float chaseSpeed = 5f;                           // The nav mesh agent's speed when chasing.
        public float chaseWaitTime = 5f;                        // The amount of time to wait when the last sighting is reached.
        public float patrolWaitTime = 1f;                       // The amount of time to wait when the patrol way point is reached.
        public float bossRegenWaitTime = 5f;                    // The amount of time to wait when HP of boss regen
        public int bossRegenAmount = 10;                        // The amount of HP the boss gained after a timer tick event occurs
        public Transform[] patrolWayPoints;                     // An array of transforms for the patrol route.


        private EnemySight enemySight;                          // Reference to the EnemySight script.
        private NavMeshAgent nav;                               // Reference to the nav mesh agent.
        private Transform player;                               // Reference to the player's transform.
        private PlayerHealth playerHealth;                      // Reference to the PlayerHealth script.
        private LastPlayerSighting lastPlayerSighting;          // Reference to the last global sighting of the player.
        private float chaseTimer;                               // A timer for the chaseWaitTime.
        private float patrolTimer;                              // A timer for the patrolWaitTime.
        private float bossRegenTimer;                           // A timer to control boss regen HP 
        private int wayPointIndex;                              // A counter for the way point array.
        private bool onHpRegen;                                 // A flag to indicate whether boss is regen HP or not


        void Awake()
        {
            // Setting up the references.
            enemySight = GetComponent<EnemySight>();
            nav = GetComponent<NavMeshAgent>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerHealth = player.GetComponent<PlayerHealth>();
            enemyHealth = GetComponent<EnemyHealth>();
            lastPlayerSighting = GameObject.FindGameObjectWithTag("GameController").GetComponent<LastPlayerSighting>();
            wayPointIndex = 0;
            onHpRegen = false;
        }


        void Update()
        {
            if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
            {
                // If the player is in sight and is alive...
                //if (enemySight.playerInSight && playerHealth.currentHealth > 0f /*&& enemySight.CalculatePathLength(lastPlayerSighting.position) <= 0.8*/) 
                //    // ... shoot.
                //    Shooting();

                //// If the player has been sighted and isn't dead...
                //else 
                if (gameObject.tag == "Boss" && (enemyHealth.currentHealth <= 0.4 * enemyHealth.startingHealth || onHpRegen))
                    RunAway();
                else
                    if (enemySight.personalLastSighting != lastPlayerSighting.resetPosition && playerHealth.currentHealth > 0f)
                    // ... chase.
                    Chasing();

                // Otherwise...
                else
                    // ... patrol.
                    Patrolling();
            }
            else nav.enabled = false;
        }

        // Boss ran away from player when their HP is approximate 40% or lower and they will recover HP over the time
        void RunAway()
        {
            // ... increment the timer.
            bossRegenTimer += Time.deltaTime;


            // Regen now .. 
            onHpRegen = true;
            if (bossRegenTimer > bossRegenWaitTime)
            {
                // Hp is not full
                if (enemyHealth.currentHealth + bossRegenAmount <= enemyHealth.startingHealth)
                {
                    enemyHealth.currentHealth += bossRegenAmount;
                    onHpRegen = true;
                }
                // Hp is full and boss returned to patrol and chase player
                else
                {
                    enemyHealth.currentHealth = enemyHealth.startingHealth;
                    onHpRegen = false;
                }

                bossRegenTimer = 0;
            }

            // run away and pretend not seeing player at all
            if ( onHpRegen)
            {
                enemySight.playerInSight = false;
                enemySight.playerInAttackedRange = false;
                nav.destination = lastPlayerSighting.resetPosition;

                // patrolling with chase speed depend on current enemy's health. Hp is lower, patrolSpeed is higher
                Patrolling(chaseSpeed*enemyHealth.startingHealth/(enemyHealth.currentHealth + 100 - enemyHealth.currentHealth% 100));
            }
            
        }

        void Shooting()
        {
            // Stop the enemy where it is.
            nav.Stop();
        }


        void Chasing(float chaseSpeed = 0f)
        {
            // Create a vector from the enemy to the last sighting of the player.
            Vector3 sightingDeltaPos = enemySight.personalLastSighting - transform.position;

            // If the the last personal sighting of the player is not close...
            if (sightingDeltaPos.sqrMagnitude > 4f)
                // ... set the destination for the NavMeshAgent to the last personal sighting of the player.
                nav.destination = enemySight.personalLastSighting;

            // Set the appropriate speed for the NavMeshAgent.
            nav.speed = chaseSpeed == 0f ? this.chaseSpeed : chaseSpeed;
             
            // If near the last personal sighting...
            if (nav.remainingDistance < nav.stoppingDistance)
            {
                // ... increment the timer.
                chaseTimer += Time.deltaTime;

                // If the timer exceeds the wait time...
                if (chaseTimer >= chaseWaitTime)
                {
                    // ... reset last global sighting, the last personal sighting and the timer.
                    lastPlayerSighting.position = lastPlayerSighting.resetPosition;
                    enemySight.personalLastSighting = lastPlayerSighting.resetPosition;
                    chaseTimer = 0f;
                }
            }
            else
                // If not near the last sighting personal sighting of the player, reset the timer.
                chaseTimer = 0f;
        }


        void Patrolling(float patrolSpeed = 0)
        {
            // Set an appropriate speed for the NavMeshAgent.
            nav.speed = patrolSpeed == 0f ? this.patrolSpeed : patrolSpeed;

            // If near the next waypoint or there is no destination...
            if (nav.destination == lastPlayerSighting.resetPosition || nav.remainingDistance < nav.stoppingDistance)
            {
                // ... increment the timer.
                patrolTimer += Time.deltaTime;

                // If the timer exceeds the wait time...
                if (patrolTimer >= patrolWaitTime)
                {
                    // ... increment the wayPointIndex.
                    if (wayPointIndex == patrolWayPoints.Length - 1)
                        wayPointIndex = 0;
                    else
                        wayPointIndex++;

                    // Reset the timer.
                    patrolTimer = 0;
                }
            }
            else
                // If not near a destination, reset the timer.
                patrolTimer = 0;

            // Set the destination to the patrolWayPoint.
            nav.destination = patrolWayPoints[wayPointIndex].position;
        }


        //void Awake ()
        //{
        //    // Set up the references.
        //    player = GameObject.FindGameObjectWithTag ("Player").transform;
        //    playerHealth = player.GetComponent <PlayerHealth> ();
        //    enemyHealth = GetComponent <EnemyHealth> ();
        //    nav = GetComponent <NavMeshAgent> ();
        //}


        //void Update ()
        //{
        //    // If the enemy and the player have health left...
        //    if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        //    {
        //        // ... set the destination of the nav mesh agent to the player.
        //        nav.SetDestination (player.position);
        //    }
        //    // Otherwise...
        //    else
        //    {
        //        // ... disable the nav mesh agent.
        //        nav.enabled = false;
        //    }
        //}
    }
}