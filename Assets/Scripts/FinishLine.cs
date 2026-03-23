using UnityEngine;
using System.Collections;

public class FinishLine : MonoBehaviour
{
    public float respawnTime = 30f;

    private Collider col;
    private MeshRenderer mesh;

    private void Start()
    {
        col = GetComponent<Collider>();
        mesh = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RaceManager.Instance.CrossFinishLine();

            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        col.enabled = false;
        mesh.enabled = false;

        yield return new WaitForSeconds(respawnTime);

        col.enabled = true;
        mesh.enabled = true;
    }
}