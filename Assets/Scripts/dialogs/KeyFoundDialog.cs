using System.Collections.Generic;

class KeyFoundDialog
{
    private Dialogue inner;

    public KeyFoundDialog()
    {
        inner = new Dialogue();
        inner.AddReplica(Role.Left, "Ключ. Кажется, это ключ.");
        inner.AddReplica(Role.Left, "А ключи, как правило… открывают двери, да?");
        inner.AddReplica(Role.Left, "Окей. Звучит как план");
    }

    public List<Replica> replicas()
    {
        return inner.Replicas;
    }
}