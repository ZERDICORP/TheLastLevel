using System.Collections.Generic;

class NooooDialog
{
    private Dialogue inner;

    public NooooDialog()
    {
        inner = new Dialogue();
        inner.AddReplica(Role.Right, "НЕТ-НЕТ-НЕТ-НЕТ-НЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕ…");
    }

    public List<Replica> replicas()
    {
        return inner.Replicas;
    }
}