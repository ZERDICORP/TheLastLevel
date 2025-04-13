using System.Collections.Generic;

class FailedDblJumpDialog
{
    private Dialogue inner;

    public FailedDblJumpDialog()
    {
        inner = new Dialogue();
        inner.AddReplica(Role.Left, "И как я должен преодолеть такое расстоение.. это просто издевательство!");
    }

    public List<Replica> replicas()
    {
        return inner.Replicas;
    }
}