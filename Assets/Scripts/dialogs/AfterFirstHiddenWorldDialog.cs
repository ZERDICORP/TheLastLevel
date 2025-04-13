using System.Collections.Generic;

class AfterFirstHiddenWorldDialog {
    private Dialogue inner;

    public AfterFirstHiddenWorldDialog()
    {
        inner = new Dialogue();
        inner.AddReplica(Role.Left, "Вау… Это что сейчас было? Похоже на телепорт…");
        inner.AddReplica(Role.Left, "Но хоть тут тихо. Без этого безумного вещателя.");
        inner.AddReplica(Role.Left, "И… погодите… стена стала ниже?");
    }

    public List<Replica> replicas() {
        return inner.Replicas;
    }
}