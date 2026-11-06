namespace OrdoMajoris;

public record Testimony(string Source, string Witness, string Statement, double HeresyPercent);

public record Order(int OrderId, string Name, string HomeWorld, int Members);

public record Sister(int SisterId, int OrderId, string Name, string Rank, int Age, string FavouriteWeapon, int KillCount);

public static class Database {
    public static readonly List<Testimony> EcclesiarchyTestimonies = [
        new("Ecclesiarchy", "Bishop Aurelius", "Praised be the God-Emperor of Mankind.",               5.0),
        new("Ecclesiarchy", "Sister Helena",   "The heretic shall burn. So it is written!",            8.0),
        new("Ecclesiarchy", "Father Matthias", "The faithful stand firm against the warp.",            4.0),
        new("Ecclesiarchy", "Bishop Aurelius", "The Governor has not attended mass in months.",       12.0),
        new("Ecclesiarchy", "Sister Helena",   "The Governor consorts with xenos traders.",           15.0),
        new("Ecclesiarchy", "Father Matthias", "The Governor speaks of 'autonomy'. That is heresy!",  18.0),
        new("Ecclesiarchy", "Bishop Aurelius", "The Governor must be purged. So it is written!",      22.0),
    ];

    public static readonly List<Testimony> GovernorTestimonies = [
        new("Governor", "Magistrate Vorn", "The Emperor is... a distant figure.",                45.0),
        new("Governor", "Magistrate Vorn", "Heresy is an outdated concept.",                     75.0),
        new("Governor", "Magistrate Vorn", "The faithful are... a varied population.",           55.0),
        new("Governor", "Advisor Kael",    "The Governor is loyal to the Throne.",               20.0),
        new("Governor", "Advisor Kael",    "The Ecclesiarchy exaggerates the Governor's words.", 62.0),
        new("Governor", "Advisor Kael",    "The Governor serves the people first.",              48.0),
        new("Governor", "Magistrate Vorn", "We need more autonomy from Terra.",                  85.0),
    ];

    public static readonly List<double> PopulationHeresy = [
        2.0, 4.5, 1.5, 8.0, 3.0, 2.5, 5.0, 95.0, 3.5, 6.0,
        1.0, 7.5, 4.0, 2.0, 3.5, 5.5, 2.5, 3.0, 9.0, 4.5,
    ];

    public static readonly List<Order> Orders = [
        new(1, "Our Martyred Lady", "Ophelia VII", 4200),
        new(2, "Ebon Chalice",      "Terra",       3100),
        new(3, "Argent Shroud",     "Terra",       2800),
        new(4, "Bloody Rose",       "Ophelia VII", 3600),
        new(5, "Valorous Heart",    "Ophelia VII", 1500),
        new(6, "Sacred Rose",       "Terra",       2400),
    ];

    public static readonly List<Sister> Sisters = [
        new(1,  1, "Amalia Verrin",   "Canoness",        52, "Power Sword",  412),
        new(2,  1, "Ysolde Marne",    "Palatine",        41, "Storm Bolter", 268),
        new(3,  1, "Thessaly Dorn",   "Sister Superior", 33, "Boltgun",      154),
        new(4,  2, "Oriel Vane",      "Battle Sister",   24, "Boltgun",       47),
        new(5,  2, "Drusilla Koll",   "Palatine",        38, "Power Sword",  231),
        new(6,  2, "Seraphina Kael",  "Seraphim",        27, "Bolt Pistol",  119),
        new(7,  3, "Marta Thule",     "Canoness",        60, "Power Sword",  587),
        new(8,  3, "Halena Sturm",    "Battle Sister",   22, "Flamer",        38),
        new(9,  3, "Veronika Kast",   "Sister Superior", 35, "Chainsword",   176),
        new(10, 4, "Sarai Vex",       "Palatine",        45, "Storm Bolter", 305),
        new(11, 4, "Rekka Dorn",      "Battle Sister",   26, "Meltagun",      62),
        new(12, 4, "Tamsin Orlo",     "Repentia",        29, "Eviscerator",  143),
        new(13, 1, "Elara Quill",     "Battle Sister",   23, "Boltgun",       41),
        new(14, 3, "Judith Hale",     "Battle Sister",   28, "Flamer",        85),
        new(15, 1, "Corvina Ashe",    "Palatine",        39, "Chainsword",   249),
        new(16, 2, "Lenora Brask",    "Battle Sister",   25, "Heavy Bolter",  56),
        new(17, 3, "Isolde Varn",     "Seraphim",        31, "Bolt Pistol",  137),
        new(18, 4, "Beatrix Hollen",  "Sister Superior", 36, "Boltgun",      188),
        new(19, 2, "Nerys Talon",     "Battle Sister",   21, "Meltagun",      29),
        new(20, 1, "Cassia Morn",     "Repentia",        30, "Eviscerator",  158),
        new(21, 4, "Ilena Rusk",      "Battle Sister",   27, "Boltgun",       73),
        new(22, 2, "Vesper Kroll",    "Canoness",        55, "Power Sword",  451),
        new(23, 3, "Anselma Dray",    "Seraphim",        26, "Bolt Pistol",  104),
        new(24, 4, "Maren Solt",      "Battle Sister",   24, "Flamer",        51),
        new(25, 1, "Philomena Gray",  "Sister Superior", 34, "Storm Bolter", 163),
        new(26, 2, "Katrin Voss",     "Repentia",        32, "Eviscerator",  197),
        new(27, 3, "Odalys Fenn",     "Palatine",        43, "Chainsword",   292),
        new(28, 1, "Yvanna Creed",    "Battle Sister",   29, "Heavy Bolter",  94),
        new(29, 5, "Lysandra Crowe",  "Repentia",        28, "Eviscerator",  171),
        new(30, 5, "Ottilie Brandt",  "Repentia",        33, "Eviscerator",  215),
        new(31, 5, "Mirelle Fane",    "Palatine",        47, "Power Sword",  326),
        new(32, 5, "Gwenhael Roth",   "Battle Sister",   25, "Meltagun",      58),
        new(33, 6, "Zenobia Hart",    "Canoness",        49, "Power Sword",  378),
        new(34, 6, "Isabeau Ferrant", "Sister Superior", 37, "Heavy Bolter", 201),
        new(35, 6, "Petra Lindqvist", "Battle Sister",   26, "Heavy Bolter",  67),
        new(36, 6, "Calista Wren",    "Seraphim",        29, "Bolt Pistol",  112),
    ];
}
