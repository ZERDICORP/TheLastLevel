using UnityEngine;

public class DialogNextButton : MonoBehaviour
{
    public void OnClick()
    {
        DialogEngine.DialogSkipOrNext();
    }
}
