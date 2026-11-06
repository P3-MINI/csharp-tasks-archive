namespace AdeptusMechanicus;

public record Cogitator(int CogitatorId, string Name, string Location);

public record ImplantOrder(int ImplantOrderId, string ForgeWorld, string Name, string Category, double Quality, decimal Cost);

public record Techpriest(int TechpriestId, int ImplantOrderId, string Name, string Rank, string Department, int Age);

public static class Database {
    public static readonly List<Cogitator> Cogitators = [
        new( 1, "Cogitator-Alpha",    "Sector A-1"),
        new( 2, "Cogitator-Beta",     "Sector A-2"),
        new( 3, "Cogitator-Gamma",    "Sector A-3"),
        new( 4, "Cogitator-Delta",    "Sector B-1"),
        new( 5, "Cogitator-Epsilon",  "Sector B-2"),
        new( 6, "Cogitator-Zeta",     "Sector C-1"),
        new( 7, "Cogitator-Eta",      "Sector C-2"),
        new( 8, "Cogitator-Theta",    "Sector C-3"),
        new( 9, "Cogitator-Iota",     "Sector D-1"),
        new(10, "Cogitator-Kappa",    "Sector D-2"),
        new(11, "Cogitator-Lambda",   "Sector D-3"),
        new(12, "Cogitator-Mu",       "Sector E-1"),
        new(13, "Cogitator-Nu",       "Sector E-2"),
        new(14, "Cogitator-Xi",       "Sector E-3"),
        new(15, "Cogitator-Omicron",  "Sector F-1"),
        new(16, "Cogitator-Pi",       "Sector F-2"),
    ];

    public static readonly Dictionary<int, decimal> Readings = new() {
        // Sector A
        [1] = 20.1M,
        [2] = 20.3M,
        [3] = 20.1M,
        // Sector B
        [4] = 99.9M,
        [5] = 20.2M,
        // Sector C
        [6] = 20.0M,
        [7] = 20.4M,
        [8] = 20.2M,
        // Sector D
        [9] = 22.1M,
        [10] = 20.1M,
        [11] = 105.3M,
        // Sector E
        [12] = 20.2M,
        [13] = 20.0M,
        [14] = 20.3M,
        // Sector F
        [15] = 20.1M,
        [16] = 20.2M,
    };

    public static readonly List<Techpriest> Techpriests = [
        new(1,  1,  "Halix Vorn",       "Magos",       "Enginseer",     142),
        new(2,  2,  "Skitarii Alpha",   "Skitarii",    "Skitarii",       34),
        new(3,  3,  "Varnak Sollex",    "Archmagos",   "Biologis",      310),
        new(4,  11, "Thalrix Dorn",     "Magos",       "Dominus",       187),
        new(5,  13, "Sereth-9",         "Techpriest",  "Logis",          96),
        new(6,  14, "Marn Thule",       "Techpriest",  "Genetor",       118),
        new(7,  4,  "Draven Koll",      "Magos",       "Enginseer",     205),
        new(8,  5,  "Osric Vane",       "Techpriest",  "Enginseer",      73),
        new(9,  12, "Sarn Vex",         "Magos",       "Dominus",       164),
        new(10, 15, "Rek Dorn",         "Techpriest",  "Lexmechanicus",  88),
        new(11, 6,  "Skitarii Beta",    "Skitarii",    "Skitarii",       29),
        new(12, 7,  "Skitarii Gamma",   "Skitarii",    "Skitarii",       37),
        new(13, 8,  "Torv Kael",        "Archmagos",   "Biologis",      276),
        new(14, 16, "Halix Sturm",      "Techpriest",  "Enginseer",      81),
        new(15, 9,  "Servitor-7",       "Servitor",    "Servitorium",    45),
        new(16, 10, "Servitor-12",      "Servitor",    "Servitorium",    51),
        new(17, 17, "Voren Kast",       "Techpriest",  "Logis",         102),
        new(18, 18, "Sarn Vex II",      "Techpriest",  "Genetor",        69),
        new(19, 19, "Marn Thule II",    "Magos",       "Lexmechanicus", 153),
        new(20, 20, "Osric Vane III",   "Techpriest",  "Enginseer",      64),
        new(21, 21, "Voren Kast II",    "Techpriest",  "Explorator",     91),
        new(22, 22, "Sereth-10",        "Techpriest",  "Explorator",     77),
    ];

    public static readonly List<ImplantOrder> ImplantOrders = [
        new(1,  "Mars",   "Cortical Stack",    "Cranial",  0.95, 1200m),
        new(2,  "Mars",   "Ocular Implant",    "Ocular",   0.88,  850m),
        new(3,  "Mars",   "Neuroconnector",    "Nervous",  0.85, 1100m),
        new(4,  "Ryza",   "Servo-Arm",         "Bionic",   0.72,  650m),
        new(5,  "Ryza",   "Mechanadendrite",   "Bionic",   0.91,  980m),
        new(6,  "Lucius", "Bionic Leg",        "Bionic",   0.65,  720m),
        new(7,  "Lucius", "Bionic Arm",        "Bionic",   0.83, 1100m),
        new(8,  "Lucius", "Reflex Enhancer",   "Muscular", 0.92,  700m),
        new(9,  "Graia",  "Autosanguine",      "Nervous",  0.99, 1500m),
        new(10, "Graia",  "Cranial Circuitry", "Cranial",  0.77,  900m),
        new(11, "Mars",   "Cranial Circuitry", "Cranial",  0.89,  950m),
        new(12, "Ryza",   "Neuroconnector",    "Nervous",  0.94, 1150m),
        new(13, "Mars",   "Cortical Stack",    "Cranial",  0.91, 1250m),
        new(14, "Mars",   "Bionic Arm",        "Bionic",   0.87, 1150m),
        new(15, "Ryza",   "Reflex Enhancer",   "Muscular", 0.88,  720m),
        new(16, "Lucius", "Servo-Arm",         "Bionic",   0.79,  680m),
        new(17, "Graia",  "Ocular Implant",    "Ocular",   0.93,  880m),
        new(18, "Graia",  "Mechanadendrite",   "Bionic",   0.86, 1020m),
        new(19, "Graia",  "Cortical Stack",    "Cranial",  0.94, 1180m),
        new(20, "Graia",  "Bionic Leg",        "Bionic",   0.81,  740m),
        new(21, "Graia",  "Ocular Implant",    "Ocular",   0.90,  920m),
        new(22, "Graia",  "Cranial Circuitry", "Cranial",  0.84,  960m),
    ];
}
