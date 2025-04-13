using System.Collections.Generic;

class Level3StartDialog
{
    private Dialogue inner;

    public Level3StartDialog()
    {
        inner = new Dialogue();
        inner.AddReplica(Role.Right, "Уже третий уровень? Да ты прям рекордсмен! АХАХА!");
        inner.AddReplica(Role.Right, "Ну что ж… Попробуй справиться с этим. Обещаю — выход уже совсем рядом. Хахаха… или нет.");

        inner.AddReplica(Role.Left, "Ага, верю-верю… почти как родному. Только без обнимашек, ладно?");
    }

    public List<Replica> replicas()
    {
        return inner.Replicas;
    }
}