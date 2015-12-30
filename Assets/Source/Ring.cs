using System.Collections;
using UnityEngine;

namespace Assets.Source
{
    public class Ring : MonoBehaviour
    {

        public float A;
        public float B;
        public int ElementsOnRing;
        public RingElement ElementPrefab;

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
            //StartCoroutine(ShrinkAndGrowRing());
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
                newElement.transform.Rotate(new Vector3(0f,0f,arc * i));
                ringElements[i] = newElement;
            }
            
            
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
