using UnityEngine;

public class FaceMouseScript : MonoBehaviour {

	// Update is called once per frame
	void Update ()
	{
	    var mousePosition = Input.mousePosition;
	    mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y,
	        Camera.main.transform.position.z - transform.position.z));
	    var mousePositionInPlane = new Vector3(mousePosition.x, transform.position.y, mousePosition.z);
	    transform.LookAt(mousePositionInPlane);
	}
}
