using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField]
    private InputController _inputController;
    private Animator _anmAnimator;

    private const string Atk1 = "atk1";
    private const string Jump = "jump";
    private const string HorizontalSpeed = "horizontalSpeed";
    private const string BattleLayer = "BattleLayer";
    private BulletContoller bulletContoller;
    public float MaxChargingTime = 2.0f;
  
    private float _currentChargingTime = 0f;



    void Awake()
    {
        _anmAnimator = GetComponent<Animator>();
        bulletContoller = GetComponentInChildren<BulletContoller>();
    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (_inputController.IsMove)
        {
            _anmAnimator.SetFloat(HorizontalSpeed, Mathf.Lerp(_anmAnimator.GetFloat(HorizontalSpeed), 1, 0.2f));
        }
        else
        {
            _anmAnimator.SetFloat(HorizontalSpeed, 0);
        }

        _anmAnimator.SetBool(Jump, !_inputController.IsOnGround);

        if (_inputController.IsAtk1Down)
        {
            // Debug.Log("攻击舰 ");
            _inputController.CanMove = false;
            _anmAnimator.SetLayerWeight(_anmAnimator.GetLayerIndex(BattleLayer), 1);
            _anmAnimator.SetTrigger(Atk1);
            _inputController.IsAtk1Down = false;
        }

        CheckCharging();
    }



    public void ClearAtkTrigger()
    {
        _anmAnimator.ResetTrigger(Atk1);
    }


    public void ComboAtk()
    {
        if (_inputController.IsAtk1Down)
        {
            _anmAnimator.SetTrigger(Atk1);
        }
        _anmAnimator.ResetTrigger(Atk1);
    }



    public void CheckCharging()
    {
        if (_inputController.IsAtk1Pressing)
        {
            _currentChargingTime += Time.deltaTime;
            if (_currentChargingTime > MaxChargingTime)
            {
                ChargingFinish();
            }
         
        }
        else
        {
            
            if (!_anmAnimator.enabled)
            {
                ChargingFinish(_currentChargingTime > 1.0f);
            }
            _currentChargingTime = 0f;
        }

    }

    private void ChargingFinish(bool ifGenerateBullet = true)
    {
        _anmAnimator.enabled = true;
        _currentChargingTime = 0f;
        if (ifGenerateBullet)
        {
            bulletContoller.NeedToGenerateCount++;
        }
       

    }

    /// <summary>
    /// 蓄力
    /// </summary>
    public void ChargingAtk()
    {
        if (_inputController.IsAtk1Pressing)
        {
            _anmAnimator.enabled = false;
        }
    }


    
    /// <summary>
    /// 攻击完成时
    /// </summary>
    public void OnAtkFinish()
    {
        _inputController.CanMove = true;
        _anmAnimator.SetLayerWeight(_anmAnimator.GetLayerIndex(BattleLayer), 0);
        ClearAtkTrigger();
    }


}
