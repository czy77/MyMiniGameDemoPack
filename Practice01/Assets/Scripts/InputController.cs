using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [Header("公开的变量")] public float HorizontalSpeed = 3.0f;
    public float JumpForce = 10.0f;
    public float DashForce = 800.0f;
    public float DashTime = 0.5f;
 
    // public bool IsJump;
    public bool IsMove;
    public bool IsOnGround;
    public bool IsAtk1Down;
    public bool IsAtk1Pressing;
    public bool CanMove = true;
    public int MaxJumpCount = 1;


    private Rigidbody2D _rigidbody2D;

    private bool _isLeftDown;
    private bool _isRightDown;
    private bool _isJumpDown;
    private bool _isDashDown;
    private int _currentJumpCount;
    private float _currentDashTime;
    private bool _isNeedToDash;
    private KeyCode Up = KeyCode.UpArrow;
    private KeyCode Down = KeyCode.DownArrow;
    private KeyCode Left = KeyCode.LeftArrow;
    private KeyCode Right = KeyCode.RightArrow;
    private KeyCode Jump = KeyCode.Space;
    private KeyCode Atk1 = KeyCode.A;
    private KeyCode Dash = KeyCode.LeftControl;
    private bool faceLeft;


    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private LayerMask groundMask;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _isLeftDown = Input.GetKey(Left);
        _isRightDown = Input.GetKey(Right);
        _isJumpDown = Input.GetKeyDown(Jump);
        _isDashDown = Input.GetKeyDown(Dash);
        IsAtk1Down = IsAtk1Down ? IsAtk1Down : Input.GetKeyDown(Atk1);
        IsAtk1Pressing = Input.GetKey(Atk1);
        
        if (IsOnGround)
        {
            _currentJumpCount = 0;
        }

        if (_isJumpDown)
        {
            _currentJumpCount++;
            // Debug.Log(_currentJumpCount);
            if (_currentJumpCount <= MaxJumpCount && IsOnGround)
            {
                this._rigidbody2D.AddForce(new Vector2(this._rigidbody2D.velocity.x, JumpForce));
            }
        }

        if (_isDashDown)
        {
            _isNeedToDash = true;
        }
        
    }


    void FixedUpdate()
    {
        HandleInput();
        
        DoDash();
       
        
    }


    private void DoDash()
    {
        if (_isNeedToDash)
        {
            _currentDashTime += Time.fixedDeltaTime;
            float toAddDashForce = faceLeft ? -DashForce : DashForce;
            this._rigidbody2D.AddForce(new Vector2(toAddDashForce, this._rigidbody2D.velocity.y),ForceMode2D.Force);
            if (_currentDashTime > DashTime)
            {
                _currentDashTime = 0f;
                _isNeedToDash = false;
            }
        }
    }
    
    /// <summary>
    /// 处理用户输入
    /// </summary>
    private void HandleInput()
    {

        float moveSpeed = 0.0f;
        if (_isLeftDown && CanMove)
        {
            if (!faceLeft)
            {
                FlipFace();
            }
            moveSpeed = -HorizontalSpeed;
        }

        if (_isRightDown && CanMove)
        {

            if (faceLeft)
            {
                FlipFace();
            }
            moveSpeed = HorizontalSpeed;
           
        }

        IsMove = moveSpeed > 0 || moveSpeed < 0;

        if (CanMove && IsOnGround)
        {
            this._rigidbody2D.velocity = new Vector2(moveSpeed, this._rigidbody2D.velocity.y);
        }
       

        //判断是否在地面
        Collider2D overlapCircle =
            Physics2D.OverlapCircle(new Vector2(groundCheckPoint.position.x, groundCheckPoint.position.y), 0.4f,
                groundMask);
        Debug.DrawLine(groundCheckPoint.position, groundCheckPoint.position + new Vector3(0, -0.4f, 0), Color.blue);
        IsOnGround = overlapCircle != null;
    }



    private void FlipFace()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        faceLeft = !faceLeft;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        bool isHitEnemy = col.CompareTag("Enemy");
        //
        if (isHitEnemy)
        {
            Enemy enemy = col.GetComponent<Enemy>();
            enemy.TakeDamage(new Damage(10.0f));
        }

        Debug.Log("HIt you " + col.name);
    }
}