using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkController : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    float walkSpeed = 0.1f;

    [SerializeField]
    bool useGravity = true;

    [SerializeField]
    new Transform camera;

    new Rigidbody rigidbody;
    float lastTime;

    // Use this for initialization
    void Start()
    {
        lastTime = Time.time;
        rigidbody = GetComponent<Rigidbody>() ? GetComponent<Rigidbody>() : gameObject.AddComponent<Rigidbody>();
        rigidbody.useGravity = useGravity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            float absolutedVertical = Mathf.Abs(vertical);
            Quaternion newrota = Quaternion.LookRotation(
                (new Vector3(camera.forward.x, rigidbody.position.y, camera.forward.z) * absolutedVertical) + (new Vector3(camera.right.x, rigidbody.position.y, camera.right.z) * horizontal),
                Vector3.up);

            rigidbody.rotation = newrota;
            rigidbody.position += newrota * Vector3.forward * walkSpeed * (Time.time - lastTime) * Mathf.Min((Mathf.Abs(horizontal) + absolutedVertical), 1f) * Mathf.Sign(vertical);

            animator.SetBool("Walk", true);
            if (vertical >= 0)
                animator.SetBool("BackWalk", false);
            else
                animator.SetBool("BackWalk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("BackWalk", false);
        }

        lastTime = Time.time;
    }
}
