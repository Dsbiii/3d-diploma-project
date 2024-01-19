using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerProfileAnimation : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler
{
    [SerializeField] private Transform _gear;
    private Coroutine _rotateCoroutine;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _rotateCoroutine = StartCoroutine(RotateGear());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_rotateCoroutine != null)
            StopCoroutine(_rotateCoroutine);
    }

    private IEnumerator RotateGear()
    {
        while (true)
        {
            _gear.transform.rotation *= Quaternion.Euler(0, 0, 2);
            yield return null;
        }
    }
}
