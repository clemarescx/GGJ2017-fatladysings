using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CryConeScript : MonoBehaviour
{
    [SerializeField] private GameObject _cryCone;
    [SerializeField] private GameObject _cryConeSpawn;
    [SerializeField] private Material _screamMaterial;
    [SerializeField] private Material _screamRenderMaterial;
    [SerializeField] private double _coneMinZ = 0.25;
    [SerializeField] private double _coneMaxZ = 10;
    [SerializeField] private double _coneMinX = 2;
    [SerializeField] private double _coneMaxX = 20;
    [SerializeField] private int _coneChargeDelay = 50;
    [SerializeField] private float _damageDuration = 0.5f;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private MovementScript _movementScript;
    public float MovementSpeedOffsetStep { get { return _movementScript.OriginalMovementSpeed / 100; } }
    
    private int _chargeCounter = 0;
    private int _powerCounter = 0;

    private void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        _movementScript = gameObject.GetComponent<MovementScript>();
    }

    // Update is called once per frame
	void Update ()
	{
        if (Input.GetButton("Scream") && !Pause.Paused)
        {
//            print("Starting Charge...");
            _chargeCounter++;

            if (_chargeCounter >= _coneChargeDelay)
            {
                if (++_powerCounter >= 100) _powerCounter = 100;

                RenderCryCone(_powerCounter);
                _movementScript.MovementSpeed =_movementScript.OriginalMovementSpeed - _powerCounter * MovementSpeedOffsetStep;

//                Debug.Log("Charging weapon: " + _powerCounter);
            }
        }

        if (Input.GetButtonUp("Scream"))
        {
//            print("Released trigger button!");

            if (_chargeCounter >= _coneChargeDelay) {

//                Debug.Log("This was a charged attack");
                FireCryCone(_powerCounter);
            }
            _chargeCounter = 0;
            _powerCounter = 0;
            _movementScript.ResetMovementSpeed();
        }
    }

    private void FireCryCone(int power) {
//        print("Firing with power: " + power);

        //Instantiate CryCone
        GameObject instantiatedProjectile = Instantiate(_cryCone, _cryConeSpawn.transform.position, _cryConeSpawn.transform.rotation);

        instantiatedProjectile.GetComponent<ConeDamageScript>().Source = _cryConeSpawn.transform.position;

        //Set shader
        instantiatedProjectile.GetComponent<Renderer>().material = _screamMaterial;

        //Expand Cone
        instantiatedProjectile.transform.localScale = new Vector3(GetConeScaleX(power), 0f, GetConeScaleZ(power));

        //Give cone damage power
        instantiatedProjectile.GetComponent<ConeDamageScript>().Damage = power;

        //Deal Damage to enemies
        StartCoroutine(DamageEnemies(instantiatedProjectile));

        //Destroy cone
        StartCoroutine(WaitThenDestroy(instantiatedProjectile, _damageDuration));

        //Play audio
        PlayAudio();
    }

    private void PlayAudio()
    {
        //        TODO: fix!
        _audioSource.Play();
    }

    private void RenderCryCone(int power)
    {
        //Instantiate CryCone
        GameObject instantiatedProjectile = Instantiate(_cryCone, _cryConeSpawn.transform.position, _cryConeSpawn.transform.rotation);

        //Set shader
        instantiatedProjectile.GetComponent<Renderer>().material = _screamRenderMaterial;

        //Expand Cone
        instantiatedProjectile.transform.localScale = new Vector3(GetConeScaleX(power), 0f, GetConeScaleZ(power));

        //Destroy cone
        StartCoroutine(WaitThenDestroy(instantiatedProjectile, 0.05f));
    }

    private float GetConeScaleZ(int power) 
    {
        return (float) (_coneMaxZ + _coneMinZ) / 100 * (101-power);
    }

    private float GetConeScaleX(int power)
    {
        return (float) (_coneMaxX + _coneMinX) / 100 * power;
    }

    private IEnumerator WaitThenDestroy(GameObject destroyee, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(destroyee);
    }

    private IEnumerator DamageEnemies(GameObject cone)
    {
//        print("Enabling MeshCollider!");
        cone.GetComponent<MeshCollider>().enabled = true;
        yield return new WaitForSeconds(_damageDuration);
        cone.GetComponent<MeshCollider>().enabled = false;
//        print("Disabling MeshCollider");
    }
}