namespace BetterMissions.Models;

public class MissionData {
    public Reckless1 Reckless1 { get; set; }
    public Reckless2 Reckless2 { get; set; }
    public Normal1 Normal1 { get; set; }
    public Prepared1 Prepared1 { get; set; }
    public Prepared2 Prepared2 { get; set; }
}

public class Reckless1 {
    public float MissionLength { get; set; }
}
public class Reckless2 {
    public float MissionLength { get; set; }
}
public class Normal1 {
    public float MissionLength { get; set; }
}
public class Prepared1 {
    public float MissionLength { get; set; }
}
public class Prepared2 {
    public float MissionLength { get; set; }
}
