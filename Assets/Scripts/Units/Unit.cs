using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    protected CharacterController _characterController;
    protected WaveControllerScript _waveController;
    [SerializeField] private GameObject _bloodSpray;
    protected HealthBar HealthBar;
    public float HealthLevel { get { return (float)MaxHealth / 180; } }

	public float HealthRatio
	{
		get { return (float) CurrentHealth / MaxHealth; }
	}


	public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        HealthBar.SetColor(HealthRatio);
        if (CurrentHealth <= 0) Die();
    }

    //TODO: Flesh out
    public virtual void Die()
    {
        Destroy(gameObject);
        PlayDeathAnimation();
        if (_waveController != null)
        {
            _waveController.DefeatedEnemy();
        }
    }

    private void PlayDeathAnimation()
    {
        var deathAnimation = Instantiate(_bloodSpray, transform.position, transform.rotation);
        Destroy(deathAnimation, 1);
    }

    protected void InitializeHealthBar()
    {
        HealthBar = GetComponentInChildren<HealthBar>();
        HealthBar.SetColor(HealthRatio);
    }
}