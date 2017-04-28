using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ShockwaveScript : MonoBehaviour {

    [SerializeField] private float _force = 750;
    [SerializeField] public float _upwardsModifier = 0;
    [SerializeField] public float _blastRadius = 20;
    [SerializeField] public ForceMode _forceMode = ForceMode.Impulse;

    // Update is called once per frame
	void Update () {
	    if (Input.GetMouseButtonDown(1))
	    {
	        CreateShockwave();
	    }
	}

    private void CreateShockwave()
    {
        AddExplosiveForce(_force, transform.position, _blastRadius, _upwardsModifier, _forceMode);

    }

//		Works like Unity's native AddExplosiveForce, but ignores objects not within direct view, i.e. allows items
//		or players to hide behind walls. However, this also means that if there are two objects in a direct line from
//		the source, the back object will not be affected.
//		todo: try to come up with a way to make it hit all items but still be stopped by cover
    public static void AddExplosiveForce(float force, Vector3 source, float radius, float upwardsModifier, ForceMode forceMode, string coverLayer = "cover")
    {
        var colliders = Physics.OverlapSphere(source,
            radius);
        foreach (var col in colliders)
        {
            var rb = col.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, source, radius, upwardsModifier, forceMode);
            }
        }
    }
}
