using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleInteractor : Interactor {

    [Header("Interact")]
    [SerializeField] private Camera _cam;
    [SerializeField] private float _interactionDistance;
    [SerializeField] private LayerMask _interactionLayer;

    //Raycast
    private RaycastHit _raycastHit;
    private ISelectable _selectable;


    public override void Interact() {
        //Get Ray details from screen
        Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));//centre of the camera
        //returns bool in case of something hitting
        //_raycastHit = contain info about object struck (transform, etc) --- _interacationDistance = makes it so we can interact with objects accross the map --- _interactionLayer = makes sure only certain layers are interactable
        if (Physics.Raycast(ray, out _raycastHit, _interactionDistance, _interactionLayer)) {
            _selectable = _raycastHit.transform.GetComponent<ISelectable>(); //found a selectable object

            if (_selectable != null) {
                _selectable.OnHoverEnter();

                if (_input.activatePressed) {
                    _selectable.OnSelect();
                }
            }
        }
        //cuz of ray, out mthod _raycastHit.transform is usable here
        if (_raycastHit.transform == null && _selectable != null) {
            _selectable.OnHoverExit();
            _selectable = null;
        }
    }
}
