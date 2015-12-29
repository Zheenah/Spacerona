using UnityEngine;
using System.Collections;
using Assets.Source;

public class InputHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    HandleMoueInput();
	    HandleKeyboardInput();
        HandleHardKeyInput();
    }

    private void HandleTouchInput()
    {
        throw new System.NotImplementedException();
    }

    void HandleHardKeyInput()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
                return;
            }
            if (Input.GetKeyDown(KeyCode.Menu))
            {
            }
        }
    }

    private void HandleMoueInput()
    {
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
        }
        else if (UnityEngine.Input.GetMouseButton(0))
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            Vector2 touchMousePos = Input.mousePosition;
            float mid = Screen.width / 2f;
            if (touchMousePos.x < mid)
                InputEventSystem.OnLeftTap(this);
            else
                InputEventSystem.OnRightTap(this);
        }
        else if (UnityEngine.Input.GetMouseButtonUp(0))
        {
        }
    }


    private void HandleKeyboardInput()
    {
        if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
        {
            InputEventSystem.OnLeftTap(this);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            InputEventSystem.OnRightTap(this);
        }
    }
}
