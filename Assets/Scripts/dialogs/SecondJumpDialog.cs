using System.Collections.Generic;

class SecondJumpDialog
{
    private Dialogue inner;

    public SecondJumpDialog()
    {
        inner = new Dialogue();
        inner.AddReplica(Role.Left, "Ха! Кажется, что-то получается. Но все равно слабовато..");
    }

    public List<Replica> replicas()
    {
        return inner.Replicas;
    }
}