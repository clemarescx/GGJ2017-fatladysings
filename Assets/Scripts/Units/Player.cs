using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Unit
{
	// Use this for initialization
	void Start ()
	{
	    MaxHealth = 100;
	    CurrentHealth = MaxHealth;
	    InitializeHealthBar();
	}
	
	// Update is called once per frame
    void Update()
    {
        HealthBar.UpdateBar(HealthRatio);
    }

    public override void Die()
    {
         SceneManager.LoadScene("WhiteBoxMainMenu");
    }
}
