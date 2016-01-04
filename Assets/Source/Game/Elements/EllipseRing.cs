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
    private float ringElementRadius;

    private float rotation;

    public bool CosinusRotation;
    public bool CosinusCreation;



    public void RotateTo(float angleInRad)
    {

        float toAdd = 2 * Mathf.PI / ElementsCount;
        float currentRadian = 0;
        foreach (var obj in elementsOnRing)
        {
            //Vector2 newPos = new Vector2(RadiusA * Mathf.Cos(angleInRad), RadiusB * Mathf.Sin(angleInRad));
            //obj.transform.localPosition = newPos;
            //float newAngle = (angleInRad * Mathf.Rad2Deg) - (arcSize / 2f);

            //obj.transform.localRotation = Quaternion.Euler(new Vector3(0f,0f,newAngle));
            //obj.transform.Rotate(new Vector3(0f, 0f, newAngle));
            float currentDegree = obj.transform.localEulerAngles.z + 22.5f;
            float speedModifier = Mathf.Abs(Mathf.Sin(currentDegree*Mathf.Deg2Rad));
            //Debug.Log("speedModifier: "+ speedModifier);
            UpdateRingElement(obj, (angleInRad ) * Mathf.Rad2Deg);

            angleInRad += toAdd;
        }
       
    }

    public void Rotate(float rotationValue)
    {
        bool useCosinusMod = CosinusRotation;

        float toAdd = 2 * Mathf.PI / ElementsCount;
        float currentRadian = 0;
        foreach (var obj in elementsOnRing)
        {
            //Vector2 newPos = new Vector2(RadiusA * Mathf.Cos(angleInRad), RadiusB * Mathf.Sin(angleInRad));
            //obj.transform.localPosition = newPos;
            //float newAngle = (angleInRad * Mathf.Rad2Deg) - (arcSize / 2f);

            //obj.transform.localRotation = Quaternion.Euler(new Vector3(0f,0f,newAngle));
            //obj.transform.Rotate(new Vector3(0f, 0f, newAngle));
            float currentDegree = obj.transform.localEulerAngles.z + (obj.Arc / 2f);
            
            float speedModifier = Mathf.Abs(Mathf.Cos(currentDegree*Mathf.Deg2Rad));


            //Vector2 radPos = new Vector2(RadiusA * Mathf.Cos(currentDegree), RadiusB * Mathf.Sin(currentDegree));
            //speedModifier = radPos.magnitude;
            // speed modfied by cosinus
            speedModifier += 3f;

            if (useCosinusMod)
                speedModifier = 1f;

            float degreeToAdd = rotationValue*speedModifier;
            //Debug.Log("speedp Plus: "+ degreeToAdd);
            float newDegree = (currentDegree + degreeToAdd);

            //Debug.Log("speedModifier: " + speedModifier + ". Curr, new: " + currentDegree + " : " + newDegree);
            UpdateRingElement(obj, newDegree);

        }
       
    }

 

    void Start ()
	{
	    CalculateParameters();
	    CreateEllipseRing();
	    CapsuleCollider coll = GetComponent<CapsuleCollider>();
	    ringElementRadius = ElementPrefab.StartRadius;

        Debug.Log(ringElementRadius);
	    coll.radius = RadiusB + ringElementRadius;
	    coll.height = 2 * (RadiusA + ringElementRadius);
	}

    private void CreateEllipseRing()
    {
        bool useCosinusMod = CosinusCreation;

        elementsOnRing = new RingElement[ElementsCount];
        float angleToSet = 0f;
        float angleSize = 360f/ElementsCount;


        float sum = 0;
        for (int i = 0; i < ElementsCount; i++)
        {
            float cos = Mathf.Cos(((float)i / ElementsCount) * Mathf.PI * 2f);
            sum += Mathf.Abs(cos) + 0.9f;
        }
        float a = 360f/sum;
        
        //Debug.Log("sum, a: " + sum + " , " +a);

        for (int i = 0; i < ElementsCount; i++)
        {
            // old:
            //elementsOnRing[i] = CreateRingElement(i * angleSize);
            elementsOnRing[i] = CreateRingElement();
            float arcLength = (elementsOnRing[i].Arc*Mathf.PI*2f*elementsOnRing[i].Radius)/360f;
            float realRadius = elementsOnRing[i].Radius + new Vector2(RadiusA * Mathf.Cos(angleToSet * Mathf.Deg2Rad), RadiusB * Mathf.Sin(angleToSet * Mathf.Deg2Rad)).magnitude;
            //float degreeToAdd = (360f*arcLength)/(2*Mathf.PI* elementsOnRing[i].Radius);
            float degreeToAdd = 0f;
            // based on Cos
            if (useCosinusMod)
            {
                degreeToAdd = Mathf.Cos(((float)i / ElementsCount) * Mathf.PI * 2);
                degreeToAdd = (Mathf.Abs(degreeToAdd) + 0.9f) * a;
                float modAngleToSet = angleToSet - (elementsOnRing[i].Arc / 2f);
                ConvertToValidRange(ref modAngleToSet);
                ConfigureRingElement(elementsOnRing[i], modAngleToSet);
                angleToSet += degreeToAdd;
            }
                
            // equal distance to next
            else
                ConfigureRingElement(elementsOnRing[i], i * angleSize);

            

        }
        
    }



    private RingElement CreateRingElement()
    {
        
             
        RingElement newElement = (RingElement) Instantiate(ElementPrefab, transform.position, Quaternion.identity);
        newElement.transform.parent = transform;
        //element.transform.localPosition = newPos;
        int randColor = Random.Range(0, Colors.Length);
        newElement.Color = Colors[randColor];
        SetArcAndRadius(newElement);

        return newElement;

    }

    private void ConfigureRingElement(RingElement element, float angle)
    {
        UpdatePosition(element, angle);        
        UpdateRotation(element, angle);
        element.UpdateCollider();
    }



    private void UpdateRingElement( RingElement element, float angleInDegree)
    {
        UpdatePosition(element, angleInDegree);
        // SetArcAndRadius(element, angleInDegree);
        UpdateRotation(element,angleInDegree ); 
              
       
    }

    private void SetArcAndRadius(RingElement element)
    {
        float radiusDistance = Vector3.Distance(element.transform.position, new Vector3());
        //element.Radius = radiusDistance;

        float newArcSize = ((360f * arcLength)) / (2f * Mathf.PI);
        //float radius = ((radiusDistance) + element.Radius + 0.5f);
        float radius = ((element.Radius * 1.5f) );

        newArcSize = newArcSize/(radius);

        //newArcSize *= radiusDistance/(RadiusA*RadiusB);

        //newArcSize = newArcSize*(radiusDistance/RadiusB);
        
        //newArcSize = newArcSize*(1 + ((radiusDistance - RadiusB)/RadiusA));
        element.Arc = newArcSize;

        //element.Arc = (newArcSize/radiusDistance)*3.5f;


    }

    private void UpdateArcAndRadiusDifferentSizes(RingElement element, float angleInDegree)
    {
        float radiusDistance = Vector3.Distance(element.transform.position, new Vector3());
        //element.Radius = radiusDistance;

        float newArcSize = ((360f * arcLength)) / (2f * Mathf.PI);
        float radius = ((radiusDistance) + element.Radius + 0.5f);
        //radius = ((element.Radius / 2) );

        newArcSize = newArcSize/(radius);

        //newArcSize *= radiusDistance/(RadiusA*RadiusB);

        //newArcSize = newArcSize*(radiusDistance/RadiusB);
        
        newArcSize = newArcSize*(1 + ((radiusDistance - RadiusB)/RadiusA));
        element.Arc = newArcSize ;

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
        float a = RadiusA + 5;
        float b = RadiusB + 5;
        float lamda = (a - b) / (a + b);
        float lamda3 = 3 * lamda * lamda;
        float circumferenceEllipse = (a + b) * Mathf.PI * (1 + (lamda3 / (10 + Mathf.Sqrt(4 - lamda3))));

        //arcSize = (circumferenceEllipse*360f)/(2*Mathf.PI*(radiusSize))/ElementsCount;

        arcLength = circumferenceEllipse/ElementsCount;
       // Debug.Log("Umfang: " + circumferenceEllipse +  "  ArchLength: " + arcLength);

        //arcSize *= 2;

        //objectCount = (int)(circumference / objectSize);
    }

    private void ConvertToValidRange(ref float angle)
    {
        angle = angle % 360f;

        if (angle < 0f)
            angle = 360f + angle;
    }

}
