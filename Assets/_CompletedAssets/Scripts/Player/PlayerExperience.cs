using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace CompleteProject{
	public class PlayerExperience : MonoBehaviour {
		public Slider expSlider;
		private PlayerLevel playerLevel;
		// Use this for initialization
		void Start () {
			playerLevel = GetComponent<PlayerLevel> ();
		}
		
		// Update is called once per frame
		void Update () {

			expSlider.maxValue = playerLevel.nextLvlScore - playerLevel.lastLvlScore;
			expSlider.value = ScoreManager.score  - playerLevel.lastLvlScore;
		}
	}

}
