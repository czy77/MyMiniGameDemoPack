using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletContoller : MonoBehaviour
{
    // Start is called before the first frame update
    /// <summary>
    /// 移动速度
    /// </summary>
    public float Speed = 5.0f;

    private GameObject _swordAirPrefabs;
    /// <summary>
    /// 移动方向
    /// </summary>
    private Vector3 _moveDirection;

    private Vector3 offVector3;

    public int NeedToGenerateCount;

    private Collider2D _polygonCollider;

    private List<GameObject> BulletList = new List<GameObject>();


    void Start()
    {
        _moveDirection = Vector3.right;
        offVector3 = new Vector3(this.transform.position.x +5, this.transform.position.y + 1, this.transform.position.z);
        _polygonCollider = GetComponent<PolygonCollider2D>();
        _swordAirPrefabs = Resources.Load<GameObject>("Prefabs/SwordAir");

    }

    // Update is called once per frame
    void Update()
    {
        BulletList.ForEach(bullet => bullet.transform.position += Time.deltaTime * Speed * _moveDirection);
    }

    /// <summary>
    /// 生成子弹 ,由动画最后一帧调用
    /// </summary>
    public void GenerateBullet()
    {
        for (int i = 0; i < NeedToGenerateCount; i++)
        {
            BulletList.Add(Instantiate(_swordAirPrefabs, this.transform.position + offVector3, Quaternion.identity));
        }
    }

}
