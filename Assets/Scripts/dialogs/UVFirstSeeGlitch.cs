using System.Collections.Generic;

class UVFirstSeeGlitch {
    private Dialogue inner;

    public UVFirstSeeGlitch()
    {
        inner = new Dialogue();
        inner.AddReplica(Role.Right, "Ты куда делся?! НЕ СМЕЙ больше так делать!");
        inner.AddReplica(Role.Right, "Думаешь, это просто так сойдёт с рук?! Ты заплатишь за это…");
        inner.AddReplica(Role.Right, "Мне нужно… кое-что исправить… Но знай — тебе конец.");

        inner.AddReplica(Role.Left, "Полегче, ковбой. Я просто отходил пописать, всё ок. )");
    }

    public List<Replica> replicas() {
        return inner.Replicas;
    }
}