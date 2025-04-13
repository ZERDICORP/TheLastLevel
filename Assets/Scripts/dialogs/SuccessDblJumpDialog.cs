using System.Collections.Generic;

class SuccessDblJumpDialog
{
    private Dialogue inner;

    public SuccessDblJumpDialog()
    {
        inner = new Dialogue();
        inner.AddReplica(Role.Left, "Ух ты! Я… только что сделал двойной прыжок?!");
        inner.AddReplica(Role.Left, "Это вообще законно?.. Как-то уж слишком… противоестественно.");
        inner.AddReplica(Role.Left, "Но, чёрт возьми, это будет полезно. Надеюсь, не последний сюрприз.");
    }

    public List<Replica> replicas()
    {
        return inner.Replicas;
    }
}