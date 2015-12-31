using UnityEngine;
using System.Collections;
using Assets.Source;

public class EllipseRing : MonoBehaviour
{

    public float RadiusA;
    public float RadiusB;
    public int ElementsCount;
    public RingElement ElementPrefab;

    private RingElement[] elementsOnRing;
    private float angleSize;
	// Use this for initialization
	void Start ()
	{
	    CalculateParameters();
	    CreateEllipseRing();
	}

    private void CreateEllipseRing()
    {
        elementsOnRing = new RingElement[ElementsCount];
        for (int i = 0; i < ElementsCount; i++)
        {
            elementsOnRing[i] = CreateRingElement(i * angleSize);
        }
        
    }

    private RingElement CreateRingElement(float angle)
    {
        Vector2 newPos = new Vector2(RadiusA * Mathf.Cos(angle * Mathf.Deg2Rad), RadiusB * Mathf.Sin(angle * Mathf.Deg2Rad));
        RingElement newElement = (RingElement) Instantiate(ElementPrefab, transform.position, Quaternion.identity);
        newElement.transform.parent = transform;
        newElement.transform.localPosition = newPos;
        newElement.transform.Rotate(new Vector3(0f,0f,angle));
        newElement.Radius = RadiusA/RadiusB;

        return newElement;

    }

    private void CalculateParameters()
    {
        angleSize = 360f/ElementsCount;
        //objectSize = ObjectToAttach.GetComponent<SpriteRenderer>().bounds.size.x;

        float a = RadiusA;
        float b = RadiusB;
        float lamda = (a - b) / (a + b);
        float lamda3 = 3 * lamda * lamda;
        float circumference = (a + b) * Mathf.PI * (1 + (lamda3 / (10 + Mathf.Sqrt(4 - lamda3))));

        //objectCount = (int)(circumference / objectSize);
    }
}
