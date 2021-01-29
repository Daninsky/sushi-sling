using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    #region Serialized Private Fields

    [SerializeField] private Image nextBullet;

    [SerializeField] private ObjectContainer objectContainer;

    #endregion


    #region Private Fields
    private GameObject activeObject;
    #endregion

    #region Monobehaviour Callbacks

    private void Update()
    {
        activeObject = objectContainer.GetActiveObject();
        nextBullet.sprite = activeObject.GetComponent<SpriteRenderer>().sprite;
    }

    #endregion
}
