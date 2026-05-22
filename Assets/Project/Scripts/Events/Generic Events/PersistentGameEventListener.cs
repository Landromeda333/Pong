public class PersistentGameEventListener : GameEventListener
{
    private void Awake()
    {
        evt.Suscribe(this);
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    private void OnDestroy()
    {
        evt.Unuscribe(this);
    }
}