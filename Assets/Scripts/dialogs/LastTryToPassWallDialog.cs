using System.Collections.Generic;

class LastTryToPassWallDialog
{
    private Dialogue inner;

    public LastTryToPassWallDialog()
    {
        inner = new Dialogue();
        inner.AddReplica(Role.Right, "Ты себя со стороны видел? Прыгаешь как безмозглый слизняк!");
        inner.AddReplica(Role.Right, "Думаешь, кто-то как ты может выбраться отсюда?");
        inner.AddReplica(Role.Right, "Это место — твоя новая реальность. Зеркало беспомощности. Ты и уровень — одно целое.");
        inner.AddReplica(Role.Right, "Ты не игрок. Ты персонаж. И ты... застрял. Навсегда.");
    }

    public List<Replica> replicas()
    {
        return inner.Replicas;
    }
}