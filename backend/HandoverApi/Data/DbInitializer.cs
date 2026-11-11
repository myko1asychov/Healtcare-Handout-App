using HandoverApi.Models;

namespace HandoverApi.Data;

public static class DbInitializer
{
    public static void Seed(AppDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Handovers.Any())
        {
            return; // Datenbank wurde bereits befüllt
        }

        var sampleHandovers = new Handover[]
        {
            new Handover
            {
                PatientCode = "PAT-0042",
                Room = "101-A",
                Priority = "Urgent",
                Situation = "Patient klagt über akute Atemnot und Brustschmerzen.",
                Background = "Hypertonie in der Anamnese, Knie-Operation vor 3 Tagen.",
                Assessment = "Verdacht auf Lungenembolie. Sauerstoffsättigung 89%.",
                Recommendation = "Diensthabenden Arzt sofort verständigen, Sauerstoff verabreichen."
            },
            new Handover
            {
                PatientCode = "PAT-0118",
                Room = "204-B",
                Priority = "Important",
                Situation = "Hohes Fieber (38,5 °C) am Nachmittag.",
                Background = "Zustand nach Appendektomie, Tag 2.",
                Assessment = "Möglicher postoperativer Entzündungsprozess.",
                Recommendation = "Paracetamol nach Verordnung verabreichen, Blutbild abnehmen."
            },
            new Handover
            {
                PatientCode = "PAT-0089",
                Room = "105-C",
                Priority = "Normal",
                Situation = "Zustand stabil, Vorbereitung auf die Entlassung.",
                Background = "Planmäßige Reha abgeschlossen.",
                Assessment = "Keine Beschwerden, Vitalwerte im Normbereich.",
                Recommendation = "Entlassungsdokumente bis 14:00 Uhr vorbereiten."
            }
        };

        context.Handovers.AddRange(sampleHandovers);
        context.SaveChanges();
    }
}