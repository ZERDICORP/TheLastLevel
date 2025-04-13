using System.Collections.Generic;

class FallDialog
{
    private Dialogue inner;

    public FallDialog()
    {
        inner = new Dialogue();
        inner.AddReplica(Role.Right, "Ахахахха как ты умудрился упасть в эту маленькую яму!!!");
        inner.AddReplica(Role.Right, "Такое ощущение что ты сделал это специально хахаха");
        inner.AddReplica(Role.Left, "Давай, смейся, потом будет не до смеха....");
        inner.AddReplica(Role.Right, "Хаха тебе меня не напугать, ЛОХ");
    }

    public List<Replica> replicas()
    {
        return inner.Replicas;
    }
}