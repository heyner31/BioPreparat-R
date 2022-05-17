using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    public float fireRate = 20f;
    public GameObject cameraGO;
    public GameObject rifleGO;
    public GameObject shotPointGO;
    public GameObject rifleLight;

    public ParticleSystem flash;
    public ParticleSystem flashSight;
    public ParticleSystem cartridge;

    public float reloadAnimTime = 2.5f;
    public float runAimAnimTime = 0.5f;

    Animator ani;

    float readyToFire;
    float reloadTimeRemaining = 0f;
    bool isSprinting = false;
    bool isAiming = false;
    bool isLightOn = false;
    float runAimTimeRemaining = 0f;

    void FixedUpdate()
    {
        float X = Input.GetAxis("Horizontal");
        float Y = Input.GetAxis("Vertical");

        //ANIM. CAMINAR/CORRER
        if (Time.time >= readyToFire)
        {
            ani.SetInteger("Fire", -1);
            if (X == 0f && Y == 0f)
            {
                ani.SetInteger("Movement", 0);
            } else
            {
                if (Y < 0f)
                {
                    ani.SetFloat("BackForward", -1f);
                } else
                {
                    ani.SetFloat("BackForward", 1f);
                }
                
                if (isSprinting)
                {
                    ani.SetInteger("Movement", 2);
                }
                else
                {
                    ani.SetInteger("Movement", 1);
                }
            }
        }

        //DISPARAR
        if (Input.GetButton("Fire1") && Time.time >= readyToFire && !isReloading())
        {
            //Debug.Log("Fire");

            readyToFire = Time.time + 1f/fireRate;
            fire();
            cartridge.Play();
            ani.SetInteger("Fire", 2);
            ani.SetInteger("Movement", -1);
        }

        //RECARGAR
        if (Input.GetKeyDown(KeyCode.R) && !isReloading())
        {
            reloadTimeRemaining = reloadAnimTime;
            ani.SetInteger("Reload", 1);
            //Debug.Log("R");
        }

        if (reloadTimeRemaining <= 1)
        {
            reloadTimeRemaining = 0;
            ani.SetInteger("Reload", -1);
        }
        else
        {
            reloadTimeRemaining -= Time.deltaTime;
        }

        if (reloadTimeRemaining <= 1)
        {
            reloadTimeRemaining = 0;
            ani.SetInteger("Reload", -1);
        }
        else
        {
            reloadTimeRemaining -= Time.deltaTime;
        }

        //APUNTAR
        if (Input.GetButton("Fire2"))
        {
            //Debug.Log("Apuntando");
            ani.SetBool("Sight", true);
            //flash.gameObject.SetActive(false);
            //flashSight.gameObject.SetActive(true);
            isAiming = true;
        } 
        else
        {
            ani.SetBool("Sight", false);
            //flash.gameObject.SetActive(true);
            //flashSight.gameObject.SetActive(false);
            isAiming = false;
        }

        //CORRER
        /*if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && !isReloading() && !isAiming)
        {
            //if (PlayerMovement.speed != 10f)
            //{
                PlayerMovement.speed = 10f;
            //}

            //if (isSprinting == false)
            //{
                isSprinting = true;
            //}
            
        } 
        else
        {
            //if (PlayerMovement.speed != 6f)
            //{
                PlayerMovement.speed = 6f;
            //}

            //if (isSprinting == true)
            //{
                isSprinting = false;
            //}
        }*/
    }

    void fire()
    {
        RaycastHit hit;
        if (Physics.Raycast(shotPointGO.transform.position, shotPointGO.transform.TransformDirection(new Vector3(0,0,-1)), out hit))
        {
            flash.Play();
            //flashSight.Play();
            //Gizmos.DrawLine(cameraGO.transform.position, )
            Debug.DrawLine(shotPointGO.transform.position, hit.point);
        }
        /*if (Physics.Raycast(rifleGO.transform.position, rifleGO.transform.TransformDirection(new Vector3(0,1,0)), out hit))
        {
            flash.Play();
            //flashSight.Play();
            //Gizmos.DrawLine(cameraGO.transform.position, )
            Debug.DrawLine(transform.position, hit.point);
        }*/
        /*if (Physics.Raycast(cameraGO.transform.position, cameraGO.transform.forward, out hit))
        {
            flash.Play();
            flashSight.Play();
            //Gizmos.DrawLine(cameraGO.transform.position, )
            Debug.DrawLine(transform.position, hit.point);
        }*/
    }

    bool isReloading()
    {
        if (reloadTimeRemaining <= 0)
        {
            return false;
        } 
        else
        {
            return true;
        }
    }

    void Start()
    {
        ani = GetComponent<Animator>();
        ani.SetInteger("Movement", 0);
    }

    void Update()
    {
        //CORRER
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && !isReloading() && !isAiming)
        {
            PlayerMovement.speed = 10f;
            isSprinting = true;
        } 
        else
        {
            PlayerMovement.speed = 6f;
            isSprinting = false;
        }

        //LUZ DEL ARMA
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (isLightOn)
            {
                rifleLight.SetActive(false);
                isLightOn = false;
            } 
            else
            {
                rifleLight.SetActive(true);
                isLightOn = true;
            }
        }
    }
}
