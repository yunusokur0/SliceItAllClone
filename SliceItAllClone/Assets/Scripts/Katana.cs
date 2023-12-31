using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Katana : MonoBehaviour
{
    public static Katana katana;
    private Rigidbody rb;
    private bool move;
    public Vector3 force;
    public Vector3 forcePB;
    private float realRotationAmount;
    public float rotationAmount;
    public float rotationAmount123;
    [SerializeField] private float maxHorizontalVelocity, minDegree, maxDegree;
    [SerializeField] private float torque;
    private bool angDrag = false;
    private bool isKnifeOnPlatform;

    public Text xAmountText;
    void Start()
    {
        katana = this;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            move = true;

            if (isKnifeOnPlatform)
            {
                Flip();
                StartCoroutine(timer());
                rb.isKinematic = false;
                isKnifeOnPlatform = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (move)
        {
                StartCoroutine(Timer());
                Flip();
                move = false;                     
        }
        else
        {
            if (realRotationAmount >= minDegree && realRotationAmount < maxDegree && angDrag)
            {
                rb.angularDrag = 7f;
            }
        }
        Move();
    }
    public void Flip()
    {
        rb.velocity = new Vector3(0, 0, 0);
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(force, ForceMode.Impulse);
        rb.AddTorque(torque, 0f, 0f, ForceMode.Impulse);
    }

    IEnumerator Timer()
    {
        angDrag = false;
        rb.angularDrag = 0.05f;
        yield return new WaitForSeconds(.3f);
        angDrag = true;
    }
    private void Move()
    {
        realRotationAmount = UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).x;
        if (rb.velocity.magnitude > maxHorizontalVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxHorizontalVelocity;
        }
        rotationAmount = transform.eulerAngles.x;
    }

    public void PushBack()
    {
        rb.velocity = new Vector3(0, 0, 0);
        rb.AddForce(forcePB, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            rb.isKinematic = true;
            isKnifeOnPlatform = true;
        }

        if(other.gameObject.CompareTag("X"))
        {
            rb.isKinematic = true;          
            Value xAmount = other.gameObject.GetComponent<Value>();
            xAmountText.text = xAmount.xAmount.ToString() + "X";
        }
    }

    IEnumerator timer()
    {
        Physics.IgnoreLayerCollision(6, 7);
        yield return new WaitForSeconds(1f);
        Physics.IgnoreLayerCollision(6, 7, false);
    }
}
