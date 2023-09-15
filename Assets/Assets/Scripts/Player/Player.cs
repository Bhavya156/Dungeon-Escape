using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public int diamonds;
    [SerializeField]
    private float _jumpForce = 5.0f;
    private bool _grounded;
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private LayerMask _groundLayer;
    private bool _resetJumpNeeded;
    [SerializeField]
    private float _playerSpeed = 2.5f;
    private PlayerAnimation _playerAnimation;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _swordArcRenderer;

    public int Health{ get; set;}
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _swordArcRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
        Health = 4;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (Input.GetMouseButtonDown(0) && isGrounded() == true)
        {
            _playerAnimation.Attack();
        }
    }

    void Flip(bool facingRight)
    {
        if (facingRight == true)
        {
            _spriteRenderer.flipX = false;
            _swordArcRenderer.flipX = false;
            _swordArcRenderer.flipY = false;
            Vector3 newPos = _swordArcRenderer.transform.localPosition;
            newPos.x = 1.01f;
            _swordArcRenderer.transform.localPosition = newPos;
        }
        else if (facingRight == false)
        {
            _spriteRenderer.flipX = true;
             _swordArcRenderer.flipX = true;
            _swordArcRenderer.flipY = true;
            Vector3 newPos = _swordArcRenderer.transform.localPosition;
            newPos.x = -1.01f;
            _swordArcRenderer.transform.localPosition = newPos;
        }
    }

    void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");
        _grounded = isGrounded();

       if (move > 0)
        {
            Flip(true);
        }
        else if (move < 0)
        {
            Flip(false);
        }
        
        _rigidbody.velocity = new Vector2(move * _playerSpeed, _rigidbody.velocity.y);

        _playerAnimation.Move(move);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded() == true)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
           StartCoroutine(resetJumpRoutine());
           _playerAnimation.Jump(true);
        }
        
    }

    bool isGrounded()
    {
        RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, Vector2.down, 1f, _groundLayer.value);
        if (hitinfo.collider != null)
        {
            if (_resetJumpNeeded == false)
            {
                _playerAnimation.Jump(false);
                return true;
            }
        }
        return false;
    }

    IEnumerator resetJumpRoutine()
    {
        _resetJumpNeeded = true;
        yield return new WaitForSeconds(0.1f);
        _resetJumpNeeded = false;
    }

    public void Damage()
    {
        if (Health < 1)
        {
            return;
        }
        Debug.Log("Player damaged");
        Health--;
        UIManger.Instance.PlayerHealth(Health);
        if (Health < 1)
        {
            _playerAnimation.Death();
        }
    }

    public void DisplayGemCount(int amount)
    {
        diamonds += amount;
        UIManger.Instance.PlayerGemCount(diamonds);
    }
}
