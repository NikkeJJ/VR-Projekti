using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class FireBulletOnActivate : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 20;
    public int targetLayer = 8;
    public int targetCount = 0;
    public GameObject endText;

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireBullet);
        endText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (targetCount >= 3)
        {
            EndGame();
        }
    }

    public void FireBullet(ActivateEventArgs arg)
    {
        GameObject spawnedBullet = Instantiate(bullet);
        spawnedBullet.transform.position = spawnPoint.position;
        spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
        Destroy(spawnedBullet, 5);

        RaycastHit hit;
        if (Physics.Raycast(spawnPoint.position, spawnPoint.forward, out hit))
        {
            if (hit.collider.gameObject.layer == targetLayer)
            {
                targetCount++;
            }
        }
    }

    void EndGame()
    {
        SceneManager.LoadScene("Main VR Scene 1");
    }
}