
using UnityEngine;

namespace Assets.Source
{
    public class RingElement  : MonoBehaviour
    {


        public float StartRadius = 2.74f;
        public float StartArc = 45f;
        ///
        /// 
        /// 
        public ParticleSystem[] ArcParticles;
        private float[] defaultEmissions;
        //SegmentParticles, ToMidParticles, CornerParticles, SparkleParticles, LinesParticles;
        
        private float defaultArc;
        private float defaultRadMeasure;
        private ParticleSystem.MinMaxCurve defaultEmission;

        private float arc;
        public float Arc
        {
            get  { return arc;}
            set
            {
                arc = value;
                for (int i = 0; i < ArcParticles.Length; i++)
                {
                    ArcParticles[i].emissionRate = (defaultEmissions[i]/ defaultRadMeasure) *value * ArcParticles[i].shape.radius;
                    SetArc(ArcParticles[i], value);
                }
            }
        }

        private float radius;
        public float Radius
        {
            get { return arc; }
            set
            {
                arc = value;
                for (int i = 0; i < ArcParticles.Length; i++)
                {
                    ArcParticles[i].emissionRate = (defaultEmissions[i] / defaultRadMeasure) * value * ArcParticles[i].shape.arc;
                    SetRadius(ArcParticles[i], value);
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
