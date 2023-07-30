using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class HandCursor : MonoBehaviour
{
    [BoxGroup("Hand Cursor Setup"), SerializeField] private CanvasGroup _cursorCanvas;
    [BoxGroup("Hand Cursor Setup"), SerializeField] private Transform _cursorTransform;
    [BoxGroup("Hand Cursor Setup"), SerializeField] private Transform _handTransform;
    [BoxGroup("Hand Cursor Setup"), SerializeField] private Transform _tapCircleTransform;
    [BoxGroup("Hand Cursor Settings"), SerializeField] private bool _activateCursor;
    [BoxGroup("Hand Cursor Settings"), ShowIf(nameof(_activateCursor)), SerializeField] private bool _hideMouseCursor;
    [BoxGroup("Hand Cursor Settings"), ShowIf(nameof(_activateCursor)), SerializeField] private bool _smoothCursorMovement;
    [BoxGroup("Hand Cursor Settings"), ShowIf(nameof(_smoothCursorMovement)), SerializeField] private float _cursorMoveSpeed = 10f;
    private Tween _tapCircleTween;
    private Tween _handCursorTween;
    
    private void Update()
    {
        if (!_activateCursor)
        {
            _cursorCanvas.alpha = 0;
            _cursorCanvas.gameObject.SetActive(false);
            return;
        }

        _cursorCanvas.alpha = 1;
        _cursorCanvas.gameObject.SetActive(true);
        UseCursor();
    }

    private void UseCursor()
    {
        SetMouseCursorVisibility();
        MoveCursorWithMouseInput();
        CursorTap();
    }

    private void CursorTap()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _tapCircleTween.Complete();
            _handCursorTween.Complete();
            _tapCircleTransform.gameObject.SetActive(true);
            _tapCircleTransform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            _tapCircleTween = _tapCircleTransform.DOScale(0.4f, 0.15f).SetEase(Ease.Linear);
            _handCursorTween = _handTransform.DOScale(0.7f, 0.15f).SetEase(Ease.Linear);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _tapCircleTween.Complete();
            _handCursorTween.Complete();
            _handCursorTween = _handTransform.DOScale(1, 0.15f).SetEase(Ease.Linear);
            _tapCircleTween = _tapCircleTransform.DOScale(0.8f, 0.15f).SetEase(Ease.Linear).OnComplete(() => _tapCircleTransform.gameObject.SetActive(false));
        }
    }

    private void SetMouseCursorVisibility()
    {
        if (_hideMouseCursor)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
    }

    private void MoveCursorWithMouseInput()
    {
        if (_smoothCursorMovement)
        {
            _cursorTransform.position = Vector2.Lerp(_cursorTransform.position, Input.mousePosition, _cursorMoveSpeed * Time.deltaTime);
        }
        else
        {
            _cursorTransform.position = Input.mousePosition;
        }
    }
}