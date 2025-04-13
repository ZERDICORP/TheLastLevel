using System.Collections.Generic;

class DoubleJumpAchiveDialog
{
    private Dialogue inner;

    public DoubleJumpAchiveDialog()
    {
        inner = new Dialogue();
        inner.AddReplica(Role.Left, "Хм… Что это было?.. Похоже, какая-то новая способность. Возможно, пригодится.");
    }

    public List<Replica> replicas()
    {
        return inner.Replicas;
    }
}