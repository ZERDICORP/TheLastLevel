using System.Collections.Generic;

class FirstJumpDialog
{
    private Dialogue inner;

    public FirstJumpDialog()
    {
        inner = new Dialogue();
        inner.AddReplica(Role.Left, "Прыгать? Не уверен что у меня получится.. Попробую ещё раз.");
    }

    public List<Replica> replicas()
    {
        return inner.Replicas;
    }
}