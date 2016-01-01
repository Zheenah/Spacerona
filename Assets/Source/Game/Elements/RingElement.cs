
using UnityEngine;

namespace Assets.Source
{
    public class RingElement  : MonoBehaviour
    {


        public float StartRadius = 2.74f;
        public float StartArc = 45f;
        public Color StartColor = new Color(0,97,255);
        ///
        /// 
        /// 
        public ParticleSystem[] ArcParticles;
        private float[] defaultEmissions;
        //SegmentParticles, ToMidParticles, CornerParticles, SparkleParticles, LinesParticles;
        
        private float defaultArc;
        private float defaultRadMeasure;
        private ParticleSystem.MinMaxCurve defaultEmission;
        public BoxCollider2D boxCollider;
        private float arc;
        public float Arc
        {
            get  { return arc;}
            set
            {
                arc = value;
                for (int i = 0; i < ArcParticles.Length; i++)
                {
                    ArcParticles[i].Clear();
                    

                    ArcParticles[i].emissionRate = (defaultEmissions[i]/ defaultRadMeasure) *value * ArcParticles[i].shape.radius;
                    ArcParticles[i].Simulate(4f);
                    ArcParticles[i].Play();
                    //SetCollider(value);
                    SetArc(ArcParticles[i], value);
                }
            }
        }


        private void SetCollider(float angleInDegree)
        {
            if (boxCollider == null)
                return;

            Vector2 newSize = boxCollider.size;
            Debug.Log("oldsize: " + newSize);

            newSize.y = (2*5*Mathf.PI)*angleInDegree/360f;
            boxCollider.size = newSize;
        }

        private float radius;
        public float Radius
        {
            get { return radius; }
            set
            {
                arc = value;
                for (int i = 0; i < ArcParticles.Length; i++)
                {
                    //ArcParticles[i].Clear();
                    ArcParticles[i].emissionRate = (defaultEmissions[i] / defaultRadMeasure) * value * ArcParticles[i].shape.arc;
                    SetRadius(ArcParticles[i], value);
                }
            }
        }


        private Color color;
        public Color Color
        {
            get { return color; }
            set
            {
                color = value;
                for (int i = 0; i < ArcParticles.Length-1; i++)
                {
                    ArcParticles[i].startColor = new Color(value.r, value.g, value.b, 146f);
                }
            }
        }


        // Use this for initialization
        void Awake ()
        {
            if (ArcParticles.Length <= 0 && ArcParticles[0] == null)
                return;

            defaultEmissions = new float[ArcParticles.Length];

            for (int i = 0; i < ArcParticles.Length; i++)
            {
                defaultEmissions[i] = ArcParticles[i].emissionRate;
            }
            defaultArc = ArcParticles[0].shape.arc;
            defaultRadMeasure = ArcParticles[0].shape.arc*ArcParticles[0].shape.radius;

            Arc = StartArc;
            Radius = StartRadius;
            Color = StartColor;

            
        }
	
        // Update is called once per frame
        void Update () {
	
        }

        private void SetParticleValues(ParticleSystem p, float arc, float radius)
        {
            //float oldArc = p.a
            SetArc(p, arc);
            SetRadius(p, radius);
        }

        private void SetArc(ParticleSystem p, float arc)
        {
            var shape = p.shape;
            shape.arc = arc;
        }

        private void SetRadius(ParticleSystem p, float radius)
        {
            var shape = p.shape;
            shape.radius = radius;
        }

    }
}
