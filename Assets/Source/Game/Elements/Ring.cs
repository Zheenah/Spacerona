using System.Collections;
using Assets.Source.Utils;
using UnityEngine;

namespace Assets.Source
{
    public class Ring : MonoBehaviour
    {

        public float A;
        public float B;
        public int ElementsOnRing;
        public RingElement ElementPrefab;
        public Color[] Colors;

        ///
        /// 
        /// 
        private RingElement[] ringElements;
        // Use this for initialization

        void Awake()
        {
            GetComponent<CircleCollider2D>().radius = A;
        }


        void Start () {

            CreateRing();
        }
	
        // Update is called once per frame
        void Update () {
	
        }

        private void CreateRing()
        {
            float arc = 360f/ElementsOnRing;
            ringElements = new RingElement[ElementsOnRing];
            for (int i = 0; i < ElementsOnRing; i++)
            {
                RingElement newElement = (RingElement)Instantiate(ElementPrefab, transform.position, Quaternion.identity);
                newElement.transform.parent = transform;
                newElement.Arc = arc;
                newElement.Radius = A;
                int randColor = Random.Range(0, Colors.Length);
                newElement.Color = Colors[randColor];
                
                    
                newElement.transform.Rotate(new Vector3(0f,0f,arc * i));
                ringElements[i] = newElement;
            }
            
            
        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            Vector3 diff = coll.transform.position - transform.position;
            float angle = diff.GetAngleBetween360(Vector3.left, -Vector3.forward);
            RingElement element = GetRingElement(angle);
            element.gameObject.SetActive(false);
            


        }

        private RingElement GetRingElement(float angle)
        {
            float ringAngle = angle - transform.rotation.eulerAngles.z;
            Debug.Log("ringangle: " + ringAngle);
            ConvertToValidRange(ref ringAngle);
            float arc = 360f / ElementsOnRing;
            int index = (int) (ringAngle/arc);
            Debug.Log("Index: " + index);
            return ringElements[index];
            

        }

        private void ConvertToValidRange(ref float angle)
        {
            angle = angle % 360f;

            if (angle < 0f)
                angle = 360f + angle;
        }
        private IEnumerator ShrinkAndGrowRing()
        {
            float shrinkVelocity = 0.4f;
            while (true)
            {
                yield return new WaitForEndOfFrame();

                float radius = -1f;
                foreach (var ringElement in ringElements)
                {
                    radius = ringElement.Radius -= shrinkVelocity * Time.deltaTime;
                }
                if (radius <= 1f || radius > 6f)
                {
                    shrinkVelocity *= -1f;
                }

            }
           
        }
    }
}
