using System.Collections.Generic;

class TestDialog {
    private Dialogue inner;

    public TestDialog()
    {
        inner = new Dialogue();
        inner.AddReplica(Role.Right, "Привет чертила!");
        inner.AddReplica(Role.Right, "Самое время поиграть в мою игру хахахах");
    }

    public List<Replica> replicas() {
        return inner.Replicas;
    }
}