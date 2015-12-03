using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {

	public void loadScene(int level)
    {
        Application.LoadLevel(level);
    }
}
