using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer _rend;

    [SerializeField]
    private GameObject scaler;

    void Start()
    {
        _rend = GetComponentInChildren<SpriteRenderer>();
        scaler = gameObject;
    }

    public void UpdateBar(float ratio)
    {
        scaler.transform.localScale = new Vector3(ratio, scaler.transform.localScale.y, scaler.transform.localScale.z);
    }
    public void SetColor(float ratio)
    {
        _rend.color = Color.Lerp(Color.red, Color.green, ratio);
    }

    void Update()
    {
        scaler.transform.rotation = Camera.main.transform.rotation;
    }



}
