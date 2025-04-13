using System.Collections.Generic;

class ThirdJumpDialog
{
    private Dialogue inner;

    public ThirdJumpDialog()
    {
        inner = new Dialogue();
        inner.AddReplica(Role.Left, "А вот сейчас уже неплохо! Возможно, я даже справлюсь!");
        inner.AddReplica(Role.Left, "Хотя всё это до жути странно… Как будто я внутри дешёвого 2D-платформера.");
    }

    public List<Replica> replicas()
    {
        return inner.Replicas;
    }
}