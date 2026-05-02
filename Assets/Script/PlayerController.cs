using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject ammoOnHead; // วงกลมบนหัว

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float hInput;

    [HideInInspector] public bool hasAmmo = false;
    [HideInInspector] public bool isControllingCannon = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        rb.freezeRotation = true;
        ammoOnHead.SetActive(false);
    }

    void Update()
    {
        if (isControllingCannon) { hInput = 0; return; }

        hInput = 0;
        if (Input.GetKey(KeyCode.A)) { hInput = -1; sr.flipX = true; }
        else if (Input.GetKey(KeyCode.D)) { hInput = 1; sr.flipX = false; }
    }

    void FixedUpdate()
    {
        if (!isControllingCannon)
            rb.linearVelocity = new Vector2(hInput * moveSpeed, rb.linearVelocity.y);
    }

    public void SetAmmo(bool state)
    {
        hasAmmo = state;
        ammoOnHead.SetActive(state);
    }
}