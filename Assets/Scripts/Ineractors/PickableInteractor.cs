using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableInteractor : Interactor {

    [Header("Pick and Drop")]
    [SerializeField] private Camera _cam;
    [SerializeField] private Transform _attachTransform;
    [SerializeField] private LayerMask _pickableLayer;
    [SerializeField] private float _pickableDistance;

    //Pick and Drop
    private bool _isPicked = false;
    private IPickable _pickable;

    //Raycast
    private RaycastHit _raycastHit;
    private ISelectable _selectable; 

    public override void Interact() {
        Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out _raycastHit, _pickableDistance, _pickableLayer)) {
            if (_input.activatePressed && !_isPicked) {
                _pickable = _raycastHit.transform.GetComponent<IPickable>();
                if (_pickable == null) return;

                _pickable.OnPicked(_attachTransform);
                _isPicked = true;
                return;
            }
        }

        if (_input.activatePressed && _isPicked && _pickable != null) {
            _pickable.OnDropped();
            _isPicked = false;
        }
    }
}

