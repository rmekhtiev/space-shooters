using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2f;

    [SerializeField]
    private float _tilt = 1f;

    [SerializeField]
    private Transform _bulletSpawner;

    [SerializeField]
    private Bullet _bulletPrefab;

    [SerializeField]
    private Transform _bullets;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _shotClip;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShotRoutine());
    }

    // Update is called once per frame
    void Update() => Move();

    private void Move() {
        var horizonal = Input.GetAxis("Horizontal");
        transform.Translate(horizonal * _speed * Time.deltaTime, 0f, 0f, Space.World);
        transform.eulerAngles = Vector3.forward * (-horizonal * _tilt);
    }

    private IEnumerator ShotRoutine()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Shot();

                yield return new WaitForSeconds(1f);
            }

            yield return null;
        }
    }
    private void Shot()
    {
        var bullet = Instantiate(_bulletPrefab, _bullets);
        bullet.transform.position = _bulletSpawner.position;
        _audioSource.PlayOneShot(_shotClip);
    }
}
