using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update

    private int _animIDAttack;
    private int _animIDBig;
    private int _animIDSpinLoop;
    private int _animIDSpinAttack;
    private int loopcnt = 10;
    public int damage;
    private bool isPlaying = false;
    //GameObject Object;
    Weapon weapon;

    public Transform WeaponTransform;


    private PlayerInput _playerInput;
    private Animator _animator;
    private CharacterController _controller;
    private StarterAssetsInputs _input;

    private bool _hasAnimator;
    private void Start()
    {
        
        _hasAnimator = TryGetComponent(out _animator);
        _controller = GetComponent<CharacterController>();
        _input = GetComponent<StarterAssetsInputs>();
#if ENABLE_INPUT_SYSTEM 
        _playerInput = GetComponent<PlayerInput>();
#else
        Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif

        AssignAnimationIDs();

        weapon = GameObject.FindGameObjectWithTag("Melee").GetComponent<Weapon>();
    }

    private void AssignAnimationIDs()
    {
        _animIDAttack = Animator.StringToHash("Attack");
        _animIDBig = Animator.StringToHash("Big");
        _animIDSpinLoop = Animator.StringToHash("SpinLoop");
        _animIDSpinAttack = Animator.StringToHash("SpinAttack");

    }

    // Update is called once per frame
    void Update()
    {
        _hasAnimator = TryGetComponent(out _animator);
        Attack();
        BigAttack();
        SpinAttack();
        SpinLoop();
    }


    private void Attack()
    {
        if (_input.attack)
        {
            _animator.SetTrigger(_animIDAttack);
            weapon.Use(damage, 0.1f, 0.4f, 0.4f);
        }
        _input.attack = false;
    }

    private void BigAttack()
    {
        if (_input.big)
        {
            _animator.SetTrigger(_animIDBig);
            weapon.Use(damage * 2, 0.1f, 0.7f, 0.7f);
        }
        _input.big = false;
    }

    private void SpinLoop()
    {
        if (_input.spinloop)
        {
            Debug.Log("true");
            _animator.SetBool(_animIDSpinLoop, true);
            StartCoroutine(PlaySkillAnimation());
        }
        _input.spinloop = false;
        
    }

    IEnumerator PlaySkillAnimation()
    {
        isPlaying = true;

        for (int i = 0; i < loopcnt; i++)
        {
            _animator.Play("SpinLoop", 0, 0.0f); // 애니메이션 재생
            weapon.Use((damage / 10) * 8, 0.1f, 0.5f, 0.5f);
            // 애니메이션 길이만큼 대기
            yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        }

        isPlaying = false;
        _animator.SetBool(_animIDSpinLoop, false);
    }

    private void SpinAttack()
    {
        if (_input.spinattack)
        {
            _animator.SetTrigger(_animIDSpinAttack);
            weapon.Use(damage + (damage / 2), 0.1f, 0.6f, 0.6f);
        }
        _input.spinattack = false;
    }












}
