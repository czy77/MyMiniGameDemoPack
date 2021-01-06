using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCreature : MonoBehaviour
{

    /// <summary>
    /// 要跟随的目标
    /// </summary>
    public Transform FollowTarget;


    private Camera _mainCamera;


    // Start is called before the first frame update
    void Start()
    {
        //FollowTarget = transform;
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (FollowTarget != null)
        {
            transform.position = _mainCamera.WorldToScreenPoint(FollowTarget.position);
        }
    }
}
