using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CompleteProject
{
	public class LevelManager : MonoBehaviour
	{
		public static int level;
		public const int BASE_SCORE  = 100;
		
		
		Text text;                      // Reference to the Text component.
		
		
		void Awake ()
		{
			// Set up the reference.
			text = GetComponent <Text> ();
			
			// Reset the score.
			level = 1;
		}
		
		
		void Update ()
		{
			// Set the displayed text to be the word "Score" followed by the score value.
			text.text = "Level: " + level;
		}
	}
}