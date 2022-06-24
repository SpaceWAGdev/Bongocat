using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugUI : MonoBehaviour
{
    TMPro.TMP_Text text;
    private void Start() {
        text = GetComponent<TMPro.TMP_Text>();
    }
    private void Update() {
        text.text = Mathf.FloorToInt(1.0f / Time.deltaTime).ToString();
    }
}
