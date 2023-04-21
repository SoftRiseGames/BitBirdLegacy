using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParallaxEffect : MonoBehaviour
{
    public bool parallax;
    private float length, startpos;
    public GameObject Cam;
    public float parallaxEffect;
    public bool upsideDown;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.X))
        {
            StartCoroutine(parallaxTimer());
        }

        if (gameObject.transform.rotation.z == 0 && !upsideDown)
        {
            float temp = (Cam.transform.position.x * (1 - parallaxEffect));
            float dist = (Cam.transform.position.x * parallaxEffect);

            transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

            if (temp > startpos + length) startpos += length;
            else if (temp < startpos - length) startpos -= length;


        }
        else if (gameObject.transform.rotation.z == 0.7071068f)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (!upsideDown)
                {
                    upsideDown = true;
                }
                else if (upsideDown)
                {
                    upsideDown = false;
                }
            }
            float temp = (Cam.transform.position.y * (1 - parallaxEffect));
            float dist = (Cam.transform.position.y * parallaxEffect);

            transform.position = new Vector3(transform.position.x, startpos + dist, transform.position.z);

            if (temp > startpos + length) startpos += length;
            else if (temp < startpos - length) startpos -= length;


        }
        else if (gameObject.transform.rotation.z == 1 && upsideDown)
        {
          
            float temp = (Cam.transform.position.x * (1 - parallaxEffect));
            float dist = (Cam.transform.position.x * parallaxEffect);

            transform.position = new Vector3(startpos - dist, transform.position.y, transform.position.z);

            if (temp > startpos + length) startpos -= length;
            else if (temp < startpos - length) startpos += length;



        }
    }
    

    IEnumerator parallaxTimer()
    {
        parallax = false;
        yield return new WaitForSeconds(2f);
        parallax = true;
    }
}
