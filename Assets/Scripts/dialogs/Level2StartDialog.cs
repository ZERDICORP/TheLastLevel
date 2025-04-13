using System.Collections.Generic;

class Level2StartDialog
{
    private Dialogue inner;

    public Level2StartDialog()
    {
        inner = new Dialogue();
        inner.AddReplica(Role.Right, "Поздравляю! Ты справился с первым… ха-ха… ПЕРВЫМ уровнем!");
        inner.AddReplica(Role.Right, "Осталось всего ничего… Ну, так… сотня… может, тысяча. Кто считает?");
        inner.AddReplica(Role.Right, "Посмотрим, осилишь ли ты этот.");

        inner.AddReplica(Role.Left, "Боже.. выключите кто нибудь ему звук..");
    }

    public List<Replica> replicas()
    {
        return inner.Replicas;
    }
}