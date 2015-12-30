using UnityEngine;
using System.Collections;
using Assets.Source;

public class GameController : MonoBehaviour
{

    public Ring Ring;
    public RingElement RingElement;
    public float RingTurnVelocity = 1f;

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
        
        Ring.transform.Rotate(new Vector3(0,0,-RingTurnVelocity * Time.deltaTime));
        RingElement.Arc += 60f*Time.deltaTime;
    }

    private void InputEventSystem_LeftTap(object sender, System.EventArgs e)
    {
        Ring.transform.Rotate(new Vector3(0, 0, RingTurnVelocity * Time.deltaTime));
        RingElement.Arc -= 60f * Time.deltaTime;
    }

    // Update is called once per frame
    void Update () {
	
	}




}
