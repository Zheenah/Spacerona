using UnityEngine;
using System.Collections;
using Assets.Source;

public class EllipseRing : MonoBehaviour
{

    public float RadiusA;
    public float RadiusB;
    public int ElementsCount;
    public RingElement ElementPrefab;
    public Color[] Colors;



    private RingElement[] elementsOnRing;
    private float angleSize;
    private float arcSize;
    private float radiusSize;
    private float arcLength;
	// Use this for initialization



    public void Rotate(float angleInRad)
    {
        float toAdd = 2 * Mathf.PI / ElementsCount;
        foreach (var obj in elementsOnRing)
        {
            //Vector2 newPos = new Vector2(RadiusA * Mathf.Cos(angleInRad), RadiusB * Mathf.Sin(angleInRad));
            //obj.transform.localPosition = newPos;
            //float newAngle = (angleInRad * Mathf.Rad2Deg) - (arcSize / 2f);

            //obj.transform.localRotation = Quaternion.Euler(new Vector3(0f,0f,newAngle));
            //obj.transform.Rotate(new Vector3(0f, 0f, newAngle));
            UpdateRingElement(obj, angleInRad * Mathf.Rad2Deg);

            angleInRad += toAdd;
        }


       
    }




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
        
        
       
        RingElement newElement = (RingElement) Instantiate(ElementPrefab, transform.position, Quaternion.identity);
        newElement.transform.parent = transform;
        //element.transform.localPosition = newPos;
        int randColor = Random.Range(0, Colors.Length);
        newElement.Color = Colors[randColor];


        //UpdateRingElement(newElement, angle);
        UpdatePosition(newElement, angle);
        UpdateRotation(newElement, angle);
        UpdateArcAndRadius(newElement, angle);
        UpdateRotation(newElement, angle);



        return newElement;

    }



    private void UpdateRingElement( RingElement element, float angleInDegree)
    {
        UpdatePosition(element, angleInDegree);
        UpdateArcAndRadius(element, angleInDegree);
        UpdateRotation(element,angleInDegree ); 
              
       
    }

    private void UpdateArcAndRadius(RingElement element, float angleInDegree)
    {
        float radiusDistance = Vector3.Distance(element.transform.position, new Vector3());
        //element.Radius = radiusDistance;

        float newArcSize = ((360f * arcLength)) / (2f * Mathf.PI);
        newArcSize = newArcSize / (radiusDistance + element.Radius );

        //element.Arc = newArcSize * (RadiusB/RadiusA) * radiusDistance;
        element.Arc = newArcSize  * radiusDistance;

        //element.Arc = (newArcSize/radiusDistance)*3.5f;


    }

    private void UpdatePosition(RingElement element, float angleInDegree)
    {
        Vector2 ellipsePos = new Vector2(RadiusA * Mathf.Cos(angleInDegree * Mathf.Deg2Rad), RadiusB * Mathf.Sin(angleInDegree * Mathf.Deg2Rad));
        element.transform.localPosition = ellipsePos;

    }

    private void UpdateRotation(RingElement element, float angleInDegree)
    {
        float newOffsetAngle = angleInDegree - (element.Arc / 2f);
        element.transform.localRotation = Quaternion.Euler(0f, 0f, newOffsetAngle);
    }


    private void CalculateParameters()
    {
        angleSize = 360f/ElementsCount;
        //objectSize = ObjectToAttach.GetComponent<SpriteRenderer>().bounds.size.x;
        radiusSize = RadiusA/RadiusB;
        float a = RadiusA;
        float b = RadiusB;
        float lamda = (a - b) / (a + b);
        float lamda3 = 3 * lamda * lamda;
        float circumferenceEllipse = (a + b) * Mathf.PI * (1 + (lamda3 / (10 + Mathf.Sqrt(4 - lamda3))));

        //arcSize = (circumferenceEllipse*360f)/(2*Mathf.PI*(radiusSize))/ElementsCount;

        arcLength = circumferenceEllipse/ElementsCount;

        //arcSize *= 2;

        //objectCount = (int)(circumference / objectSize);
    }
}
