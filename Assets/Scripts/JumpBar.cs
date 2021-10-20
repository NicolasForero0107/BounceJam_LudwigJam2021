using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpBar : MonoBehaviour
{
    public bool doTestLerping = false;
    [Header("References")]
    [SerializeField] Slider slider;

    [HideInInspector]
    public float sliderValue { get; private set; }


    float _x = 0.0f;
    bool _reverse = false;
    private void Update()
    {
        if (doTestLerping)
        {
            if (!_reverse)
                _x += Time.deltaTime;
            else
                _x -= Time.deltaTime;
            slider.value = Mathf.Lerp(-1.0f, 1.0f, _x);

            if (_x >= 1.0f)
            {
                _x = 1.0f;
                _reverse = true;
            }
            else if (_x <= 0.0f)
            {
                _x = 0.0f;
                _reverse = false;
            }
        }

        sliderValue = slider.value;
    }

}
