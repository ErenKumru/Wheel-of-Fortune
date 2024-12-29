using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ValidatedButton : MonoBehaviour
{
    [SerializeField] protected Button button;

    protected virtual void AddButtonClickAction(UnityAction action)
    {
        button.onClick.AddListener(action);
    }

    protected virtual void ActivateButton()
    {
        gameObject.SetActive(true);
    }

    protected virtual void DeactivateButton()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if(button == null)
        {
            if(TryGetComponent(out Button buttonComponent))
            {
                button = buttonComponent;
            }
            else
            {
                Debug.LogError("Missing Button component on \"" + gameObject.name + "\"!");
            }
        }
    }
#endif
}
