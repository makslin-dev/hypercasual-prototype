using UnityEngine;

public class ViewManager : MonoBehaviour
{
    [SerializeField] private Canvas[] _views;
   
    private void OnEnable()
    {
        GameManager.Instance.OnNextBlockStart += AddScore;
    }
    private void Awake()
    {
        SwitchView(1);
    }
    private void OnDisable()
    {
        GameManager.Instance.OnNextBlockStart -= AddScore;
    }
    private void SwitchView(int id)
    {
        foreach (var view in _views)
        {
            view.enabled = false;
        }
        _views[id].enabled = true;
    }
    private void AddScore()
    {

    }
}
