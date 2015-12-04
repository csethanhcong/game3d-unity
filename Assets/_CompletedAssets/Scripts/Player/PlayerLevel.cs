using UnityEngine;
using System.Collections;

namespace CompleteProject{
	public class PlayerLevel : MonoBehaviour {
		public int level;
		public int kill;
		public int nextLvlScore;
		public int lastLvlScore;

		
		void Awake() {
			level = 1;
		}
		// Use this for initialization
		void Start () {
			level = 1;
			//kill = 0;
			lastLvlScore = 0;
			nextLvlScore = LevelManager.BASE_SCORE;
		}
		
		// Update is called once per frame
		void Update () {
			if (ScoreManager.score >= nextLvlScore) {
				level++;
                //TODO: Play sound Player Level Up, don't know how

                GetComponent<PlayerHealth>().currentHealth = GetComponent<PlayerHealth>().startingHealth;
               // GetComponentInChildren<PlayerShooting>().damagePerShot += 2;

                LevelManager.level = level;
				//ScoreManager.score = level;
				lastLvlScore = nextLvlScore;
				nextLvlScore += level * LevelManager.BASE_SCORE;
			}
		}
		
		/*public void addKill() {
			kill++;
		}*/
	}

}
