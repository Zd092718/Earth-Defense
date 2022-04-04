using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] MeshRenderer[] renderers = null;
    [SerializeField] ParticleSystem explosion;
    void OnTriggerEnter(Collider other)
    {
        CrashSequence(1f);
    }

    void CrashSequence(float delay)
    {
        foreach (var renderer in renderers)
        {
            renderer.enabled = false;
        }

        explosion.Play();
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<PlayerControls>().enabled = false;
        Invoke("ReloadLevel", delay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
