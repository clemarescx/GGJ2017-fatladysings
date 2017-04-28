using System;
using System.Threading;
using Medallion;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Enemy : Unit
{

	public double Distance
	{
		get { return Vector3.Distance(transform.position, TargetPosition); }
	}


	public int DamageOutput { get; set; }
    public int MovementSpeed { get; set; }

    public Transform Target { get; set; }
    public Vector3 TargetPosition { get { return Target.position; } }
    public Vector3 TargetDirection { get {return (TargetPosition - transform.position).normalized;} }


    [SerializeField]
    public double MaxAttackRange { get; set; }

    [SerializeField]
    public double MinAttackRange { get; set; }

    [SerializeField] private double _lowHealthLimit = 0.2;
    [SerializeField] private double _returnToAttackLimit = 0.5;

    [SerializeField]
    private float _healthRegenTimer;
    private float _healthRegenOffset = 0.0f;
    private const float MovementSpeedMultiplier = 1f;
    public float TotalMovementSpeed { get { return MovementSpeed * MovementSpeedMultiplier; } }

    private Rigidbody _rigidbody;


    [SerializeField]
    private Animator _movementAnim;
    [SerializeField]
    private Animator _AttackAnim;

    public bool IsWithinAttackRange { get { return Distance > MinAttackRange && Distance < MaxAttackRange; } }
    public bool IsTooClose { get { return Distance < MinAttackRange; } }
    public bool IsTooFarAway{ get { return Distance > MaxAttackRange; } }
    public double AvgAttackRange { get { return (MaxAttackRange - MinAttackRange) / 2 + MinAttackRange; } }
    public bool IsTooWeak { get { return HealthRatio < _lowHealthLimit; } }
    public bool HasRegainedHealth { get { return HealthRatio >= _returnToAttackLimit; } }
    public bool IsWithinArena;

    private PlaySoundFromList _audioPlayer;

    protected virtual void Start()
    {
        SetTarget();
        SetAttackRanges();
        RandomizeStats();
        AddStateManager();
        SetCharacterController();
        IsWithinArena = false;
        _waveController = FindObjectOfType<WaveControllerScript>();
        InitializeHealthBar();

        _movementAnim = GetComponentInChildren<Animator>();
        _AttackAnim = GetComponentsInChildren<Animator>()[1];
        _audioPlayer = GetComponent<PlaySoundFromList>();


    }

    private void SetCharacterController()
    {
	    _characterController = GetComponent<CharacterController>();
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void AddStateManager()
    {
        gameObject.AddComponent<StateManager>();
    }
    protected abstract void SetAttackRanges();

    protected void RandomizeStats()
    {
        var totalStats = 20;
        var statPoints = Rand.Next(1, totalStats - 2);
        MaxHealth = statPoints * 10;
        CurrentHealth = MaxHealth;
        totalStats -= statPoints;
        statPoints = Rand.Next(1, totalStats - 1);
        DamageOutput = statPoints * 2;
        totalStats -= statPoints;
        MovementSpeed = totalStats;
    }

    protected void SetTarget()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_healthRegenOffset > 0)
        {
            _healthRegenOffset -= Time.deltaTime;
        }
        else if (_healthRegenOffset < 0)
        {
            _healthRegenOffset = 0;
        }
        HealthBar.UpdateBar(HealthRatio);
    }

	public void Flee()
	{
	    LookAwayFromPlayer();
	    MoveUnit(-TargetDirection);
	    RegainHealth();
	}

    private void MoveUnit(Vector3 direction)
    {
            _movementAnim.SetTrigger("WalkTrigger");
        _rigidbody.MovePosition(transform.position + direction.normalized * TotalMovementSpeed * Time.deltaTime);
    }

    public void Approach()
	{
	    LookAtPlayer();
	    MoveUnit(TargetDirection);
	}

    public virtual void Attack()
    {
        _AttackAnim.SetTrigger("AttackTrigger");
        _movementAnim.SetTrigger("StopTrigger");
        LookAtPlayer();      
    }

    private void LookAwayFromPlayer()
    {
        gameObject.transform.LookAt(-(transform.position - TargetPosition).normalized);
    }

    protected void LookAtPlayer()
    {
	    gameObject.transform.LookAt(TargetPosition);
    }

    public void RegainHealth()
    {
        if (_healthRegenTimer > 0 && CurrentHealth < MaxHealth) return;
        CurrentHealth++;
        _healthRegenTimer = _healthRegenOffset;
    }

    public override void Die()
    {
        base.Die();
        _audioPlayer.PlayClip();
    }
}