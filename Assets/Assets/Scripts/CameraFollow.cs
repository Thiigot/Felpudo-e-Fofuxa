using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Referência do Player")]
    [SerializeField] private Transform player;

    [Header("Configurações")]
    [SerializeField] private float offsetX = 3f;

    private float fixedY;
    private float fixedZ;

    void Start()
    {
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (player == null)
            return;

        transform.position = new Vector3(
            player.position.x + offsetX,
            fixedY,
            fixedZ
        );
    }
}