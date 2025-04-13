using System.Collections.Generic;

class FirstTryToPassWallDialog
{
    private Dialogue inner;

    public FirstTryToPassWallDialog()
    {
        inner = new Dialogue();
        inner.AddReplica(Role.Left, "Эээ… Это вообще реально?..");

        inner.AddReplica(Role.Right, "Продолжай ныть — так точно застрянешь здесь навсегда.");

        inner.AddReplica(Role.Left, "Повтори это ещё раз — и твои пиксельные зубы отправятся в инвентарь.");

        inner.AddReplica(Role.Right, "ХАХАХАХА! Какая агрессия! Мне нравится!");

    }

    public List<Replica> replicas()
    {
        return inner.Replicas;
    }
}