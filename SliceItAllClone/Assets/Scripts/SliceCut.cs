using EzySlice;
using System.Collections;
using TMPro;
using UnityEngine;

public class SliceCut : MonoBehaviour
{
    public Material[] materials = new Material[5];
    public Material materialwatermelon;
    public Material metarialslice;
    public bool gravity, kinematic;
    public float ExplosionForce;
    public float ExplosionRadius;
    [SerializeField] private TMP_Text popUpScore;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Slice"))
        {
           int materialIndex = Random.Range(1, 5);
           metarialslice = materials[materialIndex];
            if (other.gameObject != null)
            {
                SlicedHull sliceobj = Slice(other.gameObject, metarialslice);
                if (sliceobj != null)
                {
                    
                    GameObject slicedObjedTop = sliceobj.CreateUpperHull(other.gameObject, metarialslice);
                    GameObject slicedObjedDown = sliceobj.CreateLowerHull(other.gameObject, metarialslice);
                    Destroy(other.gameObject);
                    Addcompanet(slicedObjedTop, ExplosionForce);
                    Addcompanet(slicedObjedDown, ExplosionForce);
                    Destroy(slicedObjedTop, 1);
                    Destroy(slicedObjedDown, 1); 
                    Instantiate(popUpScore, slicedObjedTop.transform.position + new Vector3(3, 3, -3), Quaternion.Euler(0, -60, 0));
                }
            }
        }

        if(other.gameObject.CompareTag("watermelon"))
        {
            if (other.gameObject != null)
            {
                SlicedHull sliceobj = Slice(other.gameObject, materialwatermelon);
                if (sliceobj != null)
                {
                    GameObject slicedObjedTop = sliceobj.CreateUpperHull(other.gameObject, materialwatermelon);
                    GameObject slicedObjedDown = sliceobj.CreateLowerHull(other.gameObject, materialwatermelon);
                    Destroy(other.gameObject);
                    Addcompanet(slicedObjedTop, ExplosionForce);
                    Addcompanet(slicedObjedDown, ExplosionForce);
                    Destroy(slicedObjedTop,1);
                    Destroy(slicedObjedDown,1);
                    Instantiate(popUpScore, slicedObjedTop.transform.position + new Vector3(3, 3, -3), Quaternion.Euler(0, -60, 0));
                }
            }
        }
    }

    private SlicedHull Slice(GameObject obj1, Material mat)
    {
        return obj1.Slice(transform.position, transform.right, mat);

    }
    void Addcompanet(GameObject obj1, float ExplosionForce)
    {
        obj1.AddComponent<BoxCollider>();
        var rigidbody = obj1.AddComponent<Rigidbody>();
        rigidbody.useGravity = gravity;
        rigidbody.isKinematic = kinematic;
        rigidbody.AddExplosionForce(ExplosionForce, obj1.transform.position, ExplosionRadius);
        
    }
}
