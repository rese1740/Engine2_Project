using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarUI : MonoBehaviour
{
    public Transform target;     
    public Vector3 offset = new Vector3(0, 2f, 0); 
    private RectTransform rectTransform;

    public Transform player;          

    public float minScale = 0.5f;
    public float maxScale = 1.0f;
    public float minDistance = 5f;
    public float maxDistance = 20f;
    private Slider slider;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        slider = GetComponent<Slider>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (target == null)
        {
            return;
        }
        float distance = Vector3.Distance(player.position, target.position);

        float t = Mathf.InverseLerp(minDistance, maxDistance, distance);

        float scale = Mathf.Lerp(maxScale, minScale, t);

        rectTransform.localScale = Vector3.one * scale;


        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position + offset);

        if (screenPos.z > 0)
        {
            rectTransform.position = screenPos;
        }
    }

    public void SetHealth(int value)
    {
        slider.value = value;
    }

    public void SetMaxHealth(int max)
    {
        slider.maxValue = max;
    }
}
