namespace ZaidimuPortalasMVC.Models
{
    // įrašoBūsena enumeracija iš esybių modelio
    public enum IrasoBusena
    {
        Priimtas,
        Atmestas,
        Patvirtintas
    }

    // Administratoriaus sprendimas dėl skundo (Skundas.būsena, sprendimoData, sprendimoKomentaras)
    public class ComplaintDecision
    {
        public int ComplaintId { get; set; }

        public IrasoBusena Busena { get; set; } = IrasoBusena.Priimtas;

        public DateTime? SprendimoData { get; set; }

        public string? SprendimoKomentaras { get; set; }
    }
}
