using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 0.01f;
    [SerializeField] float health = 100f;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsMoving", true);
        transform.Translate(Vector3.forward * speed);
        HorizontalMovement();

    }

    void HorizontalMovement(){
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * speed);
        if(transform.position.x > 1.2f){
            transform.position = new Vector3(1.2f, transform.position.y, transform.position.z);            
        }
        else if(transform.position.x < -1.2f){
            transform.position = new Vector3(-1.2f, transform.position.y, transform.position.z);            
        }
    }

    void OnTriggerEnter(Collider collision){

        Debug.Log("OnTriggerEnter called with " + collision.gameObject.tag);

        if(collision.gameObject.tag == "Finish"){
            speed = 0;
            animator.SetBool("IsMoving", false);
            animator.SetBool("IsWin", true);
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            StartCoroutine(LoadSceneAfterDelay(0, 2)); // 2 seconds delay
        }
        StartCoroutine(LoadSceneAfterDelay(1, 2)); // 2 seconds delay
    }
        if(collision.gameObject.tag == "Log Obstacle"){
            health -= 30;
            print(health);
            if(health <= 0){
                animator.SetBool("IsMoving", false);
                animator.SetBool("IsDead", true);
                speed = 0;
                StartCoroutine(LoadSceneAfterDelay(0, 2));
            }
        }
        if(collision.gameObject.tag == "Rock Obstacle"){
            health -= 50;
            print(health);
            if(health <= 0){
                animator.SetBool("IsMoving", false);
                animator.SetBool("IsDead", true);
                speed = 0;
                StartCoroutine(LoadSceneAfterDelay(0, 2));
            }
        }
    }
    IEnumerator LoadSceneAfterDelay(int sceneIndex, float delay){
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(sceneBuildIndex: sceneIndex);
    }
}
