using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MovementScript : MonoBehaviour
{
    private Transform _camera;
    [SerializeField] [Range(0,100)] private float _movementSpeedForEditor = 30f;
    public float OriginalMovementSpeed { get; set; }
    public float MovementSpeed { get; set; }
    private CharacterController _characterController;

	// Use this for initialization
	void Start () {
	    _characterController = GetComponent<CharacterController>();
	    _camera = Camera.main.transform;
	    OriginalMovementSpeed = _movementSpeedForEditor;
	    ResetMovementSpeed();
	}



	// Update is called once per frame
	void Update ()
	{
            GetInput();
	}

    private void GetInput()
    {
        MoveUnit(CrossPlatformInputManager.GetAxisRaw("Vertical"), CrossPlatformInputManager.GetAxisRaw("Horizontal"));
    }

    private void MoveUnit(float verticalValue, float horizontalValue)
    {
        _characterController.Move((_camera.up * verticalValue + _camera.right * horizontalValue).normalized * MovementSpeed * Time.deltaTime);
        if (!_characterController.isGrounded) FixYRange();
    }

    private void FixYRange()
    {
        //TODO: if there is time, fix the Y-value. This is hacky.
        transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
    }

    public void ResetMovementSpeed()
    {
        MovementSpeed = OriginalMovementSpeed;
    }
}
