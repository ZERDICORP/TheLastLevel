using System.Collections.Generic;

class Level1StartDialog {
    private Dialogue inner;

    public Level1StartDialog()
    {
        inner = new Dialogue();
        inner.AddReplica(Role.Right, "Добро пожаловать в мою игру! Я долго трудился, чтобы ты оказался здесь. Проходи уровни, и может в конце я отпущу тебя.");
        inner.AddReplica(Role.Left, "Чего? ты кто такой то?");
        inner.AddReplica(Role.Right, "Я разработчик! Между прочим. Я сделал эту игру и ты должен её пройти!!");
        inner.AddReplica(Role.Left, "Не понял, а зачем?");
        inner.AddReplica(Role.Right, "ПРОХОДИ Я СКАЗАЛ!!");
        inner.AddReplica(Role.Left, "Ладно, зачем орать?");
    }

    public List<Replica> replicas() {
        return inner.Replicas;
    }
}