using UnityEngine;

public class FailSafe : MonoBehaviour {

    bool trigger = false;

    void Update() {
        if (trigger) {
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
