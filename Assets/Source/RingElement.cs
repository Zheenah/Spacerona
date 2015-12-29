using UnityEditor;
using UnityEngine;

namespace Assets.Source
{
    public class RingElement  : MonoBehaviour
    {

        public ParticleSystem[] ArcParticles;
        private float[] defaultEmissions;
        //SegmentParticles, ToMidParticles, CornerParticles, SparkleParticles, LinesParticles;
        public float StartArc = 45f;
        private float defaultArc;
        private ParticleSystem.MinMaxCurve defaultEmission;

        private float arc;

        public float Arc
        {
            get
            {
                return arc;
            }
            set
            {
                arc = value;

                for (int i = 0; i < ArcParticles.Length; i++)
                {
                    ArcParticles[i].emissionRate = (defaultEmissions[i]/defaultArc)*value;
                    SetArc(ArcParticles[i], value);
                }
                    

            }
        }


        // Use this for initialization
        void Start ()
        {
            if (ArcParticles.Length <= 0 && ArcParticles[0] == null)
                return;

            defaultEmissions = new float[ArcParticles.Length];

            for (int i = 0; i < ArcParticles.Length; i++)
            {
                defaultEmissions[i] = ArcParticles[i].emissionRate;
            }
            defaultArc = ArcParticles[0].shape.arc;

            Arc = StartArc;
        }
	
        // Update is called once per frame
        void Update () {
	
        }

        private void SetParticleValues(ParticleSystem p, float arc)
        {
            //float oldArc = p.a
            SetArc(p, arc);
        }

        private void SetArc(ParticleSystem p, float value)
        {
            SerializedObject so = new SerializedObject(p);
            so.FindProperty("ShapeModule.arc").floatValue = value;
            so.ApplyModifiedProperties();
        }

        private void SetEmissionRate(ParticleSystem p, float value)
        {
            SerializedObject so = new SerializedObject(p);
            so.FindProperty("ShapeModule.arc").floatValue = value;
            so.ApplyModifiedProperties();
        }
    }
}
