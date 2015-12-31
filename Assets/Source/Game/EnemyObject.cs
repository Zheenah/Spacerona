using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source
{
    public class EnemyObject : MonoBehaviour
    {

        void OnCollisionEnter2D(Collision2D coll)
        {
            foreach (var contactPoint2D in coll.contacts)
            {
                //Debug.Log(contactPoint2D.);
            }
            //Debug.Log(coll.contacts);

            //coll.contacts
        }
    }
}
