using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private FirstPersonController _playerMovement;
    [SerializeField] private float _timeDuringScreamer;
    [SerializeField] private string _sceneToLoadAfterScreamer;
    [SerializeField] private CameraManager _cameraManager;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COUCOU");
        if (collision.collider.gameObject.tag == "Enemy") {
            
            //Stop enemy Movement
            if (collision.collider.gameObject.TryGetComponent<EnemyMovement>(out var enemyMovement))
            {
                enemyMovement.SetEnemyState(EnemyState.Attack);
            }
            //Stop player movement
            if (_playerMovement != null)
            {
                _playerMovement.CanMove(false);
            }

            // Camera on enemy (screamer)
            _cameraManager.SetCameraScreamerOn();
            //Load new scene
            StartCoroutine(WaitToLoadScene());
        }
    }

    IEnumerator WaitToLoadScene()
    {
        yield return new WaitForSeconds(_timeDuringScreamer);
        SceneManager.LoadScene(_sceneToLoadAfterScreamer);
    }


}
