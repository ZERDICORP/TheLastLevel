using System.Collections.Generic;

public enum Role
{
    Left,
    Right
}

public class Replica
{
    public Role Role { get; set; }
    public string Text { get; set; }

    public Replica(Role role, string text)
    {
        Role = role;
        Text = text;
    }
}

public class Dialogue
{
    public List<Replica> Replicas { get; private set; }

    public Dialogue()
    {
        Replicas = new List<Replica>();
    }

    public void AddReplica(Role role, string text)
    {
        Replicas.Add(new Replica(role, text));
    }
}