using ZaidimuPortalasMVC.Models;

namespace ZaidimuPortalasMVC.Data;

// Prototype only: skundų peržiūros duomenys laikomi atmintyje.
// Skundai imami iš SampleData.Complaints (juos pateikia Games/ReportReview),
// o administratoriaus sprendimai saugomi čia. Pakeitimai galioja iki programos perkrovimo.
public static class ComplaintSampleData
{
    private static readonly object Sync = new();
    private static readonly Dictionary<int, ComplaintDecision> Decisions = new();
    private static bool _seeded;

    public static IReadOnlyList<Complaint> GetUnreviewed()
    {
        lock (Sync)
        {
            EnsureSeeded();
            return SampleData.Complaints
                .Where(c => GetStatus(c.Id) == IrasoBusena.Priimtas)
                .OrderBy(c => c.CreatedAt)
                .ToArray();
        }
    }

    public static Complaint? GetComplaint(int id)
    {
        lock (Sync)
        {
            EnsureSeeded();
            return SampleData.Complaints.FirstOrDefault(c => c.Id == id);
        }
    }

    public static ComplaintDecision GetDecision(int complaintId)
    {
        lock (Sync)
        {
            return Decisions.TryGetValue(complaintId, out var d)
                ? d
                : new ComplaintDecision { ComplaintId = complaintId };
        }
    }

    public static void Decide(int complaintId, IrasoBusena busena, string? komentaras)
    {
        lock (Sync)
        {
            Decisions[complaintId] = new ComplaintDecision
            {
                ComplaintId = complaintId,
                Busena = busena,
                SprendimoData = DateTime.Now,
                SprendimoKomentaras = komentaras
            };
        }
    }

    private static IrasoBusena GetStatus(int complaintId) =>
        Decisions.TryGetValue(complaintId, out var d) ? d.Busena : IrasoBusena.Priimtas;

    // Įdeda pavyzdinius skundus, kad sąrašas nebūtų tuščias
    private static void EnsureSeeded()
    {
        if (_seeded) return;
        _seeded = true;

        var nextId = SampleData.Complaints.Count == 0 ? 1 : SampleData.Complaints.Max(c => c.Id) + 1;

        SampleData.Complaints.Add(new Complaint
        {
            Id = nextId,
            ReviewId = 2,
            Username = "Kritikas99",
            Reason = "Atsiliepime yra siužeto spoilerių be įspėjimo.",
            CreatedAt = new DateTime(2026, 10, 28)
        });

        SampleData.Complaints.Add(new Complaint
        {
            Id = nextId + 1,
            ReviewId = 3,
            Username = "Gamer456",
            Reason = "Reklaminis turinys, nesusijęs su žaidimu.",
            CreatedAt = new DateTime(2026, 11, 2)
        });
    }
}
