using UnityEngine;
using System.Collections;
using Assets.Source;

public class GameController : MonoBehaviour
{

    public GameObject ObjectToRotate;
    public EllipseRing EllipseRing, EllipseRing2;
    //public RingElement RingElement;
    public float RingTurnVelocity = 1f;

    public float ellipseRotation = 0f;
    public float EllipseRotationSpeed = 1f;

	// Use this for initialization
	void Start ()
	{
	    SubscribeToEvents();
	}

    private void SubscribeToEvents()
    {
        SubscribeToInputEvents();
    }

    private void SubscribeToInputEvents()
    {
        InputEventSystem.LeftTap += InputEventSystem_LeftTap;
        InputEventSystem.RightTap += InputEventSystem_RightTap;
    }

    private void InputEventSystem_RightTap(object sender, System.EventArgs e)
    {
        if(ObjectToRotate != null)
            ObjectToRotate.transform.Rotate(new Vector3(0,0,-RingTurnVelocity * Time.deltaTime));
        ellipseRotation -= EllipseRotationSpeed*Time.deltaTime;
        EllipseRing.Rotate(-EllipseRotationSpeed * Time.deltaTime);
        EllipseRing2.Rotate(-EllipseRotationSpeed * Time.deltaTime);
        //RingElement.Arc += 60f*Time.deltaTime;

    }

    private void InputEventSystem_LeftTap(object sender, System.EventArgs e)
    {
        if (ObjectToRotate != null)
            ObjectToRotate.transform.Rotate(new Vector3(0, 0, RingTurnVelocity * Time.deltaTime));
        ellipseRotation += EllipseRotationSpeed * Time.deltaTime;
        EllipseRing.Rotate(EllipseRotationSpeed * Time.deltaTime);
        EllipseRing2.Rotate(EllipseRotationSpeed * Time.deltaTime);
        //RingElement.Arc -= 60f * Time.deltaTime;
    }

    // Update is called once per frame
    void Update () {
	
	}




}
