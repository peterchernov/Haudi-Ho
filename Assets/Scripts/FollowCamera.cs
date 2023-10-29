using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 offset = new Vector3(5f, 5f, -10f);

    private void OnValidate()
    {
        if (!playerTransform) playerTransform = FindAnyObjectByType<PlayerMovement>().transform;
    }

    private void Start()
    {
        transform.rotation = Quaternion.LookRotation(-offset);
    }

    private void LateUpdate()
    {
        transform.position = playerTransform.position + offset;
    }
}
