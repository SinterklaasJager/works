using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{

    PlayerControls controls;
    public Animator animator;
    private Rigidbody rb;

    public GameObject shootCamera;
    public GameObject standardCamera;
    public GameObject crossHair;

    bool jumping;
    bool grounded;
    bool attacking;
    bool rotateHorizontal = true;
    bool rotateVertical = true;
    bool doubleJumpAble;
    bool gunHolstered = true;
    bool swordHolstered;

    int SoftAttackNumber = 0;
    int jumpAmount;
    public int speed = 200;
    int maxSpeed;
    public int rotationSpeed = 100;

    Vector3 groundNormal;
    bool isGrounded;
    float origGroundCheckDistance;
    public float groundCheckDistance = 0.1f;

    //Melee Combat Variables
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public GameObject sword;

    //Ranged Combat
    public GameObject gun;
    public ParticleSystem explosion;
    Vector2 move;
    // Vector2 camera;
    Vector3 direction;
    Vector3 target;

    public GameObject MainCamera;
    AudioSource audioSource;
    public AudioClip gunShot;
    public AudioClip playerGrunt;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Jump.performed += ctx => Jump();

        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector3.zero;

        controls.Gameplay.SoftAttack.performed += ctx => SoftAttack();
        controls.Gameplay.HardAttack.performed += ctx => HardAttack();
        controls.Gameplay.Shoot.performed += ctx => Shoot();

        // controls.Gameplay.Camera.performed += ctx => camera = ctx.ReadValue<Vector2>();
        // controls.Gameplay.Camera.canceled += ctx => camera = Vector3.zero;

        controls.Gameplay.CameraReset.performed += ctx => CameraReset();
    }

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        origGroundCheckDistance = groundCheckDistance;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Jump()
    {

        if (CheckGroundStatus() || doubleJumpAble)
        {
            animator.SetTrigger("Jump");


            if (doubleJumpAble)
            {
                StartCoroutine(doubleJumpCoroutine());
            }
            else
            {
                StartCoroutine(jumpCoroutine());
            }
        }

    }

    bool CheckGroundStatus()
    {
        RaycastHit hitInfo;
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * groundCheckDistance), Color.red);

        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, groundCheckDistance))
        {
            groundNormal = hitInfo.normal;
            return true;
            // animator.applyRootMotion = true;
        }
        else
        {
            groundNormal = Vector3.up;
            return false;
            // animator.applyRootMotion = false;
        }
    }

    void SoftAttack()
    {
        if (swordHolstered)
        {
            animator.SetTrigger("getSword");
            swordHolstered = false;
            gunHolstered = true;
            gun.SetActive(false);
            sword.SetActive(true);
            shootCamera.SetActive(false);
            standardCamera.SetActive(true);
            crossHair.SetActive(false);
        }
        else
        {
            if (Time.time >= nextAttackTime)
            {
                audioSource.PlayOneShot(playerGrunt);
                animator.SetTrigger("AttackSoft");
                DealDamage(20);
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        // if (SoftAttackNumber == 0)
        // {

        //     animator.SetInteger("SoftAttack", 1);
        //     SoftAttackNumber = 1;
        // }
        // else if (SoftAttackNumber == 1)
        // {
        //     animator.SetInteger("SoftAttack", 2);
        //     SoftAttackNumber = 2;
        // }
        // else if (SoftAttackNumber == 2)
        // {
        //     animator.SetInteger("SoftAttack", 1);
        //     SoftAttackNumber = 1;

        // }

    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    void HardAttack()
    {
        if (swordHolstered)
        {
            animator.SetTrigger("getSword");
            swordHolstered = false;
            gunHolstered = true;
            gun.SetActive(false);
            sword.SetActive(true);
            shootCamera.SetActive(false);
            standardCamera.SetActive(true);
            crossHair.SetActive(false);
        }
        else
        {
            if (Time.time >= nextAttackTime)
            {
                audioSource.PlayOneShot(playerGrunt);
                animator.SetTrigger("HardAttack");
                DealDamage(40);
                nextAttackTime = Time.time + 1f / (attackRate / 2);
            }
        }


    }
    void Shoot()
    {
        if (gunHolstered)
        {
            animator.SetTrigger("getGun");
            gun.SetActive(true);
            sword.SetActive(false);
            gunHolstered = false;
            swordHolstered = true;
            shootCamera.SetActive(true);
            standardCamera.SetActive(false);
            crossHair.SetActive(true);
        }
        else
        {
            if (Time.time >= nextAttackTime)
            {
                explosion.Play();
                // int layerMask = 1 << 8;
                audioSource.PlayOneShot(gunShot);
                RaycastHit hit;

                Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);

                if (Physics.Raycast(ray, out hit, 100f))
                {
                    Debug.Log(hit.collider);
                    // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
                    // transform.LookAt(hit.point);

                    animator.SetTrigger("shoot");
                    if (hit.collider.tag == "Enemy")
                    {
                        hit.collider.gameObject.GetComponent<Enemy>().enemyHealth.Damage(10);
                    }



                    nextAttackTime = Time.time + 1f / (attackRate * 2);
                }
            }
        }

    }

    void DealDamage(int damage)
    {

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("we hit" + enemy.name);
            enemy.GetComponent<Enemy>().enemyHealth.Damage(damage);
        }

    }

    void Move()
    {

    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void CameraReset()
    {
        // MainCamera.transform.localEulerAngles = new Vector3(0, 0, 0);
        updateLookDirection();
    }

    void CameraControls()
    {

    }

    void MovementControls()
    {
        float x = move.x * Time.deltaTime * rotationSpeed;
        float y = move.y * Time.deltaTime * speed;


        animator.SetFloat("Speed", Mathf.Abs(y));

        if (CheckGroundStatus())
        {
            animator.SetBool("InAir", false);
            groundedMovement(x, y);
        }
        else
        {
            animator.SetBool("InAir", true);
            airMovement(x, y);
        }

    }
    void groundedMovement(float x, float y)
    {
        maxSpeed = 10;


        if (move.magnitude == 0)
        {
            rb.velocity = rb.velocity - new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        }

        if (Mathf.Abs(y) > 1f)
        {
            rb.AddRelativeForce(0, 0, y, ForceMode.Force);
        }
        if (gunHolstered)
        {
            if (Mathf.Abs(x) > 1.5f)
            {
                gameObject.transform.Rotate(0, x * 2, 0, Space.Self);
            }
            else if (Mathf.Abs(x) > 0.5f)
            {
                gameObject.transform.Rotate(0, x, 0, Space.Self);
            }
        }
        else
        {
            rb.AddRelativeForce(x * 10, 0, 0, ForceMode.Force);
        }

    }

    void airMovement(float x, float y)
    {

        maxSpeed = 8;

        if (Mathf.Abs(y) > 1f)
        {
            rb.AddRelativeForce(0, 0, y, ForceMode.Force);
        }
    }
    void updateLookDirection()
    {
        // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Plane plane = new Plane(Vector3.up, Vector3.zero);

        // float distance;
        // if (plane.Raycast(ray, out distance))
        // {
        //     target = ray.GetPoint(distance);
        //     direction = (target - transform.position) * -1;
        //     float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        //     transform.rotation = Quaternion.Euler(0, rotation, 0);
        // }
    }

    IEnumerator groundedCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        rb.AddForce(0, -500f, 0, ForceMode.Force);
        yield return new WaitForSeconds(1f);
        rb.AddForce(0, -5000f, 0, ForceMode.Impulse);
    }
    IEnumerator jumpCoroutine()
    {

        // Debug.Log("jump");
        rb.AddForce(0, 5, 0, ForceMode.Impulse);
        // jumpAmount++;
        yield return new WaitForSeconds(0.15f);
        // Debug.Log("DoubmeJumpAble");
        doubleJumpAble = true;
        yield return new WaitForSeconds(0.5f);
        doubleJumpAble = false;
        // Debug.Log("down");
        rb.AddForce(0, -100f, 0, ForceMode.Force);
    }
    IEnumerator doubleJumpCoroutine()
    {
        StopCoroutine(jumpCoroutine());
        // Debug.Log("doublejump");
        rb.AddForce(0, 4, 0, ForceMode.Impulse);
        // jumpAmount++;
        yield return new WaitForSeconds(0.45f);
        // Debug.Log("down");
        rb.AddForce(0, -150f, 0, ForceMode.Force);
        doubleJumpAble = false;
    }


    void FixedUpdate()
    {
        MovementControls();

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }


    }
    // Update is called once per frame
    void Update()
    {

        CameraControls();
        updateLookDirection();


    }
}
