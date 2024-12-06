using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DestroyItems : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] GameObject BrokenPrefab;
    [SerializeField] GameObject DropArea;
    [SerializeField] private float fallDistance = 1.5f; // Jarak maksimum objek jatuh ke bawah
    private bool isDestroyed = false; // Variabel untuk memastikan Destroy() hanya dipanggil sekali

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Destroy()
    {
        if (isDestroyed) return; // Jika sudah dihancurkan, keluar dari fungsi

        isDestroyed = true; // Tandai bahwa objek sudah dihancurkan

        // Buat instance dari BrokenPrefab
        GameObject brokenInstance = Instantiate(BrokenPrefab, transform.position, transform.rotation);
        Destroy(gameObject);

        // Dapatkan Rigidbody dari brokenInstance
        Rigidbody[] rigidbodies = brokenInstance.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody body in rigidbodies)
        {
            // Hitung arah jatuh (ke bawah)
            Vector3 fallDirection = (DropArea.transform.position - body.transform.position).normalized;
            // Atur kecepatan objek jatuh ke bawah
            body.velocity = fallDirection * fallDistance; // Atur kecepatan sesuai kebutuhan
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PandaRolling"))
        {
            Destroy();
        }
    }

    // private void Block()
    // {
    //     navMeshModifier.overrideArea = true;
    //     navMeshModifier.area = 1;

    //     NavMeshSurface nav
    // }
}