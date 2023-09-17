using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ThrowPokemon : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public GameObject _lastParent;
    private bool grabbed = false;
    public GameObject partnerManager;
    private Vector3 collisionPosition;
    public int slotID;
    private XRGrabInteractable _grabInteractable;
    private bool socket;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _grabInteractable = GetComponent<XRGrabInteractable>();

    }

    // Update is called once per frame
    public void prepareThrow()
    {
        socket = false;
        foreach (var VARIABLE in _grabInteractable.interactorsSelecting)
        {
            //print(VARIABLE.GetType().ToString());
            string s = VARIABLE.GetType().ToString();
            if ( s.Equals("UnityEngine.XR.Interaction.Toolkit.XRSocketInteractor"))
            {
                //print("socket");
                socket = true;
            }
        }
        if(!socket){
            _rigidbody.constraints &= ~RigidbodyConstraints.FreezeAll;
            transform.SetParent(null);
            grabbed = true;
            
        }
        else
        {
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            transform.SetParent(_lastParent.transform);
            grabbed = false;
        }
    }
    void FixedUpdate()
    {
        if(grabbed) _rigidbody.AddForce(new Vector3(0, -1.0f, 0)*4);  
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            collisionPosition = transform.position;
            SpawnPokemon();
            grabbed = false;
            //print(_lastParent);
            transform.SetParent(_lastParent.transform);
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            transform.position = _lastParent.transform.position;
            transform.rotation = _lastParent.transform.rotation;
        }
    }

    private void SpawnPokemon()
    {
        partnerManager.GetComponent<PartnerManager>().takeOutPokemon(slotID,collisionPosition);
    }
    public void DespawnPokemon()
    {
        partnerManager.GetComponent<PartnerManager>().despawnPartner(slotID);
    }
}
