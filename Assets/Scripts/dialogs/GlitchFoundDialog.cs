using System.Collections.Generic;

class GlitchFoundDialog
{
    private Dialogue inner;

    public GlitchFoundDialog()
    {
        inner = new Dialogue();
        inner.AddReplica(Role.Right, "Эй… Стой. Тебе туда нельзя. Там ничего нет! Совсем ничего!");

        inner.AddReplica(Role.Left, "Написано: \"Клавиша E — единственный способ выбраться…\"");
        inner.AddReplica(Role.Left, "Хмм. Значит ли это, что мне нужно нажать на клавишу E?");

        inner.AddReplica(Role.Right, "Конечно нет! Что за чушь! Это просто… визуальный баг! Забудь об этом.");
        inner.AddReplica(Role.Right, "Я даю тебе последний шанс. Пройди уровень. Или останешься… навсегда.");
        inner.AddReplica(Role.Right, "Быстро. Слушайся. Слушай меня...");
    }

    public List<Replica> replicas()
    {
        return inner.Replicas;
    }
}