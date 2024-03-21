using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : PickUp
{
    [Header("Settings")]
    [SerializeField] private float collisionpushSpeed = 20f;
    [SerializeField] private float destroyDellay = 3f;
    [SerializeField] Vector3 collisionTorgue = new Vector3(2f,2f,2f);
    protected override void PickUpBy(GameObject picker)
    {
        GetMovementComponent().enabled = false;
        GetComponent<Collider>().enabled = false;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddForce((transform.position - picker.transform.position).normalized * collisionpushSpeed, ForceMode.VelocityChange);
        rb.AddTorque(collisionTorgue, ForceMode.VelocityChange);

        Invoke(nameof(DestroySelf), destroyDellay);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

}
